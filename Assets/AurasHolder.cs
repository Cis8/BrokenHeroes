using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AurasHolder : MonoBehaviour
{
    Dictionary<FactionEnum, int> playerFactionCounter = new Dictionary<FactionEnum, int>(), enemyFactionCounter = new Dictionary<FactionEnum, int>();

    List<PassiveAbility> playerAuras = new List<PassiveAbility>();
    List<PassiveAbility> enemyAuras = new List<PassiveAbility>();

    private void Start()
    {
        BattleEventSystem.current.OnFighterInstantiated += AddFaction;
        BattleEventSystem.current.OnAllFightersInstantiated += ApplyPassives;
    }

    private void ApplyPassives()
    {
        ApplyPassiveForParty("PlayerTeam");
        ApplyPassiveForParty("EnemyTeam");
    }

    private void ApplyPassiveForParty(string tag)
    {
        // Here all the switch cases to select which passives should be applied to the Passives in the "Auras" Child Obj of FightersManager.
        List<PassiveAbility> auraList;
        Dictionary<FactionEnum, int> factionsDictionary;
        if (tag == "PlayerTeam")
        {
            auraList = playerAuras;
            factionsDictionary = playerFactionCounter;
        }
        else
        {
            auraList = enemyAuras;
            factionsDictionary = enemyFactionCounter;
        }

        foreach (FactionEnum fact in factionsDictionary.Keys)
        {
            switch (fact)
            {
                case FactionEnum.Soulless:
                    if (factionsDictionary[fact] >= 2)
                    {
                        auraList.Add(new Assets.BattleAssetts.Scripts.PassiveAbilities.FactionPassives.BronzeSoullessPassive(tag));
                        if (factionsDictionary[fact] >= 4)
                        {
                            //auraList.Add(new Assets.BattleAssetts.Scripts.PassiveAbilities.FactionPassives.SilverSoullessPassive(tag));
                            if (factionsDictionary[fact] >= 6)
                            {
                                //auraList.Add(new Assets.BattleAssetts.Scripts.PassiveAbilities.FactionPassives.GoldSoullessPassive(tag));
                            }
                        }
                    }
                    break;
                case FactionEnum.Ancient:
                    if (factionsDictionary[fact] >= 2)
                    {
                        //auraList.Add(new Assets.BattleAssetts.Scripts.PassiveAbilities.FactionPassives.BronzeAncientPassive(tag));
                        if (factionsDictionary[fact] >= 4)
                        {
                            //auraList.Add(new Assets.BattleAssetts.Scripts.PassiveAbilities.FactionPassives.SilverAncientPassive(tag));
                            if (factionsDictionary[fact] >= 6)
                            {
                                //auraList.Add(new Assets.BattleAssetts.Scripts.PassiveAbilities.FactionPassives.GoldAncientPassive(tag));
                            }
                        }
                    }
                    break;
                case FactionEnum.Human:
                    if (factionsDictionary[fact] >= 2)
                    {
                        //auraList.Add(new Assets.BattleAssetts.Scripts.PassiveAbilities.FactionPassives.BronzeHumanPassive(tag));
                        if (factionsDictionary[fact] >= 4)
                        {
                            //auraList.Add(new Assets.BattleAssetts.Scripts.PassiveAbilities.FactionPassives.SilverHumanPassive(tag));
                            if (factionsDictionary[fact] >= 6)
                            {
                                //auraList.Add(new Assets.BattleAssetts.Scripts.PassiveAbilities.FactionPassives.GoldHumanPassive(tag));
                            }
                        }
                    }
                    break;
            }
        }
    }

    private void AddFaction(Fighter f)
    {
        FactionEnum faction = f.GetUnit().Faction;
        if (f.tag == "PlayerTeam")
        {
            if (playerFactionCounter.ContainsKey(faction))
                playerFactionCounter[faction]++;
            else
                playerFactionCounter.Add(faction, 1);
        }
        else
        {
            if (enemyFactionCounter.ContainsKey(faction))
                enemyFactionCounter[faction]++;
            else
                enemyFactionCounter.Add(faction, 1);
        }
    }

    public void AddAura(string tag, PassiveAbility aura)
    {
        if (tag == "PlayerTeam")
            playerAuras.Add(aura);
        else
            enemyAuras.Add(aura);
    }

    private void OnDestroy()
    {
        BattleEventSystem.current.OnFighterInstantiated -= AddFaction;
        BattleEventSystem.current.OnAllFightersInstantiated -= ApplyPassives;
    }
}