using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class LessLifeStrategy : AttackStrategy
{
    // this strategy aims at taking as targets the front enemies
    public override List<Fighter> ChooseTargets(int numberOfTargets, string attackingFighterTag)
    {
        List<Fighter> targets;
        if (attackingFighterTag == "PlayerTeam")
        {
            targets = FightersManager.current.fighters.GetEnemyFightersByLowestLife();
        }
        else
        {
            targets = FightersManager.current.fighters.GetPlayerFightersByLowestLife();
        }

        // i indicates the position of the enemy
        int i = 0;
        List<Fighter> targetsChosen = new List<Fighter>();
        while (targetsChosen.Count < numberOfTargets && i < 6)
        {
            while (!targets[i].fighterLogic.IsAlive())
            {
                //Debug.Log("target i" + targets[i].fighterLogic.ToString());
                i++;
            }

            targetsChosen.Add(targets[i]);
            i++;
        }
        return targetsChosen;
    }
}
