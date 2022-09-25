using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ModifierData : ScriptableObject
{
    public enum DurationStackType { NONE, ADD, RESET }

    //public enum StackingMethodology { ADD, MULTIPLY, RESET }

    [SerializeField]
    bool isPositive;
    [SerializeField]
    int duration;
    [SerializeField]
    bool isStackable;
    // the following 3 fields will be considered just in case isStackable is true
    [SerializeField]
    DurationStackType durationStackType;
    /*[SerializeField]
    DurationStackType durationStackType;*/
    [SerializeField]
    bool isEffectStackable;
    [SerializeField]
    Sprite icon;



    public bool IsPositive { get => isPositive; }
    public DurationStackType IsDurationStackable { get => durationStackType; }
    public bool IsEffectStackable { get => isEffectStackable; }
    public bool IsStackable { get => isStackable; }
    public Sprite Icon { get => icon; set => icon = value; }
    public int Duration { get => duration; set => duration = value; }

    public abstract Modifier InitializeModifier(Fighter target, Fighter appliedBy);
}
