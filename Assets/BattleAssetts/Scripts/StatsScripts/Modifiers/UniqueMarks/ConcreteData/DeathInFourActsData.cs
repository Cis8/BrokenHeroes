using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Modifiers/Unique Marks/Doombot/DeathInFourActs")]
public class DeathInFourActsData : ModifierData
{
    public override Modifier InitializeModifier(Fighter target, Fighter appliedBy)
    {
        return new DeathInFourActs(Duration, 1, this, target, appliedBy);
    }
}
