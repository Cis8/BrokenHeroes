using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System;
using System.Linq;

public enum StateModifiersEnum { STUN, SILENCE, TERROR }


public abstract class FighterLogic
{
    [SerializeField]
    private Unit unit;
    private bool alive = true;
    // canCastAbilityThisTurn is set to false when the fighter attacks as first action. If it fills the energy while still executing its attacks he will not unleash the ability
    private bool canCastAbilityThisTurn = true;
    // actionNotStarted is set to false when the ActionToPerformNow returns "movingForward". It is reset when the turn ends
    private bool actionNotStarted = true;
    private string team;
    protected List<Modifier> modifiers;
    private List<PassiveAbility> passiveAbilities;
    private Fighter parent;
    public AttackStrategy baseAtkStrategy;
    public AttackStrategy abilityAtkStrategy;
    private StateModifiers stateModifiers;
    private CalcEngine.CalcEngine dmgCalculator;
    //private Type scalingType;
    //private dynamic scalings;
    //System.Object scalings;





    // counts the number of resurrections taken
    private int resurrectionCounter = 0;

    public List<Modifier> Modifiers { get => modifiers; set => modifiers = value; }
    protected Fighter Parent { get => parent; set => parent = value; }
    public Unit Unit { get => unit; set => unit = value; }
    public List<PassiveAbility> PassiveAbilities { get => passiveAbilities; set => passiveAbilities = value; }
    public int ResurrectionCounter { get => resurrectionCounter; set => resurrectionCounter = value; }
    public StateModifiers StateModifs { get => stateModifiers; set => stateModifiers = value; }
    public CalcEngine.CalcEngine DmgCalculator { get => dmgCalculator; set => dmgCalculator = value; }

    public FighterLogic(Unit unit, Fighter parent, string team)
    {
        Unit = unit;
        this.team = team;
        Parent = parent;
        modifiers = new List<Modifier>();
        passiveAbilities = new List<PassiveAbility>();
        stateModifiers = new StateModifiers(Parent);
        //scalingType = Type.GetType(parent.fighterName + "Scalings");
        DmgCalculator = new CalcEngine.CalcEngine();

        InitializeStrategy(true, Unit.baseAttackStrategy);
        InitializeStrategy(false, Unit.baseAttackStrategy);

        
        BattleEventSystem.current.OnFighterTookDamage += HealFromLifeSteal;
        BattleEventSystem.current.OnTurnStarted += TryResurrect;
        BattleEventSystem.current.OnFighterTurnEnded += ResetCanCastAbilityAndStartedAndActionNotStarted;
        BattleEventSystem.current.OnFighterRemoved += ClearPassivesSubscriptions;
    }

    private void ClearPassivesSubscriptions(Fighter f)
    {
        if(f == Parent)
        {
            foreach (PassiveAbility pa in PassiveAbilities)
            {
                pa.TerminateAbility();
            }
        }
    }

    private void ResetCanCastAbilityAndStartedAndActionNotStarted(Fighter f)
    {
        if(f == Parent)
        {
            canCastAbilityThisTurn = true;
            Unit.CurrentRemainingAttacks = Unit.CurrentAttacksPerAction;
            actionNotStarted = true;
        }
    }

    private void InitializeStrategy(bool isForBaseAttack, AttackStrategyEnum strategyEnum)
    {
        switch (strategyEnum)
        {
            case AttackStrategyEnum.Standard:
                if (isForBaseAttack)
                    baseAtkStrategy = new StandardStrategy();
                else
                    abilityAtkStrategy = new StandardStrategy();
                break;
            case AttackStrategyEnum.LessLife:
                if (isForBaseAttack)
                    baseAtkStrategy = new LessLifeStrategy();
                else
                    abilityAtkStrategy = new LessLifeStrategy();
                break;
        }
    }

    ~FighterLogic()
    {
        BattleEventSystem.current.OnFighterTookDamage -= HealFromLifeSteal;
        BattleEventSystem.current.OnTurnStarted -= TryResurrect;
        BattleEventSystem.current.OnFighterTurnEnded -= ResetCanCastAbilityAndStartedAndActionNotStarted;
    }

