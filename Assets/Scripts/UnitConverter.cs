using Assets.BattleAssetts.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Linq;
using System;

public class UnitConverter
{
    static Unit unitPrefab = Resources.Load<Unit>("Unit");

    static Scalings statsScalings = Resources.Load<Scalings>("Scalings");

    // Perc modifiers
    [SerializeField]
    static int extraPercMaxHP;
    [SerializeField]
    static int extraPercPhysicalAttack;
    [SerializeField]
    static int extraPercMagicalAttack;
    [SerializeField]
    static int extraPercArmorPen;
    [SerializeField]
    static int extraPercMagicalDefPen;
    [SerializeField]
    static int extraPercSpeed;
    [SerializeField]
    static int extraPercArmor;
    [SerializeField]
    static int extraPercMagicalDef;

    // Flat modifiers
    [SerializeField]
    static int extraMaxHP;
    [SerializeField]
    static int extraHealBonusPerc;
    [SerializeField]
    static int extraPhysicalAttack;
    [SerializeField]
    static int extraMagicalAttack;
    [SerializeField]
    static int extraArmorPen;
    [SerializeField]
    static int extraMagicalDefPen;
    [SerializeField]
    static int extraSpeed;
    [SerializeField]
    static int extraArmor;
    [SerializeField]
    static int extraMagicalDef;
    [SerializeField]
    static int extraInitialEnergy;
    [SerializeField]
    static int extraEnergyPerAttack;
    [SerializeField]
    static int extraAttacksPerAction;
    [SerializeField]
    static int extraBleedResist;
    [SerializeField]
    static int extraBurnResist;
    [SerializeField]
    static int extraPoisonResist;
    [SerializeField]
    static int extraThorns;
    [SerializeField]
    static float extraLifesteal;
    [SerializeField]
    static int extraRemainingResurrections;
    // ranges from 0 to 1. 1 means that every attack is critical.
    [SerializeField]
    static float extraCriticalChance;
    [SerializeField]
    static float extraCriticalMultiplier;

    public static Unit ConvertUnit(UnitSpec unitSpec, Transform parent)
    {

        int l = unitSpec.Lvl;
        ClassEnum c = unitSpec.Class;

        ConvertEquipment(unitSpec);

        // Initialize converted values
        int finalMaxHp = ConvertMaxHp(unitSpec);
        Convert.ToInt32(CalcEngineUtil.FloatStatScalingCalculator(statsScalings.dataArray.Where(elem => elem.Class_Name == unitSpec.Class.ToString()).FirstOrDefault().Max_Hp, unitSpec));

        Unit u = GameObject.Instantiate<Unit>(unitPrefab, parent);
        u.Init(unitSpec.FighterName,
            unitSpec.Faction,
            unitSpec.Lvl,
            ConvertMaxHp(unitSpec),
            ConvertBonusHealPerc(),
            extraInitialEnergy,
            ConvertAttacksPerAction(unitSpec),
            ConvertEnergyPerAttack(unitSpec),
            ConvertPhysicalAtk(unitSpec),
            ConvertMagicalAtk(unitSpec),
            ConvertArmor(unitSpec),
            ConvertMagicalDef(unitSpec),
            ConvertArmorPenetration(unitSpec),
            ConvertMagicDefPenetration(unitSpec),
            unitSpec.AttackSpecification,
            unitSpec.AbilitySpecification,
            ConvertSpeed(unitSpec),
            ConvertBleedResist(unitSpec),
            ConvertBurnResist(unitSpec),
            ConvertPoisonResist(unitSpec),
            ConvertThorns(unitSpec),
            ConvertLifesteal(unitSpec),
            ConvertRemainingResurrections(unitSpec),
            ConvertCriticalChance(unitSpec),
            ConvertCriticalMultiplier(unitSpec));
            return u;

    }

