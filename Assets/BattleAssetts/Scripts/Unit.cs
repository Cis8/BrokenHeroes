using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

/*public enum FactionEnum { Soulless, Ancient, Human }
public enum ScalingTypeEnum { Physical, Magical, CurrentLife, MissingLife, MaxLife }*/

public class Unit : MonoBehaviour
{
	public FighterName fighterName;
	private FactionEnum faction;
	int level = 1;
	int maxHP;
	int initialEnergy = 0;
	int energyPerAttack = 50;
	int attacksPerAction = 1;
	int physicalAttack;
	int magicalAttack;
	int armorPenetration;
	int magicDefensePenetration;
	AtkAbilTargetSpecification atkSpec;
	AtkAbilTargetSpecification abilSpec;
	int speed;
	int armor;
	int magicalDefense;
	int bleedResist;
	int burnResist;
	int poisonResist;
	int thorns;
	float lifesteal;
	int remainingResurrections;
	// ranges from 0 to 1. 1 means that every attack is critical.
	float criticalChance;
	float criticalMultiplier;
	int position;


	//runtime vars
    int currentHP;
	int currentAttacksPerAction;
	int currentRemainingAttacks;
	int currentPhysicalAttack;
	int currentMagicalAttack;
	int currentArmorPenetration;
	int currentMagicDefensePenetration;
	int currentMagicalAttackBonusPerc = 0;
	int currentPhysicalAttackBonusPerc = 0;
	int currentHealBonusPerc = 0;
	int currentSpeed;
	int currentSpeedBonusPerc = 0;
	int currentEnergy;
	int currentEnergyPerAtk;
	int currentBonusEnergyGainedOnDamageTaken = 0;
	int currentBonusEnergyGainedFromAttack = 0;
	int currentArmor;
	int currentMagicalDefense;
	int currentBleedResist;
	int currentBurnResist;
	int currentPoisonResist;
	int currentMagicalDefenseBonusPerc = 0;
	int currentArmorBonusPerc = 0;
	float currentCriticalChance;
	float currentCriticalMultiplier;
	int currentThorns;
	float currentLifesteal;
	int currentRemainingResurrections = 0;

	public void Init(
		FighterName name,
		FactionEnum faction,
		int level,
		int maxHP,
		int initialEnergy,
		int attacksPerAction,
		int energyPerAttack,
		int physicalAtk,
		int magicalAtk,
		int armor,
		int magicalDef,
		int armorPen,
		int magicDefPen,
		AtkAbilTargetSpecification atkSpecification,
		AtkAbilTargetSpecification abilSpecification,
		int speed,
		int bleedResist,
		int burnResist,
		int poisonResist,
		int thorns,
		float lifesteal,
		int remainingResurrections,
		float criticalChance,
		float criticalMultiplier)
	{
		// TODO set all the current values
		fighterName = name;
		this.faction = faction;
		this.level = level;
		this.MaxHP = maxHP;
		this.initialEnergy = initialEnergy;
        this.AttacksPerAction = attacksPerAction;
		this.EnergyPerAttack = energyPerAttack;
		this.PhysicalAttack = physicalAtk;
		this.MagicalAttack = magicalAtk;
		this.Armor = armor;
		this.MagicalDefense = magicalDef;
		this.ArmorPenetration = armorPen;
		this.MagicDefensePenetration = magicDefPen;
		this.AtkSpec = atkSpecification;
		this.AbilSpec = abilSpecification;
		this.Speed = speed;
		this.BleedResist = bleedResist;
		this.BurnResist = burnResist;
		this.PoisonResist = poisonResist;
		this.Thorns = thorns;
		this.Lifesteal = lifesteal;
		this.RemainingResurrections = remainingResurrections;
		this.CriticalChance = criticalChance;
		this.CriticalMultiplier = criticalMultiplier;
    }

	public int MaxHP { get => maxHP; private set { maxHP = value; CurrentHP = value; } }

	public int CurrentEnergy { get => currentEnergy; }
	public int CurrentHP { get => currentHP; private set => currentHP = value; }
    public int CurrentMagicalAttackBonusPerc { get => currentMagicalAttackBonusPerc; }
    public int CurrentPhysicalAttackBonusPerc { get => currentPhysicalAttackBonusPerc; }
    public float CurrentLifesteal { get { if (currentLifesteal < 0f) return 0f; return currentLifesteal; } set => currentLifesteal = value; }
    public int CurrentMagicalDefenseBonusPerc { get { if (currentMagicalDefenseBonusPerc < -100) return -100; return currentMagicDefensePenetration; } set => currentMagicalDefenseBonusPerc = value; }
    public int CurrentArmorBonusPerc { get { if (currentArmorBonusPerc < -100) return -100; return currentArmorBonusPerc; } set => currentArmorBonusPerc = value; }
    public int CurrentEnergyPerAtk { get { if (currentEnergyPerAtk < 0) return 0; return currentEnergyPerAtk; } set => currentEnergyPerAtk = value; }
    public int CurrentHealBonusPerc { get { if (currentHealBonusPerc < -100) return -100; return currentHealBonusPerc; } set => currentHealBonusPerc = value; }
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

	public int SPEED { get => GetComplessiveSpeed(); }