    public void ExecuteAttack()
    {
        List<Fighter> enemiesToAttack = baseAtkStrategy.ChooseTargets(Unit.NumberOfTargetsBaseAttack, team);
        AttackLogic(enemiesToAttack);
        unit.AddEnergy((int)((float)unit.CurrentEnergyPerAtk * GetFloatMultiplier(Unit.CurrentBonusEnergyGainedFromAttack)));
    }

    public void ExecuteAbility()
    {
        List<Fighter> enemiesToAttack = abilityAtkStrategy.ChooseTargets(Unit.NumberOfTargetsAbility, team);
        unit.ZeroEnergy();
        AbilityLogic(enemiesToAttack);
    }

    public virtual bool TakeDamage(DmgInfo info)
    {
        //Debug.Log("Dmg to be taken: " + info.Amount);
        BattleEventSystem.current.FighterAboutToTakeDamage(Parent, info);

        // Now we apply crit and dmg reductions from armor and magic resist
        info = CalculateDamageToBeTaken(info);
        if (Unit.CurrentThorns > 0 && info.Source.DmgSourceEnum == DmgSourceEnum.Attack)
        {
            info.DealerFighter.TakeDamage(new DmgInfo(
                (int)(GetFloatMultiplier(Unit.CurrentThorns, false) * info.Amount),
                DmgTypeEnum.True,
                new DmgSource(DmgSourceEnum.Thorns, false),
                Parent,
                info.DealerFighter,
                false,
                Unit.CurrentCriticalMultiplier));
        }
        Unit.SubtractLife(info.Amount);
        if(info.Source.IsReactable)
        {
            int percentEnergyGainedFromDmg = (int)(((float)info.Amount / (float)Unit.MaxHp) * 100);
            Unit.AddEnergy(percentEnergyGainedFromDmg);
        }
        alive = Unit.CurrentHP > 0;
        BattleEventSystem.current.FighterTookDamage(Parent, info);
        if (!alive)
            ResetState();
        //Debug.Log("Effective dmg taken: " + info.Amount);
        return alive;
    }

    private DmgInfo CalculateDamageToBeTaken(DmgInfo info)
    {
        // apply damage modifiers
        //Debug.Log("Perc mult: " + info.GetPercentModification());
        //Debug.Log("Flat mod: " + info.GetFlatModification());
        info.Amount = (int)((float)info.Amount * info.GetPercentModification()) + info.GetFlatModification();

        // apply critical damage multiplier if is a crit
        if (info.IsCritical)
        {
            info.Amount = (int)(info.Amount * info.CritModifier);
        }

        // apply armor or magic resist dmg reduction
        info = ApplyDefensiveStatisticReduction(info);
        return info;
    }

    void ApplyDoTResistanceForDotType(DmgInfo info, int DoTResistance)
    {
        if (DoTResistance > 100)
            info.Amount = 0;
        else if(DoTResistance > 0)
            info.Amount = (int)((float)info.Amount * (1f - GetFloatMultiplier(DoTResistance, false)));
        else if(DoTResistance < 0)
            info.Amount = (int)((float)info.Amount * (1f + GetFloatMultiplier(-DoTResistance, false)));
    }

    DmgInfo ApplyDefensiveStatisticReduction(DmgInfo info)
    {
        // here we consider the bleed/burn/poison resistances
        switch (info.Source.DmgSourceEnum)
        {
            case DmgSourceEnum.Bleed:
                ApplyDoTResistanceForDotType(info, Unit.CurrentBleedResist);
                break;
            case DmgSourceEnum.Burn:
                ApplyDoTResistanceForDotType(info, Unit.CurrentBurnResist);
                break;
            case DmgSourceEnum.Poison:
                ApplyDoTResistanceForDotType(info, Unit.CurrentPoisonResist);
                break;
            default:
                break;
        }

        int defensiveStatisticStat;
        if (info.Type == DmgTypeEnum.Physical)
        {
            defensiveStatisticStat = (int)((1f - GetFloatMultiplier(info.DealerFighter.GetUnit().CurrentArmorPenetration, false)) * Unit.ARM);
        }
        else if (info.Type == DmgTypeEnum.Magical)
        {
            defensiveStatisticStat = (int)((1f - GetFloatMultiplier(info.DealerFighter.GetUnit().CurrentMagicDefensePenetration, false)) * Unit.MDEF);
        }
        else
        {
            defensiveStatisticStat = 0;
        }
        // y = 20 (log ((1/8)*x+1))
        info.Amount -= (int)(info.Amount * DamageReductionByDefensiveStatisticPercentual(defensiveStatisticStat)) / 100;
        return info;
    }

