using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public enum ScalingTypeEnum { Physical, Magical, CurrentLife, MissingLife, MaxLife }

[CreateAssetMenu]
public class Unit : ScriptableObject, ISerializationCallbackReceiver
{
	public FighterName fighterName;

	[SerializeField]
	int lvl = 1;

	[SerializeField]
	int maxHP;

	[SerializeField]
	int initialEnergy = 0;

	[SerializeField]
	int energyPerAttack = 50;

	[SerializeField]
	int attacksPerAction = 1;

	[SerializeField]
	int physicalAttack;

	[SerializeField]
	int magicalAttack;

	[SerializeField]
	int armorPenetration;

	[SerializeField]
	int magicDefensePenetration;

	[SerializeField]
	DmgTypeEnum attackDamageType;

	[SerializeField]
	string attackSelfFormula;

	[SerializeField]
	string attackTargetFormula;

	//[SerializeField]
	//List<ScalingTypeEnum> attackScaleTypes;

	//[SerializeField]
	//List<float> attackScalingRates;

	[SerializeField]
	int numberOfTargetsBaseAttack;

	[SerializeField]
	DmgTypeEnum abilityDamageType;

	[SerializeField]
	string abilitySelfFormula;

	[SerializeField]
	string abilityTargetFormula;

	//[SerializeField]
	//List<ScalingTypeEnum> abilityScaleTypes;

	//[SerializeField]
	//List<float> abilityScalingRates;

	[SerializeField]
	int numberOfTargetsAbility;

	[SerializeField]
	int speed;

	[SerializeField]
	int armor;

	[SerializeField]
	int magicalDefense;

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
	float criticalChance;

	[SerializeField]
	float criticalMultiplier;

	public AttackStrategyEnum baseAttackStrategy;

	public AttackStrategyEnum abilityStrategyEnum;

	[System.NonSerialized] int position;


	//runtime vars
	[System.NonSerialized]
    int currentHP;
	[System.NonSerialized]
	int currentAttacksPerAction;
	[System.NonSerialized]
	int currentRemainingAttacks;
	[System.NonSerialized]
	int currentPhysicalAttack;
	[System.NonSerialized]
	int currentMagicalAttack;
	[System.NonSerialized]
	int currentArmorPenetration;
	[System.NonSerialized]
	int currentMagicDefensePenetration;
	[System.NonSerialized]
	int currentMagicalAttackBonusPerc = 0;
	[System.NonSerialized]
	int currentPhysicalAttackBonusPerc = 0;
	[System.NonSerialized]
	int currentHealBonusPerc = 0;
	[System.NonSerialized]
	int currentSpeed;
	[System.NonSerialized]
	int currentEnergy;
	[System.NonSerialized]
	int currentEnergyPerAtk;
	[System.NonSerialized]
	int currentArmor;
	[System.NonSerialized]
	int currentMagicalDefense;
	[System.NonSerialized]
	int currentBleedResist;
	[System.NonSerialized]
	int currentBurnResist;
	[System.NonSerialized]
	int currentPoisonResist;
	[System.NonSerialized]
	int currentMagicalDefenseBonusPerc = 0;
	[System.NonSerialized]
	int currentArmorBonusPerc = 0;
	[System.NonSerialized]
	float currentCriticalChance;
	[System.NonSerialized]
	float currentCriticalMultiplier;
	[System.NonSerialized]
	int currentThorns;
	[System.NonSerialized]
	float currentLifesteal;
	[System.NonSerialized]
	int currentRemainingResurrections = 0;


	public int MaxHp { get => maxHP; }

	public int CurrentEnergy { get => currentEnergy; }
	public int CurrentHP { get => currentHP; }
    public int NumberOfTargetsAbility { get => numberOfTargetsAbility; }
    public int NumberOfTargetsBaseAttack { get => numberOfTargetsBaseAttack;}
    public int CurrentMagicalAttackBonusPerc { get => currentMagicalAttackBonusPerc; }
    public int CurrentPhysicalAttackBonusPerc { get => currentPhysicalAttackBonusPerc; }
    public float CurrentLifesteal { get { if (currentLifesteal < 0f) return 0f; return currentLifesteal; } set => currentLifesteal = value; }
    public int CurrentMagicalDefenseBonusPerc { get { if (currentMagicalDefenseBonusPerc < -100) return -100; return currentMagicDefensePenetration; } set => currentMagicalDefenseBonusPerc = value; }
    public int CurrentArmorBonusPerc { get { if (currentArmorBonusPerc < -100) return -100; return currentArmorBonusPerc; } set => currentArmorBonusPerc = value; }
    public int CurrentEnergyPerAtk { get { if (currentEnergyPerAtk < 0) return 0; return currentEnergyPerAtk; } set => currentEnergyPerAtk = value; }
    public int CurrentHealBonusPerc { get { if (currentHealBonusPerc < -100) return -100; return currentHealBonusPerc; } set => currentHealBonusPerc = value; }

