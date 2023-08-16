using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public enum FactionEnum { Soulless, Ancient, Human }

public enum ClassEnum { Warrior, Tank, Assassin, Mage, Ranger, Support }
public enum ScalingTypeEnum { Physical, Magical, CurrentLife, MissingLife, MaxLife }

[CreateAssetMenu]
public class UnitSpec : ScriptableObject
{
	[SerializeField]
    private FighterName fighterName;

    [SerializeField]
	private FactionEnum faction;

	[SerializeField]
	private ClassEnum @class;

	[SerializeField]
	int lvl = 1;

	[SerializeField]
	int energyPerAttack = 50;

	[SerializeField]
	int attacksPerAction = 1;

	[SerializeField]
	AtkAbilTargetSpecification attackSpecification;

	[SerializeField]
	AtkAbilTargetSpecification abilitySpecification;

	[SerializeField]
	int bleedResist;

	[SerializeField]
	int burnResist;

	[SerializeField]
	int poisonResist;

	[SerializeField]
	int thorns;

	[SerializeField]
	float lifesteal;

	[SerializeField]
	int remainingResurrections;

	// ranges from 0 to 1. 1 means that every attack is critical.
	[SerializeField]
	float criticalChance = 0.1f;

	[SerializeField]
	float criticalMultiplier = 2.0f;


	// Getters and Setters
	public FactionEnum Faction { get => faction; set => faction = value; }
    public FighterName FighterName { get => fighterName; set => fighterName = value; }
    public int Lvl { get => lvl; set => lvl = value; }
    public int EnergyPerAttack { get => energyPerAttack; set => energyPerAttack = value; }
    public int AttacksPerAction { get => attacksPerAction; set => attacksPerAction = value; }
    public int BleedResist { get => bleedResist; set => bleedResist = value; }
    public int BurnResist { get => burnResist; set => burnResist = value; }
    public int PoisonResist { get => poisonResist; set => poisonResist = value; }
    public int Thorns { get => thorns; set => thorns = value; }
    public float Lifesteal { get => lifesteal; set => lifesteal = value; }
    public int RemainingResurrections { get => remainingResurrections; set => remainingResurrections = value; }
    public float CriticalChance { get => criticalChance; set => criticalChance = value; }
    public float CriticalMultiplier { get => criticalMultiplier; set => criticalMultiplier = value; }
    public AtkAbilTargetSpecification AttackSpecification { get => attackSpecification; set => attackSpecification = value; }
    public AtkAbilTargetSpecification AbilitySpecification { get => abilitySpecification; set => abilitySpecification = value; }
    public ClassEnum Class { get => @class; set => @class = value; }
}