    public static void ConvertEquipment(UnitSpec unitSpec)
    {
        List<Equipment> allEquipment = GameAssets.current.Items.Values.Where(it => {
            return it.Name == unitSpec.Weapon ||
            it.Name == unitSpec.Helmet ||
            it.Name == unitSpec.Chestplate ||
            it.Name == unitSpec.Gloves ||
            it.Name == unitSpec.Boots;
        }).Cast<Equipment>().ToList();

        // Perc modifiers
        extraPercMaxHP = 0;
        extraPercPhysicalAttack = 0;
        extraPercMagicalAttack = 0;
        extraPercArmorPen = 0;
        extraPercMagicalDefPen = 0;
        extraPercSpeed = 0;
        extraPercArmor = 0;
        extraPercMagicalDef = 0;

        // Flat modifiers
        extraMaxHP = 0;
        extraHealBonusPerc = 0;
        extraPhysicalAttack = 0;
        extraMagicalAttack = 0;
        extraArmorPen = 0;
        extraMagicalDefPen = 0;
        extraSpeed = 0;
        extraArmor = 0;
        extraMagicalDef = 0;
        extraInitialEnergy = 0;
        extraEnergyPerAttack = 0;
        extraAttacksPerAction = 0;
        extraBleedResist = 0;
        extraBurnResist = 0;
        extraPoisonResist = 0;
        extraThorns = 0;
        extraLifesteal = 0f;
        extraRemainingResurrections = 0;
        extraCriticalChance = 0f;
        extraCriticalMultiplier = 0f;

        foreach (Equipment e in allEquipment)
        {
            extraPercMaxHP += e.StatisticsModifications.ExtraPercMaxHP;
            extraPercPhysicalAttack += e.StatisticsModifications.ExtraPercPhysicalAttack;
            extraPercMagicalAttack += e.StatisticsModifications.ExtraPercMagicalAttack;
            extraPercArmorPen += e.StatisticsModifications.ExtraPercArmorPen;
            extraPercMagicalDefPen += e.StatisticsModifications.ExtraPercMagicalDefPen;
            extraPercSpeed += e.StatisticsModifications.ExtraPercSpeed;
            extraPercArmor += e.StatisticsModifications.ExtraPercArmor;
            extraPercMagicalDef += e.StatisticsModifications.ExtraPercMagicalDefPen;

            extraMaxHP += e.StatisticsModifications.ExtraMaxHP;
            extraHealBonusPerc += e.StatisticsModifications.ExtraHealBonusPerc;
            extraPhysicalAttack += e.StatisticsModifications.ExtraPhysicalAttack;
            extraMagicalAttack += e.StatisticsModifications.ExtraMagicalAttack;
            extraArmorPen += e.StatisticsModifications.ExtraArmorPen;
            extraMagicalDefPen += e.StatisticsModifications.ExtraMagicalDefPen;
            extraSpeed += e.StatisticsModifications.ExtraSpeed;
            extraArmor += e.StatisticsModifications.ExtraArmor;
            extraMagicalDef += e.StatisticsModifications.ExtraMagicalDef;
            extraInitialEnergy += e.StatisticsModifications.ExtraInitialEnergy;
            extraEnergyPerAttack += e.StatisticsModifications.ExtraEnergyPerAttack;
            extraAttacksPerAction += e.StatisticsModifications.ExtraAttacksPerAction;
            extraBleedResist += e.StatisticsModifications.ExtraBleedResist;
            extraBurnResist += e.StatisticsModifications.ExtraBurnResist;
            extraPoisonResist += e.StatisticsModifications.ExtraPoisonResist;
            extraThorns += e.StatisticsModifications.ExtraThorns;
            extraLifesteal += e.StatisticsModifications.ExtraLifesteal;
            extraRemainingResurrections += e.StatisticsModifications.ExtraRemainingResurrections;
            extraCriticalChance += e.StatisticsModifications.ExtraCriticalChance;
            extraCriticalMultiplier += e.StatisticsModifications.ExtraCriticalMultiplier;
        }
    }

    public static int ConvertMaxHp(UnitSpec unitSpec)
    {
        return Convert.ToInt32((CalcEngineUtil.Int32StatScalingCalculator(statsScalings.dataArray.Where(elem => elem.Class_Name == unitSpec.Class.ToString()).FirstOrDefault().Max_Hp, unitSpec) + extraMaxHP) * Unit.GetPercMultiplier(extraPercMaxHP, true));
    }

