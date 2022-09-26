using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class FightersList
{
    // the following lists are sorted by SPEED
    List<Fighter> playerFightersBySpeed;
    List<Fighter> enemyFightersBySpeed;
    List<Fighter> allFightersBySpeed;

    // the following lists are sorted by POSITION
    SortedList<int, Fighter> playerFightersByPosition;
    SortedList<int, Fighter> enemyFightersByPosition;

    // list to store the Fighters that died and will be removed at the end of the turn
    List<Fighter> toRemove;

    public FightersList()
    {
        BattleEventSystem.current.OnModifierApplied += SortFightersBySpeed;

        playerFightersBySpeed = new List<Fighter>();
        enemyFightersBySpeed = new List<Fighter>();
        allFightersBySpeed = new List<Fighter>();

        playerFightersByPosition = new SortedList<int, Fighter>();
        enemyFightersByPosition = new SortedList<int, Fighter>();

        toRemove = new List<Fighter>();
    }

    public void AddFighter(Fighter f)
    {
        List<Fighter> l;
        SortedList<int, Fighter> sl;
        if (f.tag == "PlayerTeam")
        {
            l = playerFightersBySpeed;
            sl = playerFightersByPosition;
        }
        else
        {
            l = enemyFightersBySpeed;
            sl = enemyFightersByPosition;
        }
        sl.Add(f.GetUnit().Position, f);

        var index = getIndexOfTheFirstSlowerFighter(l, f);
        if (index >= 0)
        {
            l.Insert(index, f);
        }
        else
            l.Add(f);

        //////////////Disabled.Log("Player fighters by speed: " + playerFightersBySpeed.Count);
        //////////////Disabled.Log("Enemty fighters by speed: " + enemyFightersBySpeed.Count);
        //////////////Disabled.Log("Player fighters by pos: " + playerFightersByPosition.Count);
        //////////////Disabled.Log("Enemty fighters by pos: " + enemyFightersByPosition.Count);

        //////////////Disabled.Log("l: " + l.Count);

        // now it is added to the whole list of fighters
        index = getIndexOfTheFirstSlowerFighter(allFightersBySpeed, f);
        l = allFightersBySpeed;
        if (index >= 0)
            l.Insert(index, f);
        else
            l.Add(f);
        //////////////Disabled.Log("All fighters: " + allFighters.Count);
    }

    private int getIndexOfTheFirstSlowerFighter(List<Fighter> list, Fighter fighter)
    {
        return list.FindIndex(f => f.GetUnit().CurrentSpeed < fighter.GetUnit().CurrentSpeed);
    }

    public List<Fighter> GetPlayerFightersBySpeed()
    {
        return playerFightersBySpeed;
    }

    public List<Fighter> GetEnemyFightersBySpeed()
    {
        return enemyFightersBySpeed;
    }

    public List<Fighter> GetAllFightersBySpeed()
    {
        return allFightersBySpeed;
    }

    public List<Fighter> GetPlayerFightersByLowestLife()
    {
        // TODO: improve efficiency
        return playerFightersBySpeed.OrderBy(f => f.GetUnit().CurrentHP).ToList();
    }

    public List<Fighter> GetEnemyFightersByLowestLife()
    {
        // TODO: improve efficiency
        return enemyFightersBySpeed.OrderBy(f => f.GetUnit().CurrentHP).ToList();
    }

    public SortedList<int, Fighter> GetPlayerFightersByPosition()
    {
        //////////////Disabled.Log("FL heroes by pos COUNT: " + playerFightersByPosition.Count);
        //////////////Disabled.Log("FL heroes by pos: " + playerFightersByPosition.Values.ToList().Count);

        return playerFightersByPosition;
    }

    public SortedList<int, Fighter> GetEnemyFightersByPosition()
    {
        //////////////Disabled.Log("FL enemies by pos COUNT: " + enemyFightersByPosition.Count);
        //////////////Disabled.Log("FL enemies by pos: " + enemyFightersByPosition.Values);


        return enemyFightersByPosition;
    }

    public void ToBeRemoved(Fighter f)
    {
        toRemove.Add(f);
    }

    public List<Fighter> RemoveFighters()
    {
        foreach (Fighter f in toRemove)
        {
            if (f.tag == "PlayerTeam")
            {
                playerFightersBySpeed.Remove(f);
                playerFightersByPosition.Remove(f.GetUnit().Position);
            }
            else
            {
                enemyFightersBySpeed.Remove(f);
                enemyFightersByPosition.Remove(f.GetUnit().Position);
            }
            allFightersBySpeed.Remove(f);
        }

        // Here we checks if there is at least one ALIVE fighter per team
        if(playerFightersBySpeed.Count == 0 || !playerFightersBySpeed.Exists(f => f.fighterLogic.IsAlive()))
        {
            // The enemy won
            BattleEventSystem.current.BattleEnded(BattleState.LOST);
        }
        else if(enemyFightersBySpeed.Count == 0 || !enemyFightersBySpeed.Exists(f => f.fighterLogic.IsAlive()))
        {
            // The player won
            BattleEventSystem.current.BattleEnded(BattleState.WON);
        }

        List<Fighter> ret = toRemove;
        toRemove = new List<Fighter>();
        return ret;
    }

    public bool IsAnyoneAlive(string teamTag)
    {
        if(teamTag == "PlayerTeam")
        {
            return playerFightersBySpeed.Exists((f) => f.fighterLogic.IsAlive());
        }
        else
        {
            return enemyFightersBySpeed.Exists((f) => f.fighterLogic.IsAlive());
        }
    }

    public void SortFightersBySpeed(Modifier m)
    {
        if(m.GetType() == typeof(StatModifier) && ((StatModifierData)(m.Modifier_Data)).Statistic == StatEnum.Speed)
        {
            if (m.Target.tag == "PlayerTeam")
            {
                playerFightersBySpeed.OrderBy(f => f.GetUnit().CurrentSpeed);
            }
            else
            {
                enemyFightersBySpeed.OrderBy(f => f.GetUnit().CurrentSpeed);
            }
            allFightersBySpeed.OrderBy(f => f.GetUnit().CurrentSpeed);
        }
    }
}
