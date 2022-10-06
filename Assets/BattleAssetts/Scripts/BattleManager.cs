using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
using System;

public enum BattleState { START, FIGHTING, WON, LOST }
public class BattleManager : MonoBehaviour
{
    public static BattleManager current;

    public BattleState state;
    public int turn;

    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        BattleEventSystem.current.OnBattleEnd += EndBattle;
        BattleEventSystem.current.OnTeamPlayerDied += BattleLostByDeathOfTeamPlayer;
        BattleEventSystem.current.OnTeamEnemyDied += BattleLostByDeathOfTeamEnemy;
        BattleEventSystem.current.OnFighterDied += CheckBattleEnd;
        //BattleEventSystem.current.On
        //+= StartBattle;
        turn = 0;
        ////////////Disabled.Log("Battle now starts");
        state = BattleState.START;
        StartCoroutine(ExecuteBattle());
    }

    private void CheckBattleEnd(Fighter f, DmgInfo info)
    {
        if(f.tag == "PlayerTeam")
        {
            if (!FightersManager.current.fighters.IsAnyoneAlive(f.tag))
            {
                // The enemy won
                BattleEventSystem.current.BattleEnded(BattleState.LOST);
            }
        }
        else
        {
            if (!FightersManager.current.fighters.IsAnyoneAlive(f.tag))
            {
                // The player won
                BattleEventSystem.current.BattleEnded(BattleState.WON);
            }
        }

    }

    /*private void StartBattle()
    {
        ////////////Disabled.Log("Battle now starts");
        state = BattleState.START;
        StartCoroutine(ExecuteBattle());
    }*/

    private IEnumerator ExecuteTurn()
    {
        // Probably foreach is not suitable since if a unit gets slowed might act again. Or even in principle we are modifying the queue we are iterating over
        foreach(Fighter f in FightersManager.current.fighters.GetAllFightersBySpeed())
        {
            if(state == BattleState.FIGHTING)
            {
                BattleEventSystem.current.FighterTurnStarted(f);
                //Debug.Log("START TURN F");
                if (f.fighterLogic.IsAlive())
                {
                    yield return StartCoroutine(f.StartAttack());
                }
                else
                {
                    ////////Disabled.Log("Fighter " + f.name + " cannot attack since is dead.");
                }
                //Debug.Log("END TURN F");
                BattleEventSystem.current.FighterTurnEnded(f);
            }
            
            //////////////Disabled.Log("Fighter " + f.name + " has attacked");
        }
    }

    private IEnumerator ExecuteBattle()
    {
        state = BattleState.FIGHTING;
        yield return new WaitForSeconds(0.5f);

        while (turn < 15)
        {
            turn++;
            // TODO: send an event TurnChanged
            BattleEventSystem.current.TurnStarted(turn);
            yield return new WaitForSeconds(0.2f);
            yield return StartCoroutine(ExecuteTurn());
            yield return new WaitForSeconds(0.5f);
            BattleEventSystem.current.TurnEnded(turn);
            if (state == BattleState.WON || state == BattleState.LOST)
                break;
        }
        if(turn >= 15)
        {
            // The battle is lost
            state = BattleState.LOST;
            BattleEventSystem.current.BattleEnded(state);
        }
    }

    void BattleLostByDeathOfTeamPlayer()
    {
        state = BattleState.LOST;
    }

    void BattleLostByDeathOfTeamEnemy()
    {
        state = BattleState.WON;
    }

    private void EndBattle(BattleState status)
    {
        state = status;
    }
}
