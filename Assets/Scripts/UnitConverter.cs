using Assets.BattleAssetts.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Linq;

public class UnitConverter
{
    [SerializeField]
    static Unit unitPrefab = Resources.Load<Unit>("Unit");
    [SerializeField]
    static Scalings statsScalings = Resources.Load<Scalings>("Scalings");


    public static Unit Convert(UnitSpec unitSpec, Transform parent)
    {

        int l = unitSpec.Lvl;
        ClassEnum c = unitSpec.Class;
        Unit u = GameObject.Instantiate<Unit>(unitPrefab, parent);
        u.Init(unitSpec.FighterName,
            unitSpec.Faction,
            unitSpec.Lvl,
            ConvertMaxHp(unitSpec),
            0,
            unitSpec.AttacksPerAction,
            unitSpec.EnergyPerAttack,
            ConvertPhysicalAtk(unitSpec),
            ConvertMagicalAtk(unitSpec),
            ConvertArmor(unitSpec),
            ConvertMagicalDef(unitSpec),
            ConvertArmorPenetration(unitSpec),
            ConvertMagicDefPenetration(unitSpec),
            unitSpec.AttackSpecification,
            unitSpec.AbilitySpecification,
            ConvertSpeed(unitSpec),
            unitSpec.BleedResist,
            unitSpec.BurnResist,
            unitSpec.PoisonResist,
            unitSpec.Thorns,
            unitSpec.Lifesteal,
            unitSpec.RemainingResurrections,
            unitSpec.CriticalChance,
            unitSpec.CriticalMultiplier);
            return u;
    }

    static int ConvertMaxHp(UnitSpec unitSpec)
    {
        return CalcEngineUtil.Int32StatScalingCalculator(statsScalings.dataArray.Where(elem => elem.Class_Name == unitSpec.Class.ToString()).FirstOrDefault().Max_Hp, unitSpec);
    }

    static int ConvertPhysicalAtk(UnitSpec unitSpec)
    {
        return CalcEngineUtil.Int32StatScalingCalculator(statsScalings.dataArray.Where(elem => elem.Class_Name == unitSpec.Class.ToString()).FirstOrDefault().Phy_Atk, unitSpec);
    }
    static int ConvertMagicalAtk(UnitSpec unitSpec)
    {
        return CalcEngineUtil.Int32StatScalingCalculator(statsScalings.dataArray.Where(elem => elem.Class_Name == unitSpec.Class.ToString()).FirstOrDefault().Mag_Atk, unitSpec);
    }
    static int ConvertArmorPenetration(UnitSpec unitSpec)
    {
        return CalcEngineUtil.Int32StatScalingCalculator(statsScalings.dataArray.Where(elem => elem.Class_Name == unitSpec.Class.ToString()).FirstOrDefault().Armor_Pen, unitSpec);
    }
    static int ConvertMagicDefPenetration(UnitSpec unitSpec)
    {
        return CalcEngineUtil.Int32StatScalingCalculator(statsScalings.dataArray.Where(elem => elem.Class_Name == unitSpec.Class.ToString()).FirstOrDefault().Mdef_Pen, unitSpec);
    }
    static int ConvertSpeed(UnitSpec unitSpec)
    {
        return CalcEngineUtil.Int32StatScalingCalculator(statsScalings.dataArray.Where(elem => elem.Class_Name == unitSpec.Class.ToString()).FirstOrDefault().Speed, unitSpec);
    }
    static int ConvertArmor(UnitSpec unitSpec)
    {
        return CalcEngineUtil.Int32StatScalingCalculator(statsScalings.dataArray.Where(elem => elem.Class_Name == unitSpec.Class.ToString()).FirstOrDefault().Arm, unitSpec);
    }

    static int ConvertMagicalDef(UnitSpec unitSpec)
    {
        return CalcEngineUtil.Int32StatScalingCalculator(statsScalings.dataArray.Where(elem => elem.Class_Name == unitSpec.Class.ToString()).FirstOrDefault().Mdef, unitSpec);
    }

    /* Use as a template
    static int ConvertStat(int level, ClassEnum @class)
    {
        return 10 + 2 * level;
    }*/
    }
