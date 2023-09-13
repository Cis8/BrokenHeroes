using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathInFourActs : Modifier
{
    public DeathInFourActs(int duration, int stacks, DeathInFourActsData modifierData, Fighter target, Fighter appliedBy) : base(duration, stacks, modifierData, target, appliedBy)
    {
        modifierName = "DeathInFourActs";
    }

    public override void End()
    {
        
    }

    protected override void ApplyEffect(bool firstApplication)
    {
        
    }
}
