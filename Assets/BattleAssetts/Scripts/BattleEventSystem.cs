using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEventSystem : MonoBehaviour
{
    public static BattleEventSystem current;

    private void Awake()
    {
        current = this;
    }



    // FIGHTER INSTANTIATED
    public delegate void Action(Fighter f);
    public event Action OnFighterInstantiated;
    public void FighterInstantiated(Fighter f)
    {
        OnFighterInstantiated?.Invoke(f);
    }

    // FIGHTER PRE TOOK DAMAGE
    public delegate void DamageToBeTaken(Fighter f, DmgInfo info);
    public event DamageToBeTaken OnFighterAboutToTakeDamage;
    public void FighterAboutToTakeDamage(Fighter f, DmgInfo info)
    {
        OnFighterAboutToTakeDamage?.Invoke(f, info);
    }

    // FIGHTER TOOK DAMAGE
    public delegate void DamageTaken(Fighter f, DmgInfo info);
    public event DamageTaken OnFighterTookDamage;
    public void FighterTookDamage(Fighter f, DmgInfo info)
    {
        OnFighterTookDamage?.Invoke(f, info);
    }

    // FIGHTER DIED
    public event DamageTaken OnFighterDied;
    public void FighterDied(Fighter f, DmgInfo info)
    {
        OnFighterDied?.Invoke(f, info);
    }

    // FIGHTER HEALED
    public delegate void HealTaken(Fighter f, HealInfo info);
    public event HealTaken OnFighterHealed;
    public void FighterHealed(Fighter f, HealInfo info)
    {
        OnFighterHealed?.Invoke(f, info);
    }

    // MODIFIER APPLIED
    public delegate void ModifierApplied(Modifier m);
    public event ModifierApplied OnModifierApplied;
    public void ModifierHasBeenApplied(Modifier m)
    {
        OnModifierApplied?.Invoke(m);
    }

    // MODIFIER REMOVED
    public delegate void ModifierRemoved(Modifier m);
    public event ModifierRemoved OnModifierRemoved;
    public void ModifierHasBeenRemoved(Modifier m)
    {
        OnModifierRemoved?.Invoke(m);
    }

    // PLAYER TEAM DIED
    public delegate void TeamPlayerDied();
    public event TeamPlayerDied OnTeamPlayerDied;
    public void TeamPlayerHasDied()
    {
        OnTeamPlayerDied?.Invoke();
    }

    // ENEMY TEAM DIED
    public delegate void TeamEnemyDied();
    public event TeamEnemyDied OnTeamEnemyDied;
    public void TeamEnemyHasDied()
    {
        OnTeamEnemyDied?.Invoke();
    }

    // FIGHTER DIED
    public event Action OnFighterRemoved;
    public void RemovedFighter(Fighter f)
    {
        OnFighterRemoved?.Invoke(f);
    }

    // TURN STARTED
    public delegate void Turn(int t);
    public event Turn OnTurnStarted;
    public void TurnStarted(int t)
    {
        OnTurnStarted?.Invoke(t);
    }


    // TURN ENDED
    public event Turn OnTurnEnded;
    public void TurnEnded(int t)
    {
        OnTurnEnded?.Invoke(t);
    }

    // FIGHTER TURN STARTED
    public delegate void FighterTurn(Fighter f);
    public event FighterTurn OnFighterTurnStarted;
    public void FighterTurnStarted(Fighter f)
    {
        OnFighterTurnStarted?.Invoke(f);
    }

    // FIGHTER TURN ENDED
    public event FighterTurn OnFighterTurnEnded;
    public void FighterTurnEnded(Fighter f)
    {
        OnFighterTurnEnded?.Invoke(f);
    }

    // STATE MODIFIER APPLIED
    public delegate void StateModifier(FighterLogic.StateModifiers.StateModifierInfo info);
    public event StateModifier OnStateModifierApplied;
    public void StateModifierApplied(FighterLogic.StateModifiers.StateModifierInfo info)
    {
        OnStateModifierApplied?.Invoke(info);
    }

    // STATE MODIFIER REMOVED
    public event StateModifier OnStateModifierRemoved;
    public void StateModifierRemoved(FighterLogic.StateModifiers.StateModifierInfo info)
    {
        OnStateModifierRemoved?.Invoke(info);
    }

    public delegate void BattleEnd(BattleState endingState);
    public event BattleEnd OnBattleEnd;
    public void BattleEnded(BattleState endingState)
    {
        OnBattleEnd?.Invoke(endingState);
    }

    public delegate void FighterResurrect(Fighter f);
    public event FighterResurrect OnFighterResurrected;
    public void FighterResurrected(Fighter f)
    {
        OnFighterResurrected?.Invoke(f);
    }

    /*// ALL FIGHTERS READY
    public delegate void FightersFinishedLoading();
    public event FightersFinishedLoading OnAllFightersLoaded;
    public void AllFightersLoaded()
    {
        OnAllFightersLoaded?.Invoke();
    }*/
}