    float DamageReductionByDefensiveStatisticPercentual(int defensiceStatisticAmount)
    {
        return (20 * Mathf.Log((defensiceStatisticAmount / 16) + 1));
    }

    protected virtual void AttackBase(List<Fighter> enemiesToAttack)
    {
        bool isCritical = CastDiceForCritical();
        foreach (Fighter en in enemiesToAttack)
        {
            GenerateDmgInfoForAtk(en, isCritical);
        }
    }

    protected virtual bool CastDiceForCritical()
    {
        return (1f - Unit.CurrentCriticalChance) < UnityEngine.Random.Range(0f, 1f);
    }

    protected virtual void GenerateDmgInfoForAtk(Fighter f, bool isCritical)
    {
        int totalBaseAmount = CalculateBaseDamageBasedOnScalings("Attack", f); // Unit.AttackScaleType, Unit.AttackScalingRate

        f.TakeDamage(new DmgInfo(
                    totalBaseAmount,
                    Unit.AttackDamageType,
                    new DmgSource(DmgSourceEnum.Attack),
                    parent,
                    f,
                    isCritical,
                    Unit.CurrentCriticalMultiplier));
    }

    protected int CalculateBaseDamageBasedOnScalings(string action, Fighter target) // List<ScalingTypeEnum> scalingTypes, List<float> scalingValues
    {
        /*float totalBaseAmount = 0f;
        for (int i = 0; i < scalingTypes.Count; i++)
        {
            float currentBaseAmount = 0f;
            switch (Unit.AttackScaleType[i])
            {
                case ScalingTypeEnum.Physical:
                    currentBaseAmount += ((float)Unit.CurrentPhysicalAttack * GetPercMultiplier(Unit.CurrentPhysicalAttackBonusPerc));
                    break;
                case ScalingTypeEnum.Magical:
                    currentBaseAmount += ((float)Unit.CurrentMagicalAttack * GetPercMultiplier(Unit.CurrentMagicalAttack));
                    break;
                case ScalingTypeEnum.MissingLife:
                    currentBaseAmount += (float)(Unit.MaxHP - Unit.CurrentHP);
                    break;
                case ScalingTypeEnum.CurrentLife:
                    currentBaseAmount += (float)(Unit.CurrentHP);
                    break;
                case ScalingTypeEnum.MaxLife:
                    currentBaseAmount += (float)(Unit.MaxHP);
                    break;
            }
            currentBaseAmount *= scalingValues[i];
            totalBaseAmount += currentBaseAmount;
        }
        return totalBaseAmount;*/


        // First we load the dmg scalings based on the fighter itself
        /*Debug.Log("Pre loading calculation");
        dmgCalculator.DataContext = Unit;
        System.Object[] scalingsDataArray = (System.Object[])(scalings.dataArray);
        dynamic dynamicResultOfTheFiltering = scalingsDataArray.Where(e => ((dynamic)e).Name == "Attack").FirstOrDefault();
        int ret = System.Convert.ToInt32(dmgCalculator.Evaluate(dynamicResultOfTheFiltering.Formula));
        Debug.Log("Post loading calculation");*/

        string toUseSelfFormula;
        string toUseTargetFormula;
        if (action == "Attack")
        {
            toUseSelfFormula = Unit.AttackSelfFormula;
            toUseTargetFormula = Unit.AttackTargetFormula;
        }
        else if (action == "Ability")
        {
            toUseSelfFormula = Unit.AbilitySelfFormula;
            toUseTargetFormula = Unit.AbilityTargetFormula;
        }
        else
            throw new System.Exception("Action not supported");

        int ret = 0;
        if(toUseSelfFormula != "")
        {
            DmgCalculator.DataContext = Unit;
            ret += System.Convert.ToInt32(DmgCalculator.Evaluate(toUseSelfFormula));
        }
            
        if (toUseTargetFormula != "")
        {
            DmgCalculator.DataContext = target.GetUnit();
            ret += System.Convert.ToInt32(DmgCalculator.Evaluate(toUseTargetFormula));
        }
            
        
        return ret;     
    }