    //public List<float> AttackScalingRate { get => attackScalingRates; set => attackScalingRates = value; }
    //public List<ScalingTypeEnum> AttackScaleType { get => attackScaleTypes; set => attackScaleTypes = value; }
    //public List<ScalingTypeEnum> AbilityScaleTypes { get => abilityScaleTypes; set => abilityScaleTypes = value; }
    //public List<float> AbilityScalingRates { get => abilityScalingRates; set => abilityScalingRates = value; }
    public DmgTypeEnum AttackDamageType { get => attackDamageType; set => attackDamageType = value; }
    public DmgTypeEnum AbilityDamageType { get => abilityDamageType; set => abilityDamageType = value; }
    public int CurrentArmorPenetration { get { if (currentArmorPenetration < 0) return 0; return currentArmorPenetration; } set => currentArmorPenetration = value; }
    public int CurrentMagicDefensePenetration { get { if (currentMagicDefensePenetration < 0) return 0; return currentMagicDefensePenetration; } set => currentMagicDefensePenetration = value; }
    public int CurrentThorns { get { if (currentThorns < 0) return 0; return currentThorns; } set => currentThorns = value; }
    public float CurrentCriticalChance { get { if (currentCriticalChance > 1) return 1; else if (currentCriticalChance < 0) return 0; return currentCriticalChance; } set => currentCriticalChance = value; }
    public float CurrentCriticalMultiplier { get { if (currentCriticalMultiplier < 1.0f) return 1.0f; return currentCriticalMultiplier; } set => currentCriticalMultiplier = value; }
    public int Position { get => position; set => position = value; }
    public int CurrentPhysicalAttack { get => currentPhysicalAttack; set => currentPhysicalAttack = value; }
    public int CurrentMagicalAttack { get => currentMagicalAttack; set => currentMagicalAttack = value; }
    public int CurrentArmor { get { if (currentArmor < 0) return 0; return currentArmor; } set => currentArmor = value; }
    public int CurrentMagicalDefense { get { if (currentMagicalDefense < 0) return 0; return currentMagicalDefense; } set => currentMagicalDefense = value; }
    public int CurrentSpeed { get => currentSpeed; set => currentSpeed = value; }

    public int CurrentRemainingResurrections { get => currentRemainingResurrections; set => currentRemainingResurrections = value; }

	public int AD { get => GetComplessivePhysicalAtk(); }
	public int AP { get => GetComplessiveMagicalAtk(); }
	public int ARM { get => GetComplessiveArmor(); }
	public int MDEF { get => GetComplessiveMagicDefense(); }
	public int LostHp { get => MaxHp - CurrentHP; }
	public int Hp { get => CurrentHP; }
	public string AbilitySelfFormula { get => abilitySelfFormula; set => abilitySelfFormula = value; }
    public string AbilityTargetFormula { get => abilityTargetFormula; set => abilityTargetFormula = value; }
    public string AttackSelfFormula { get => attackSelfFormula; set => attackSelfFormula = value; }
    public string AttackTargetFormula { get => attackTargetFormula; set => attackTargetFormula = value; }
    public int CurrentBleedResist { get => currentBleedResist; set => currentBleedResist = value; }
    public int CurrentBurnResist { get => currentBurnResist; set => currentBurnResist = value; }
    public int CurrentPoisonResist { get => currentPoisonResist; set => currentPoisonResist = value; }
    public int AttacksPerAction { get => attacksPerAction; set => attacksPerAction = value; }
    public int CurrentAttacksPerAction { get => currentAttacksPerAction; set => currentAttacksPerAction = value; }
    public int CurrentRemainingAttacks { get => currentRemainingAttacks; set => currentRemainingAttacks = value; }

