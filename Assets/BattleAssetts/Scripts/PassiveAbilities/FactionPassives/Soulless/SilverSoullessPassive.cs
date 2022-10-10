using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.BattleAssetts.Scripts.PassiveAbilities.FactionPassives
{
    public class SilverSoullessPassive : FactionPassiveAbility
    {
        public SilverSoullessPassive(string tag) : base(tag, "Silver Soulless") 
        {
            CheckInitialize();
        }

        public override void InitializeAbility()
        {
            BattleEventSystem.current.OnFighterTookDamage += CheckHeal;

        }

        private void CheckHeal(Fighter f, DmgInfo info)
        {
            if (info.DealerFighter.tag == info.DamagedFighter.tag)
            {
                f.fighterLogic.HealFlat(new HealInfo(
                    info.Amount / 3,
                    new HealSource(HealSourceEnum.Passives, false),
                    f,
                    f));
            }
        }

        public override void TerminateAbility()
        {
            BattleEventSystem.current.OnFighterTookDamage -= CheckHeal;
        }
    }
}