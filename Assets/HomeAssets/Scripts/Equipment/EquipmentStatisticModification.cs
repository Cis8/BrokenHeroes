using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquipmentStatisticModification
{
	// The modifiers are applied before entering the battle in the following order:
	// 1) Perc modifiers
	// 2) Flat modifiers

	// Perc modifiers
	[SerializeField]
	int extraPercMaxHP;
	[SerializeField]
	int extraPercPhysicalAttack;
	[SerializeField]
	int extraPercMagicalAttack;
	[SerializeField]
	int extraPercArmorPen;
	[SerializeField]
	int extraPercMagicalDefPen;
	[SerializeField]
	int extraPercSpeed;
	[SerializeField]
	int extraPercArmor;
	[SerializeField]
	int extraPercMagicalDef;

	// Flat modifiers
	[SerializeField]
	int extraMaxHP;
	[SerializeField]
	int extraHealBonusPerc;
	[SerializeField]
	int extraPhysicalAttack;
	[SerializeField]
	int extraMagicalAttack;
	[SerializeField]
	int extraArmorPen;
	[SerializeField]
	int extraMagicalDefPen;
	[SerializeField]
	int extraSpeed;
	[SerializeField]
	int extraArmor;
	[SerializeField]
	int extraMagicalDef;
	[SerializeField]
	int extraInitialEnergy;
	[SerializeField]
	int extraEnergyPerAttack;
	[SerializeField]
	int extraAttacksPerAction;
	[SerializeField]
	int extraBleedResist;
	[SerializeField]
	int extraBurnResist;
	[SerializeField]
	int extraPoisonResist;
	[SerializeField]
	int extraThorns;
	[SerializeField]
	float extraLifesteal;
	[SerializeField]
	int extraRemainingResurrections;
	// ranges from 0 to 1. 1 means that every attack is critical.
	[SerializeField]
	float extraCriticalChance;
	[SerializeField]
	float extraCriticalMultiplier;

    public int ExtraPercMaxHP { get => extraPercMaxHP; private set => extraPercMaxHP = value; }
    public int ExtraPercPhysicalAttack { get => extraPercPhysicalAttack; private set => extraPercPhysicalAttack = value; }
    public int ExtraPercMagicalAttack { get => extraPercMagicalAttack; private set => extraPercMagicalAttack = value; }
    public int ExtraPercArmorPen { get => extraPercArmorPen; private set => extraPercArmorPen = value; }
    public int ExtraPercMagicalDefPen { get => extraPercMagicalDefPen; private set => extraPercMagicalDefPen = value; }
    public int ExtraPercSpeed { get => extraPercSpeed; private set => extraPercSpeed = value; }
    public int ExtraPercArmor { get => extraPercArmor; private set => extraPercArmor = value; }
    public int ExtraPercMagicalDef { get => extraPercMagicalDef; private set => extraPercMagicalDef = value; }
    public int ExtraMaxHP { get => extraMaxHP; private set => extraMaxHP = value; }
    public int ExtraPhysicalAttack { get => extraPhysicalAttack; private set => extraPhysicalAttack = value; }
    public int ExtraMagicalAttack { get => extraMagicalAttack; private set => extraMagicalAttack = value; }
    public int ExtraArmorPen { get => extraArmorPen; private set => extraArmorPen = value; }
    public int ExtraMagicalDefPen { get => extraMagicalDefPen; private set => extraMagicalDefPen = value; }
    public int ExtraSpeed { get => extraSpeed; private set => extraSpeed = value; }
    public int ExtraArmor { get => extraArmor; private set => extraArmor = value; }
    public int ExtraMagicalDef { get => extraMagicalDef; private set => extraMagicalDef = value; }
    public int ExtraInitialEnergy { get => extraInitialEnergy; private set => extraInitialEnergy = value; }
    public int ExtraEnergyPerAttack { get => extraEnergyPerAttack; private set => extraEnergyPerAttack = value; }
    public int ExtraAttacksPerAction { get => extraAttacksPerAction; private set => extraAttacksPerAction = value; }
    public int ExtraBleedResist { get => extraBleedResist; private set => extraBleedResist = value; }
    public int ExtraBurnResist { get => extraBurnResist; private set => extraBurnResist = value; }
    public int ExtraPoisonResist { get => extraPoisonResist; private set => extraPoisonResist = value; }
    public int ExtraThorns { get => extraThorns; private set => extraThorns = value; }
    public float ExtraLifesteal { get => extraLifesteal; private set => extraLifesteal = value; }
    public int ExtraRemainingResurrections { get => extraRemainingResurrections; private set => extraRemainingResurrections = value; }
    public float ExtraCriticalChance { get => extraCriticalChance; private set => extraCriticalChance = value; }
    public float ExtraCriticalMultiplier { get => extraCriticalMultiplier; private set => extraCriticalMultiplier = value; }
    public int ExtraHealBonusPerc { get => extraHealBonusPerc; private set => extraHealBonusPerc = value; }
}