    private int GetComplessiveArmor()
    {
		return (int)((float)CurrentArmor * GetPercMultiplier(CurrentArmorBonusPerc));
	}

	private int GetComplessiveMagicDefense()
	{
		return (int)((float)CurrentMagicalDefense * GetPercMultiplier(CurrentMagicalDefenseBonusPerc));
	}

	private int GetComplessiveMagicalAtk()
    {		
		return (int)((float)currentMagicalAttack * GetPercMultiplier(CurrentMagicalAttackBonusPerc));
	}

    private int GetComplessivePhysicalAtk()
    {
        return (int)((float)currentPhysicalAttack * GetPercMultiplier(CurrentPhysicalAttackBonusPerc));
    }

	public float GetPercMultiplier(int stat, bool addHundredPercent = true)
	{
		int statistic = stat;
		if (addHundredPercent)
			statistic += 100;
		float ret = (float)(statistic) / (float)100;
		if (ret < 0f)
			return 0;
		else
			return ret;
	}

	public void OnAfterDeserialize()
	{
		Debug.Log("Unit Deserialized");
		currentHP = maxHP;
		CurrentAttacksPerAction = AttacksPerAction;
		currentRemainingAttacks = AttacksPerAction;
		CurrentPhysicalAttack = physicalAttack;
		CurrentMagicalAttack = magicalAttack;
		CurrentArmorPenetration = armorPenetration;
		CurrentMagicDefensePenetration = magicDefensePenetration;
		CurrentSpeed = speed;
		currentEnergy = initialEnergy;
		currentEnergyPerAtk = energyPerAttack;
		CurrentArmor = armor;
		CurrentMagicalDefense = magicalDefense;
		CurrentBleedResist = bleedResist;
		CurrentBurnResist = burnResist;
		CurrentPoisonResist = poisonResist;
		CurrentCriticalChance = criticalChance;
		CurrentCriticalMultiplier = criticalMultiplier;
		CurrentLifesteal = lifesteal;
		CurrentThorns = thorns;
		CurrentRemainingResurrections = remainingResurrections;
	}

	public void OnBeforeSerialize() { }

	public int SubtractLife(int amount)
    {
		currentHP -= amount;
		if (currentHP < 0)
			currentHP = 0;
		return currentHP;
    }

	public int AddLife(int amount)
	{
		currentHP += amount;
		if (currentHP > maxHP)
			currentHP = maxHP;
		return currentHP;
	}

	public void SubtractArmor(int amount)
	{
		CurrentArmor -= amount;

	}

	public void AddArmor(int amount)
    {
		CurrentArmor += amount;
    }

	public void SubtractMagicalDefense(int amount)
	{
		CurrentMagicalDefense -= amount;
		
	}

	public void AddMagicalDefense(int amount)
	{
		CurrentMagicalDefense += amount;
	}

	public void AddBleedResist(int amount)
    {
		CurrentBleedResist += amount;
    }

	public void SubtractBleedResist(int amount)
	{
		CurrentBleedResist -= amount;
	}

	public void AddBurnResist(int amount)
	{
		CurrentBurnResist += amount;
	}

	public void SubtractBurnResist(int amount)
	{
		CurrentBurnResist -= amount;
	}

	public void AddPoisonResist(int amount)
	{
		CurrentPoisonResist += amount;
	}

	public void SubtractPoisonResist(int amount)
	{
		CurrentPoisonResist -= amount;
	}

	public void SubtractMagicalDefensePenetration(int amount)
	{
		CurrentMagicDefensePenetration -= amount;

	}

	public void AddMagicalDefensePenetration(int amount)
	{
		CurrentMagicDefensePenetration += amount;
	}

	public void SubtractArmorPenetration(int amount)
	{
		CurrentArmorPenetration -= amount;

	}

	public void AddArmorPenetration(int amount)
	{
		CurrentArmorPenetration += amount;
	}

	public void SubtractMagicalDefenseBonusPerc(int amount)
    {
		currentMagicalDefenseBonusPerc -= amount;
	}

	public void AddMagicalDefenseBonusPerc(int amount)
	{
		currentMagicalDefenseBonusPerc += amount;
	}

	public void SubtractArmorBonusPerc(int amount)
	{
		currentArmorBonusPerc -= amount;
	}

