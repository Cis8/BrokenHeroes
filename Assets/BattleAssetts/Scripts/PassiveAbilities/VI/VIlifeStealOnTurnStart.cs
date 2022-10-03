using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VIlifeStealOnTurnStart : PassiveAbility
{
    StatModifierData lifestealOnTurnStart;
    public VIlifeStealOnTurnStart(Fighter parent, string passiveName) : base(parent, passiveName)
    {
        lifestealOnTurnStart = Resources.Load<StatModifierData>("Fighters/VI/ModifiersData/LifestealOnTurnStartVI");
        InitializeAbility();
    }

    public override void InitializeAbility()
    {
        BattleEventSystem.current.OnTurnStarted += IncrementLifestealOnTurnStart;
    }

    public override void TerminateAbility()
    {
        BattleEventSystem.current.OnTurnStarted -= IncrementLifestealOnTurnStart;
    }

    private void IncrementLifestealOnTurnStart(int turn)
    {
        lifestealOnTurnStart.InitializeModifier(Parent, Parent).Apply();
        if(turn == 1)
        {
            //Parent.fighterLogic.StateModifs.AddTerror(2, Parent);
            //Parent.fighterLogic.StateModifs.AddStun(2, Parent);
            //Parent.fighterLogic.StateModifs.AddSilence(2, Parent);
        }
    }
}