    protected float GetFloatMultiplier(int stat, bool addHundredPercent = true)
    {
        return Unit.GetPercMultiplier(stat, addHundredPercent);
    }

    protected virtual void AbilityBase(List<Fighter> enemiesToAttack)
    {
        bool isCritical = (1f - Unit.CurrentCriticalChance) < UnityEngine.Random.Range(0f, 1f);
        foreach (Fighter en in enemiesToAttack)
        {
            GenerateDmgInfoForAbility(en, isCritical);
        }
    }

    protected virtual void GenerateDmgInfoForAbility(Fighter f, bool isCritical)
    {
        int totalBaseAmount = CalculateBaseDamageBasedOnScalings("Ability", f); // Unit.AbilityScaleTypes, Unit.AbilityScalingRates

        f.TakeDamage(new DmgInfo(
                    totalBaseAmount,
                    Unit.AbilityDamageType,
                    new DmgSource(DmgSourceEnum.Ability),
                    parent,
                    f,
                    isCritical,
                    Unit.CurrentCriticalMultiplier));
    }

    protected virtual void AttackLogic(List<Fighter> targets)
    {
        AttackBase(targets);
    }

    protected virtual void AbilityLogic(List<Fighter> targets)
    {
        AbilityBase(targets);
    }

    public string Name()
    {
        //Disabled.Log("Unit is: " + Unit.fighterName.ToString());
        return Unit.fighterName.ToString();
    }
    public string ActionToPerformNow()
    {
        // TODO: check if is stunned, so that "nothing" is returned
        if(canCastAbilityThisTurn && Unit.CurrentEnergy >= 100 && !stateModifiers.IsSilenced())
        {
            return "ability";
        }
        else
        {
            canCastAbilityThisTurn = false;
            // return movingForward
            if(actionNotStarted)
            {
                actionNotStarted = false;
                return "moveForward";
            }
            else
            {
                if (Unit.CurrentRemainingAttacks > 0)
                {
                    Unit.CurrentRemainingAttacks--;
                    return "baseAtk";
                }
                else
                {
                    return "moveBack";
                }
            }
        }
    }

    public void RemoveModifier(Modifier m)
    {
        m.Remove();
    }

    // the heal augment percent or reduction percent is applied ONLY HERE
    public void HealFlat(HealInfo info)
    {
        if(info.Amount > 0 && (IsAlive() || info.Source.HealSourceEnum == HealSourceEnum.Resurrection))
        {
            info.Amount = ModifyHealAmountBonusPerc(info.Amount);
            Unit.AddLife(info.Amount);
            BattleEventSystem.current.FighterHealed(Parent, info);
        }
    }
    protected void HealFromLifeSteal(Fighter f, DmgInfo info)
    {
        
        if ((info.Source.DmgSourceEnum == DmgSourceEnum.Ability || info.Source.DmgSourceEnum == DmgSourceEnum.Attack) && info.DealerFighter == Parent && Unit.CurrentLifesteal > 0)
        {
            Debug.Log("HealingFromLifesteal, it is: " + Unit.CurrentLifesteal);
            //Disabled.Log("LS: " + Unit.CurrentLifesteal + ", DMG: " + info.Amount + ", MULT: " + (Unit.CurrentLifesteal * (float)info.Amount));
            HealInfo recover = new HealInfo((int)(Unit.CurrentLifesteal * (float)info.Amount), new HealSource(HealSourceEnum.Lifesteal, true), Parent, Parent);
            HealFlat(recover);
        }
    }

    public bool IsAlive()
    {
        return alive;
    }

    protected virtual void TryResurrect(int turn)
    {
        //Debug.Log();
        if (!IsAlive() && Unit.CurrentRemainingResurrections > 0)
        {
            resurrectionCounter++;
            Unit.DecrementRemainingResurrections();
            HealFlat(
                new HealInfo(
                Unit.MaxHp,
                new HealSource(HealSourceEnum.Resurrection, true),
                Parent,
                Parent));
            alive = true;
            BattleEventSystem.current.FighterResurrected(Parent);
        }
    }

