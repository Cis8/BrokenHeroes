using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Modifiers/Statistic Modifier")]
public class StatModifierData : ModifierData, ISerializationCallbackReceiver
{
    [SerializeField]
    private int amount;
    [SerializeField]
    private float floatAmount;
    [SerializeField]
    private StatEnum statistic;

    [System.NonSerialized]
    private int currentAmount;
    [System.NonSerialized]
    private float currentFloatAmount;

	public void OnAfterDeserialize()
	{
        currentAmount = amount;
        currentFloatAmount = floatAmount;
	}

	public int Amount { get => currentAmount; set => currentAmount = value; }
    public StatEnum Statistic { get => statistic; set => statistic = value; }
    public float FloatAmount { get => currentFloatAmount; set => currentFloatAmount = value; }

    public override Modifier InitializeModifier(Fighter target, Fighter appliedBy)
    {
        return new StatModifier(Duration, 1, this, target, appliedBy);
    }

    public void OnBeforeSerialize()
    {
    }
}