    public int LostHP { get => MaxHP - CurrentHP; }
	public int Hp { get => CurrentHP; }
    public int CurrentBleedResist { get => currentBleedResist; set => currentBleedResist = value; }
    public int CurrentBurnResist { get => currentBurnResist; set => currentBurnResist = value; }
    public int CurrentPoisonResist { get => currentPoisonResist; set => currentPoisonResist = value; }
    public int AttacksPerAction { get => attacksPerAction; set { attacksPerAction = value; CurrentAttacksPerAction = value; CurrentRemainingAttacks = value; } }
    public int CurrentAttacksPerAction { get => currentAttacksPerAction; set => currentAttacksPerAction = value; }
    public int CurrentRemainingAttacks { get => currentRemainingAttacks; set => currentRemainingAttacks = value; }
    public int CurrentBonusEnergyGainedOnDamageTaken { get { if (currentBonusEnergyGainedOnDamageTaken < -100) return -100; return currentBonusEnergyGainedOnDamageTaken; } set => currentBonusEnergyGainedOnDamageTaken = value; }
    public int CurrentBonusEnergyGainedFromAttack { get { if (currentBonusEnergyGainedFromAttack < -100) return -100; return currentBonusEnergyGainedFromAttack; }  set => currentBonusEnergyGainedFromAttack = value; }

    public int CurrentSpeedBonusPerc { get { if (currentSpeedBonusPerc < -100) return -100; return currentSpeedBonusPerc; } set => currentSpeedBonusPerc = value; }

    public FactionEnum Faction { get => faction; set => faction = value; }
    public AtkAbilTargetSpecification AtkSpec { get => atkSpec; set => atkSpec = value; }
    public AtkAbilTargetSpecification AbilSpec { get => abilSpec; set => abilSpec = value; }
    public int PhysicalAttack { get => physicalAttack; set { physicalAttack = value; CurrentPhysicalAttack = value; } }

    private int MagicalAttack { get => magicalAttack; set { magicalAttack = value; CurrentMagicalAttack = value; } }

    private int Armor { get => armor; set { armor = value; CurrentArmor = value; } }
    private int MagicalDefense { get => magicalDefense; set { magicalDefense = value; CurrentMagicalDefense = value; } }

    private int ArmorPenetration { get => armorPenetration; set { armorPenetration = value; CurrentArmorPenetration = value; } }

    private int MagicDefensePenetration { get => magicDefensePenetration; set { magicDefensePenetration = value; CurrentMagicDefensePenetration = value; } }

    private int Speed { get => speed; set { speed = value; CurrentSpeed = value; } }

    private int BleedResist { get => bleedResist; set { bleedResist = value; CurrentBleedResist = value; } }
    private int BurnResist { get => burnResist; set { burnResist = value; CurrentBurnResist = value; } }
    private int PoisonResist { get => poisonResist; set { poisonResist = value; CurrentPoisonResist = value; } }

    private int Thorns { get => thorns; set { thorns = value; CurrentThorns = value; } }

    private float Lifesteal { get => lifesteal; set { lifesteal = value; CurrentLifesteal = value; } }

    private int RemainingResurrections { get => remainingResurrections; set { remainingResurrections = value; CurrentRemainingResurrections = value; } }

    private float CriticalChance { get => criticalChance; set { criticalChance = value; CurrentCriticalChance = value; } }
    private float CriticalMultiplier { get => criticalMultiplier; set { criticalMultiplier = value; CurrentCriticalMultiplier = value; } }

	private int EnergyPerAttack { get => energyPerAttack; set { energyPerAttack = value; CurrentEnergyPerAtk = value; } }

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

	private int GetComplessiveSpeed()
	{
		return (int)((float)currentSpeed * GetPercMultiplier(CurrentSpeedBonusPerc));
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
		CurrentPhysicalAttack = PhysicalAttack;
		CurrentMagicalAttack = MagicalAttack;
		CurrentArmorPenetration = ArmorPenetration;
		CurrentMagicDefensePenetration = MagicDefensePenetration;
		CurrentSpeed = Speed;
		currentEnergy = initialEnergy;
		currentEnergyPerAtk = EnergyPerAttack;
		CurrentArmor = Armor;
		CurrentMagicalDefense = MagicalDefense;
		CurrentBleedResist = BleedResist;
		CurrentBurnResist = BurnResist;
		CurrentPoisonResist = PoisonResist;
		CurrentCriticalChance = CriticalChance;
		CurrentCriticalMultiplier = CriticalMultiplier;
		CurrentLifesteal = Lifesteal;
		CurrentThorns = Thorns;
		CurrentRemainingResurrections = RemainingResurrections;
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

	public void SubtractSpeedBonusPerc(int amount)
    {
		CurrentSpeedBonusPerc -= amount;
    }

	public void AddSpeedBonusPerc(int amount)
	{
		CurrentSpeedBonusPerc += amount;
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

	public void AddBonusEnergyPerAtk(int amount)
    {
		CurrentBonusEnergyGainedFromAttack += amount;
    }

	public void SubtractBonusEnergyPerAtk(int amount)
	{
		CurrentBonusEnergyGainedFromAttack -= amount;
	}

	public void AddBonusEnergyOnDmgTaken(int amount)
	{
		CurrentBonusEnergyGainedOnDamageTaken += amount;
	}

	public void SubtractBonusEnergyOnDmgTaken(int amount)
	{
		CurrentBonusEnergyGainedOnDamageTaken -= amount;
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