	public void AddArmorBonusPerc(int amount)
	{
		currentArmorBonusPerc += amount;
	}

	public void AddHealBonusPerc(int amount)
    {
		CurrentHealBonusPerc += amount;
    }

	public void SubtractHealBonusPerc(int amount)
	{
		CurrentHealBonusPerc -= amount;
	}

	public void SubtractSpeed(int amount)
	{
		CurrentSpeed -= amount;
	}

	public void AddSpeed(int amount)
	{
		CurrentSpeed += amount;
	}

	public int SubtractPhysicalAttack(int amount)
	{
		CurrentPhysicalAttack -= amount;
		if (CurrentPhysicalAttack < 0)
			CurrentPhysicalAttack = 0;
		return CurrentPhysicalAttack;
	}

	public void AddPhysicalAttack(int amount)
	{
		CurrentPhysicalAttack += amount;
	}

	public int SubtractMagicalAttack(int amount)
	{
		CurrentMagicalAttack -= amount;
		if (CurrentMagicalAttack < 0)
			CurrentMagicalAttack = 0;
		return CurrentMagicalAttack;
	}

	public void AddMagicalAttack(int amount)
	{
		CurrentMagicalAttack += amount;
	}

	public void AddPhysicalAttackBonusPerc(int amount)
    {
		currentPhysicalAttackBonusPerc += amount;
    }

	public void AddMagicalAttackBonusPerc(int amount)
	{
		currentMagicalAttackBonusPerc += amount;
	}

	public void SubtractPhysicalAttackBonusPerc(int amount)
	{
		currentPhysicalAttackBonusPerc -= amount;
	}

	public void SubtractMagicalAttackBonusPerc(int amount)
	{
		currentMagicalAttackBonusPerc -= amount;
	}
	public int SubtractEnergy(int amount)
	{
		currentEnergy -= amount;
		if (currentEnergy < 0)
			currentEnergy = 0;
		return currentEnergy;
	}

	// returns the excess of the energy gained that can't be stored
	public int AddEnergy(int amount)
	{
		currentEnergy += amount;
		int extraEnergy = 0;
		if (currentEnergy > 100)
        {
			extraEnergy = currentEnergy - 100;
			currentEnergy = 100;
		}
		return extraEnergy;
	}

	public void ZeroEnergy()
    {
		currentEnergy = 0;
    }

	public void AddCriticalChance(float amount)
    {
		CurrentCriticalChance += amount;
	}

	public void SubtractCriticalChance(float amount)
	{
		CurrentCriticalChance -= amount;
	}
	public void AddCriticalMultiplier(float amount)
	{
		CurrentCriticalMultiplier += amount;
	}

	public void SubtractCriticalMultiplier(float amount)
	{
		CurrentCriticalMultiplier -= amount;
	}

	public void AddLifesteal(float amount)
	{
		currentLifesteal += amount;
	}

	public void SubtractLifesteal(float amount)
	{
		currentLifesteal -= amount;
	}

	public void DecrementRemainingResurrections()
    {
		currentRemainingResurrections--;
    }

	public void IncrementRemainingResurrections()
    {
		currentRemainingResurrections++;
    }

	public void AddThorns(int amount)
	{
		CurrentThorns += amount;
	}

	public void SubtractThorns(int amount)
	{
		CurrentThorns -= amount;
	}

	public void AddEnergyPerAtk(int amount)
	{
		currentEnergyPerAtk += amount;
	}

	public void SubtractEnergyPerAtk(int amount)
	{
		currentEnergyPerAtk -= amount;
	}
	/*public int GetPosition()
    {
		return position;
    }
	public void SetPosition(int pos)
    {
		position = pos;
    }

	public int GetPhysicalAttack()
    {
		return currentPhysicalAttack;
    }

	public int GetMagicalAttack()
	{
		return CurrentMagicalAttack;
	}
	public int GetArmor()
	{
		return currentArmor;
	}

	public int GetMagicalDefense()
	{
		return currentMagicalDefense;
	}

	public int GetSpeed()
	{
		return currentSpeed;
	}

	public int GetLife()
	{
		return CurrentHP;
	}

	public float GetCriticalChance()
	{
		return currentCriticalChance;
	}
	public float GetCriticalMultiplier()
	{
		return currentCriticalMultiplier;
	}

	public int GetMaxLife()
    {
		return maxHP;
    }*/
}
