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
	private bool unlocked =  false;
	[NonSerialized]
	private bool runtimeUnlocked = false;

	[SerializeField]
	private FactionEnum faction;

	[SerializeField]
	private ClassEnum @class;

	[SerializeField]
	int lvl = 1;

	[SerializeField]
	int exp = 0;
	[NonSerialized]
	private int runtimeExp;

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

	// EQUIPMENT
	[SerializeField]
	string weapon;
	[SerializeField]
	string helmet;
	[SerializeField]
	string chestplate;
	[SerializeField]
	string gloves;
	[SerializeField]
	string boots;

	public int CurrentLvlRequiredExp()
    {
		return 100 + 20 * Lvl;
	}

	// returns the hero's level after the addition of the exp
	public int AddExp(int amount)
    {
		Exp += amount;
		while (Exp >= CurrentLvlRequiredExp())
        {
			Exp -= CurrentLvlRequiredExp();
			Lvl++;
			HomeEventSystem.current.HeroHasLevelledUp(fighterName);
        }
		return Lvl;
    }

	public void EquipItem(EquipmentPieceEnum kind, string name)
    {
        switch (kind)
        {
            case EquipmentPieceEnum.Weapon:
				weapon = name;
                break;
            case EquipmentPieceEnum.Helmet:
				helmet = name;
				break;
            case EquipmentPieceEnum.Chestplate:
				chestplate = name;
				break;
            case EquipmentPieceEnum.Gloves:
				gloves = name;
				break;
            case EquipmentPieceEnum.Boots:
				boots = name;
				break;
        }
    }

    public void UnequipItem(EquipmentPieceEnum kind)
    {
		switch (kind)
		{
			case EquipmentPieceEnum.Weapon:
				weapon = "";
				break;
			case EquipmentPieceEnum.Helmet:
				helmet = "";
				break;
			case EquipmentPieceEnum.Chestplate:
				chestplate = "";
				break;
			case EquipmentPieceEnum.Gloves:
				gloves = "";
				break;
			case EquipmentPieceEnum.Boots:
				boots = "";
				break;
		}
	}



	// Getters and Setters
	public FactionEnum Faction { get => faction; set => faction = value; }
    public FighterName FighterName { get => fighterName; set => fighterName = value; }
    public int Lvl { get => lvl; set => lvl = value; }
	public int Exp { get => runtimeExp; private set => runtimeExp = value; }
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
    public bool Unlocked { get => runtimeUnlocked; set => runtimeUnlocked = value; }
    public string Weapon { get => weapon; set => weapon = value; }
    public string Helmet { get => helmet; set => helmet = value; }
    public string Chestplate { get => chestplate; set => chestplate = value; }
    public string Gloves { get => gloves; set => gloves = value; }
    public string Boots { get => boots; set => boots = value; }
}
