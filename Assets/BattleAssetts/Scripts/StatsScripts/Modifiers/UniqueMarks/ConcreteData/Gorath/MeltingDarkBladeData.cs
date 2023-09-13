using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "Modifiers/Unique Marks/Gorath/Melting Dark Blade")]
public class MeltingDarkBladeData : ModifierData
{
    [SerializeField]
    GorathScalings gorathScalings;

    public GorathScalings GorathScalings { get => gorathScalings; set => gorathScalings = value; }

    public override Modifier InitializeModifier(Fighter target, Fighter appliedBy)
    {
        MeltingDarkBlade mdb = new MeltingDarkBlade(Duration, 1, this, target, appliedBy);
        return mdb;
    }

}
