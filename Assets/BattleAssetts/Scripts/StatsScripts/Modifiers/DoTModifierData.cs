using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Modifiers/DoT")]
public class DoTModifierData : ModifierData
{
    public enum DoT_Type { Burn, Bleed, Poison }

    // ON_APPLICATION: the damage deald by each tick and each stack is fixed at the value calculated once the mark is applied for the first time
    // ON_TICK: the damage is calculated every time Tick() called
    // ON_STACK: the damage is calculated every time the damage for a stack is applied
    public enum DamageCalculationType { ON_APPLICATION, ON_TICK, ON_STACK}

    [SerializeField]
    DoT_Type type;

    [SerializeField]
    string baseFormula;
    [SerializeField]
    string applierFormula;
    [SerializeField]
    string targetFormula;
    [SerializeField]
    DamageCalculationType applicationType;

    public DoT_Type DotType { get => type; set => type = value; }
    public string BaseFormula { get => baseFormula; set => baseFormula = value; }
    public string ApplierFormula { get => applierFormula; set => applierFormula = value; }
    public string TargetFormula { get => targetFormula; set => targetFormula = value; }
    public DamageCalculationType ApplicationType { get => applicationType; set => applicationType = value; }

    public void OnEnable()
    {
        Addressables.LoadAssetAsync<Sprite>(type.ToString() + "Icon").Completed += handle => 
        Icon = handle.Result;
    }

    public override Modifier InitializeModifier(Fighter target, Fighter appliedBy)
    {
        return new DoTModifier(Duration, 1, this, target, appliedBy);
    }
}
