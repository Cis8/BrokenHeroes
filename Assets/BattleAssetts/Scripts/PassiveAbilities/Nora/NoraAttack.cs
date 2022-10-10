using UnityEditor;
using UnityEngine;

using System.Linq;

namespace Assets.BattleAssetts.Scripts.PassiveAbilities.Nora
{
    public class NoraAttack : PassiveAbility
    {
        private int baseScaling;

        StatModifierData bonusAtk;

        public NoraAttack(Fighter parent) : base(parent, "Buff Nora On Attack")
        {
            bonusAtk = Resources.Load<StatModifierData>("Fighters/Nora/ModifiersData/NoraMagicAtkUpOnAttack");
            baseScaling = System.Convert.ToInt32(parent.fighterLogic.DmgCalculator.Evaluate(((NoraLogic)parent.fighterLogic).Scalings.dataArray.Where(e => e.Name == "Attack Buff On Attack").FirstOrDefault().Base_Formula));
            CheckInitialize();
        }
        public override void InitializeAbility()
        {
            BattleEventSystem.current.OnFighterTookDamage += CheckAttackResult;
        }


        private void CheckAttackResult(Fighter damagedFighter, DmgInfo info)
        {
            if(info.DealerFighter == Parent && info.Source.DmgSourceEnum == DmgSourceEnum.Attack)
            {
                if (damagedFighter.fighterLogic.IsAlive())
                {
                    // boost atk for 25% and take damage

                    bonusAtk.Amount = baseScaling;
                    bonusAtk.InitializeModifier(Parent, Parent).Apply();
                }
                else
                {
                    // boost atk for 50% and heal
                    
                    bonusAtk.Amount = baseScaling * 2;
                    bonusAtk.InitializeModifier(Parent, Parent).Apply();
                    Parent.GetUnit().AddEnergy(100);
                }
            }
        }

        public override void TerminateAbility()
        {
            BattleEventSystem.current.OnFighterTookDamage -= CheckAttackResult;
        }
    }
}