    protected int ModifyHealAmountBonusPerc(float healBase)
    {
        return (int)(healBase * GetFloatMultiplier(Unit.CurrentHealBonusPerc));
    }

    protected void ResetState()
    {
        int n = modifiers.Count;
        for(int i=0; i < n; i++)
        {
            modifiers[0].Remove();
        }
    }
    public class StateModifiers
    {
        Fighter parent;

        public StateModifiers(Fighter p)
        {
            parent = p;
            BattleEventSystem.current.OnFighterAboutToTakeDamage += CheckNegateCritical;
            BattleEventSystem.current.OnTurnStarted += DecrementAllStateModifiers;
        }

        public class StateModifierInfo
        {
            StateModifiersEnum type;
            int duration;
            Fighter target;
            Fighter appliedBy;

            public StateModifierInfo(StateModifiersEnum type, int duration, Fighter target, Fighter appliedBy)
            {
                this.Type = type;
                this.Duration = duration;
                this.Target = target;
                this.AppliedBy = appliedBy;
            }

            public StateModifiersEnum Type { get => type; set => type = value; }
            public int Duration { get => duration; set => duration = value; }
            public Fighter Target { get => target; set => target = value; }
            public Fighter AppliedBy { get => appliedBy; set => appliedBy = value; }
        }
        // STATE MODIFIERS
        private int stun = 0;
        private int silence = 0;
        private int terror = 0;

        public int Stun { get => stun; }
        public int Silence { get => silence; }
        public int Terror { get => terror;}

        public int GetStateModifierDuration(StateModifiersEnum type)
        {
            switch (type)
            {
                case StateModifiersEnum.STUN:
                    return stun;
                case StateModifiersEnum.SILENCE:
                    return silence;
                case StateModifiersEnum.TERROR:
                    return terror;
                default:
                    return 0;
            }
        }

        public bool IsStunned()
        {
            return Stun > 0;
        }

        public bool IsSilenced()
        {
            return Silence > 0;
        }

        public bool IsTerrified()
        {
            return Terror > 0;
        }


        private void DecrementAllStateModifiers(int t)
        {
            DecrementStun(parent);
            DecrementSilence(parent);
            DecrementTerror(parent);
        }

        public void DecrementStun(Fighter origin)
        {
            if(Stun > 0)
            {
                stun--;
                if (Stun == 0)
                    BattleEventSystem.current.StateModifierRemoved(new StateModifierInfo(StateModifiersEnum.STUN, Stun, origin, parent));
            }
        }

        public void AddStun(int amount, Fighter origin)
        {
            bool notify = false;
            if (Stun == 0 && amount > 0)
                notify = true;
            stun += amount;
            if (notify)
                BattleEventSystem.current.StateModifierApplied(new StateModifierInfo(StateModifiersEnum.STUN, Stun, origin, parent));
        }

        public void DecrementSilence(Fighter origin)
        {
            if(Silence > 0)
            {
                silence--;
                if (Silence == 0)
                    BattleEventSystem.current.StateModifierRemoved(new StateModifierInfo(StateModifiersEnum.SILENCE, Stun, origin, parent));
            }
        }

        public void AddSilence(int amount, Fighter origin)
        {
            bool notify = false;
            if (Silence == 0 && amount > 0)
                notify = true;
            silence += amount;
            if (notify)
                BattleEventSystem.current.StateModifierApplied(new StateModifierInfo(StateModifiersEnum.SILENCE, Stun, origin, parent));
        }

        public void DecrementTerror(Fighter origin)
        {
            if(Terror > 0)
            {
                terror--;
                if (Terror == 0)
                    BattleEventSystem.current.StateModifierRemoved(new StateModifierInfo(StateModifiersEnum.TERROR, Stun, origin, parent));
            }
        }

        public void AddTerror(int amount, Fighter origin)
        {
            bool notify = false;
            if (Terror == 0 && amount > 0)
                notify = true;
            terror += amount;
            if (notify)
                BattleEventSystem.current.StateModifierApplied(new StateModifierInfo(StateModifiersEnum.TERROR, Stun, origin, parent));
        }

        private void CheckNegateCritical(Fighter f, DmgInfo info)
        {
            if(Terror > 0 && parent == info.DealerFighter)
            {
                info.IsCritical = false;
            }
        }
    }

}