    public static int ConvertPhysicalAtk(UnitSpec unitSpec)
    {
        return Convert.ToInt32((CalcEngineUtil.Int32StatScalingCalculator(statsScalings.dataArray.Where(elem => elem.Class_Name == unitSpec.Class.ToString()).FirstOrDefault().Phy_Atk, unitSpec) + extraPhysicalAttack) * Unit.GetPercMultiplier(extraPercPhysicalAttack, true));
    }
    public static int ConvertMagicalAtk(UnitSpec unitSpec)
    {
        return Convert.ToInt32((CalcEngineUtil.Int32StatScalingCalculator(statsScalings.dataArray.Where(elem => elem.Class_Name == unitSpec.Class.ToString()).FirstOrDefault().Mag_Atk, unitSpec) + extraMagicalAttack) * Unit.GetPercMultiplier(extraPercMagicalAttack, true));
    }
    public static int ConvertArmorPenetration(UnitSpec unitSpec)
    {
        return Convert.ToInt32((CalcEngineUtil.Int32StatScalingCalculator(statsScalings.dataArray.Where(elem => elem.Class_Name == unitSpec.Class.ToString()).FirstOrDefault().Armor_Pen, unitSpec) + extraArmorPen ) * Unit.GetPercMultiplier(extraPercArmorPen, true));
    }
    public static int ConvertMagicDefPenetration(UnitSpec unitSpec)
    {
        return Convert.ToInt32((CalcEngineUtil.Int32StatScalingCalculator(statsScalings.dataArray.Where(elem => elem.Class_Name == unitSpec.Class.ToString()).FirstOrDefault().Mdef_Pen, unitSpec) + extraMagicalDefPen ) * Unit.GetPercMultiplier(extraPercMagicalDefPen, true));
    }
    public static int ConvertSpeed(UnitSpec unitSpec)
    {
        return Convert.ToInt32((CalcEngineUtil.Int32StatScalingCalculator(statsScalings.dataArray.Where(elem => elem.Class_Name == unitSpec.Class.ToString()).FirstOrDefault().Speed, unitSpec) + extraSpeed) * Unit.GetPercMultiplier(extraPercSpeed, true));
    }
    public static int ConvertArmor(UnitSpec unitSpec)
    {
        return Convert.ToInt32((CalcEngineUtil.Int32StatScalingCalculator(statsScalings.dataArray.Where(elem => elem.Class_Name == unitSpec.Class.ToString()).FirstOrDefault().Arm, unitSpec) + extraArmor) * Unit.GetPercMultiplier(extraPercArmor, true));
    }

    public static int ConvertMagicalDef(UnitSpec unitSpec)
    {
        return Convert.ToInt32((CalcEngineUtil.Int32StatScalingCalculator(statsScalings.dataArray.Where(elem => elem.Class_Name == unitSpec.Class.ToString()).FirstOrDefault().Mdef, unitSpec) + extraMagicalDef) * Unit.GetPercMultiplier(extraPercMagicalDef, true));
    }
    
    public static int ConvertAttacksPerAction(UnitSpec unitSpec)
    {
        return unitSpec.AttacksPerAction + extraAttacksPerAction;
    }

    public static int ConvertEnergyPerAttack(UnitSpec unitSpec)
    {
        return unitSpec.EnergyPerAttack + extraEnergyPerAttack;
    }
    public static int ConvertBleedResist(UnitSpec unitSpec)
    {
        return unitSpec.BleedResist + extraBleedResist;
    }
    public static int ConvertBurnResist(UnitSpec unitSpec)
    {
        return unitSpec.BurnResist + extraBurnResist;
    }
    public static int ConvertPoisonResist(UnitSpec unitSpec)
    {
        return unitSpec.PoisonResist + extraPoisonResist;
    }

    public static int ConvertThorns(UnitSpec unitSpec)
    {
        return unitSpec.Thorns + extraThorns;
    }

    public static float ConvertCriticalMultiplier(UnitSpec unitSpec)
    {
        return unitSpec.CriticalMultiplier + extraCriticalMultiplier;
    }

    public static float ConvertCriticalChance(UnitSpec unitSpec)
    {
        return unitSpec.CriticalChance + extraCriticalChance;
    }

    public static int ConvertRemainingResurrections(UnitSpec unitSpec)
    {
        return unitSpec.RemainingResurrections + extraRemainingResurrections;
    }

    public static float ConvertLifesteal(UnitSpec unitSpec)
    {
        return unitSpec.Lifesteal + extraLifesteal;
    }

    public static int ConvertBonusHealPerc()
    {
        return extraHealBonusPerc;
    }

    /* Use as a template
    static int ConvertStat(int level, ClassEnum @class)
    {
        return 10 + 2 * level;
    }*/
}
