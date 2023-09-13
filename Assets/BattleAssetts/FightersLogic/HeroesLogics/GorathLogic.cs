using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 0) scalings
using System.Linq;
using UnityEngine.AddressableAssets;

public class GorathLogic : FighterLogic
{
    // 1) scalings
    private GorathScalings scalings;
    CalcEngine.CalcEngine ce;
    StatModifierData kingsMarkAdBuff;
    string kingsMarkHealFormula;
    private MeltingDarkBladeData meltingDarkBladeData;
    private KingsMarkData kingsMarkData;

    public GorathLogic(Unit unit, Fighter parent, string team) : base(unit, parent, team)
    {
        ce = new CalcEngine.CalcEngine();
        // 2) scalings
        Addressables.LoadAssetAsync<GorathScalings>(parent.fighterName + "Scalings").Completed += handle => {
            Scalings = handle.Result;
            kingsMarkHealFormula = scalings.dataArray.Where(da => da.Name == "KingsMarkHeal").FirstOrDefault().Self_Formula;
            //Add passives: PassiveAbilities.Add(new Assets.BattleAssetts.Scripts.PassiveAbilities.Nora.NoraAttack(Parent
            meltingDarkBladeData = GameObject.Instantiate(Resources.Load<MeltingDarkBladeData>("Fighters/Gorath/ModifiersData/MeltingDarkBladeData"));
            kingsMarkData = GameObject.Instantiate(Resources.Load<KingsMarkData>("Fighters/Gorath/ModifiersData/KingsMarkData"));
        };

        kingsMarkAdBuff = GameObject.Instantiate(Resources.Load<StatModifierData>("Fighters/Gorath/ModifiersData/KingsMarkAdBuffData"));

        //passiveCalculator.DataContext = nr;

        PassiveAbilities.Add(new NoraResurrection(Parent, "Immortal Hate"));
        BattleEventSystem.current.OnFighterTookDamage += ApplyKingsMark;
        BattleEventSystem.current.OnFighterDied += HealGorath;
        BattleEventSystem.current.OnFighterDied += BuffGorath;
    }

    ~GorathLogic()
    {
        BattleEventSystem.current.OnFighterTookDamage -= ApplyKingsMark;
        BattleEventSystem.current.OnFighterDied -= HealGorath;
        BattleEventSystem.current.OnFighterDied -= BuffGorath;
    }

    private void ApplyKingsMark(Fighter f, DmgInfo info)
    {
        if (info.DamagedFighter == Parent && info.DealerFighter.tag != Parent.tag && (info.Source.DmgSourceEnum == DmgSourceEnum.Attack || info.Source.DmgSourceEnum == DmgSourceEnum.Ability))
        {
            kingsMarkData.InitializeModifier(info.DealerFighter, Parent).Apply();
        }
    }

    private void HealGorath(Fighter f, DmgInfo info)
    {
        int CalculateHealing()
        {
            ce.DataContext = Parent.GetUnit();
            return System.Convert.ToInt32(ce.Evaluate(kingsMarkHealFormula));
        }

        if (f.fighterLogic.HasModifierWithName("KingsMark") || info.Source.SourceName == "KingsMark")
        {
            Parent.fighterLogic.HealFlat(new HealInfo(
                CalculateHealing(),
                new HealSource(HealSourceEnum.Passives, true),
                Parent,
                Parent));
        }
    }

    private void BuffGorath(Fighter f, DmgInfo info)
    {
        if (f.fighterLogic.HasModifierWithName("KingsMark") || info.Source.SourceName == "KingsMark")
        {
            kingsMarkAdBuff.InitializeModifier(Parent, Parent).Apply();
        }
    }



    protected override void AbilityLogic(List<Fighter> targets)
    {
        base.AbilityLogic(targets);
        meltingDarkBladeData.InitializeModifier(this.Parent, this.Parent).Apply();
    }

    public GorathScalings Scalings { get => scalings; set => scalings = value; }
}
