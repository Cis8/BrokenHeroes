﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.BattleAssetts.Scripts.PassiveAbilities.FactionPassives
{
    public class BronzeSoullessPassive : FactionPassiveAbility
    {
        StatModifierData speedBuffModifierData;
        public BronzeSoullessPassive(string tag) : base(tag, "Bronze Soulless") 
        {
            CheckInitialize();
        }

        public override void InitializeAbility()
        {
            Addressables.LoadAssetAsync<StatModifierData>("BronzeSoullessSpeedBuff").Completed += handle =>
            {
                speedBuffModifierData = handle.Result;
            };
            BattleEventSystem.current.OnFighterTookDamage += CheckSpeedBuff;
        }

        private void CheckSpeedBuff(Fighter f, DmgInfo info)
        {
            if (info.DealerFighter.tag == info.DamagedFighter.tag)
            {
                int speedAmount = (int)(100 * ((float)info.Amount / (float)info.DamagedFighter.GetUnit().MaxHP));
                speedBuffModifierData.Amount = speedAmount;
                speedBuffModifierData.InitializeModifier(f, f).Apply();
                Debug.Log("New speed is: " + f.GetUnit().SPEED);
            }
        }

        public override void TerminateAbility()
        {
            BattleEventSystem.current.OnFighterTookDamage -= CheckSpeedBuff;
        }
    }
}