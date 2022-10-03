using UnityEditor;
using UnityEngine;

using System.Linq;
using Assets.BattleAssetts.Scripts;

namespace Assets.BattleAssetts.Scripts.PassiveAbilities.Nora
{
    public class NoraAbility : PassiveAbility
    {
        string healFormula;
        string damageFormula;
        string energyGainedUponKillFormula;
        public NoraAbility(Fighter parent) : base(parent, "Nora Ability Heal Damage Energy")
        {
            healFormula = ((NoraLogic)parent.fighterLogic).Scalings.dataArray.Where(e => e.Name == "Must Die Gained Life").FirstOrDefault().Self_Formula;
            damageFormula = ((NoraLogic)parent.fighterLogic).Scalings.dataArray.Where(e => e.Name == "Must Die Lost Life").FirstOrDefault().Self_Formula;
            energyGainedUponKillFormula = ((NoraLogic)parent.fighterLogic).Scalings.dataArray.Where(e => e.Name == "Must Die Gained Energy").FirstOrDefault().Base_Formula;
            InitializeAbility();
        }
        public override void InitializeAbility()
        {
            BattleEventSystem.current.OnFighterTookDamage += CheckAbilityResult;
        }

        public override void TerminateAbility()
        {
            BattleEventSystem.current.OnFighterTookDamage -= CheckAbilityResult;
        }

        private void CheckAbilityResult(Fighter damagedFighter, DmgInfo info)
        {
            if(info.DealerFighter == Parent && info.Source.DmgSourceEnum == DmgSourceEnum.Ability)
            {
                if (damagedFighter.fighterLogic.IsAlive())
                {
                    // take damage
                    int dmgAmount = CalcEngineUtil.Int32Calculator(Parent.fighterLogic.DmgCalculator, "",damageFormula, "", Parent.GetUnit());
                    Debug.Log("Dmg to be taken: " + dmgAmount);
                    Parent.fighterLogic.TakeDamage(new DmgInfo(
                        dmgAmount,
                        DmgTypeEnum.Magical,
                        new DmgSource(DmgSourceEnum.Passives, true),
                        Parent,
                        Parent,
                        false,
                        2f
                        ));
                }
                else
                {
                    // heal
                    Parent.fighterLogic.DmgCalculator.DataContext = Parent.GetUnit();
                    int healAmount = System.Convert.ToInt32(Parent.fighterLogic.DmgCalculator.Evaluate(healFormula));
                    Parent.fighterLogic.HealFlat(new HealInfo(
                        healAmount,
                        new HealSource(HealSourceEnum.Passives,true),
                        Parent,
                        Parent));
                    int energyToGain = System.Convert.ToInt32(Parent.fighterLogic.DmgCalculator.Evaluate(energyGainedUponKillFormula));
                    Parent.GetUnit().AddEnergy(energyToGain);
                }
            }
        }
    }
}