using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class StandardStrategy : AttackStrategy
{
    // this strategy aims at taking as targets the front enemies
    public override List<Fighter> ChooseTargets(int numberOfTargets, string attackingFighterTag)
    {
        SortedList<int, Fighter> targets;
        if (attackingFighterTag == "PlayerTeam")
        {
            targets = FightersManager.current.fighters.GetEnemyFightersByPosition();
        } 
        else
        {
            targets = FightersManager.current.fighters.GetPlayerFightersByPosition();
        }

        // doesn't work if not all are alive
        /*if (numberOfTargets >= targets.Count)
        {
            // All the enemies are targeted
            return targets.Values.ToList();
        }*/

        // i indicates the position of the enemy
        int i = 0;
        List<Fighter> targetsChosen = new List<Fighter>();
        while (targetsChosen.Count < numberOfTargets && i < 6)
        {
            while ((!targets.ContainsKey(i) || !targets[i].fighterLogic.IsAlive()) && i < 6)
            {
                //Disabled.Log("target i" + targets[i].fighterLogic.ToString());
                i++;
            }
            if(i < 6)    
                targetsChosen.Add(targets[i]);
            i++;
        }
        return targetsChosen;
    }
}
