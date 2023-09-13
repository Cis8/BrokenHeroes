using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatEnum { PhysicalAtk, PhysicalAtkPercentual, MagicalAtk, MagicalAtkPercentual, Armor, ArmorPercentual,
    MagicDefense, MagicDefensePercentual, Speed, SpeedBonusPerc, Lifesteal, EnergyPerAtk, CriticalChance, CriticalMultiplier,
    ArmorPenetration, MagicalDefensePenetration, BleedResist, BurnResist, PoisonResist, Thorns, EnergyBonusPerAtk, EnergyBonusOnDmgTaken};

public class StatModifier : Modifier
{
    public StatModifier(int duration, int stacks, StatModifierData modifierData, Fighter target, Fighter appliedBy) : base(duration, stacks, modifierData, target, appliedBy)
    {
    }

    public override void End()
    {
        ApplyStatisticModification(Modifier_Data.IsPositive, false);
    }

    protected override void ApplyEffect(bool firstApplication)
    {
        ApplyStatisticModification(!Modifier_Data.IsPositive, true);
    }

    void ApplyStatisticModification(bool condition, bool isBeingAdded)
    {
        int multiplier;
        if (isBeingAdded)
            multiplier = 1;
        else
            multiplier = Stacks;
        switch (((StatModifierData)Modifier_Data).Statistic)
        {
            case StatEnum.PhysicalAtk:
                if (condition)
                {
                    Target.GetUnit().SubtractPhysicalAttack(((StatModifierData)Modifier_Data).Amount * multiplier);
                }
                else
                {
                    Target.GetUnit().AddPhysicalAttack(((StatModifierData)Modifier_Data).Amount * multiplier);
                }
                break;
            case StatEnum.PhysicalAtkPercentual:
                if (condition)
                {
                    Target.GetUnit().SubtractPhysicalAttackBonusPerc(((StatModifierData)Modifier_Data).Amount * multiplier);
                }
                else
                {
                    Target.GetUnit().AddPhysicalAttackBonusPerc(((StatModifierData)Modifier_Data).Amount * multiplier);
                }
                break;
            case StatEnum.MagicalAtk:
                if (condition)
                {
                    Target.GetUnit().SubtractMagicalAttack(((StatModifierData)Modifier_Data).Amount * multiplier);
                }
                else
                {
                    Target.GetUnit().AddMagicalAttack(((StatModifierData)Modifier_Data).Amount * multiplier);
                }
                break;
            case StatEnum.MagicalAtkPercentual:
                if (condition)
                {
                    Target.GetUnit().SubtractMagicalAttackBonusPerc(((StatModifierData)Modifier_Data).Amount * multiplier);
                }
                else
                {
                    Target.GetUnit().AddMagicalAttackBonusPerc(((StatModifierData)Modifier_Data).Amount * multiplier);
                }
                break;
            case StatEnum.Armor:
                if (condition)
                {
                    Target.GetUnit().SubtractArmor(((StatModifierData)Modifier_Data).Amount * multiplier);
                }
                else
                {
                    Target.GetUnit().AddArmor(((StatModifierData)Modifier_Data).Amount * multiplier);
                }
                break;
            case StatEnum.ArmorPercentual:
                if (condition)
                {
                    Target.GetUnit().SubtractArmorBonusPerc(((StatModifierData)Modifier_Data).Amount * multiplier);
                }
                else
                {
                    Target.GetUnit().AddArmorBonusPerc(((StatModifierData)Modifier_Data).Amount * multiplier);
                }
                break;
            case StatEnum.MagicDefense:
                if (condition)
                {
                    Target.GetUnit().SubtractMagicalDefense(((StatModifierData)Modifier_Data).Amount * multiplier);
                }
                else
                {
                    Target.GetUnit().AddMagicalDefense(((StatModifierData)Modifier_Data).Amount * multiplier);
                }
                break;
            case StatEnum.MagicDefensePercentual:
                if (condition)
                {
                    Target.GetUnit().SubtractMagicalDefenseBonusPerc(((StatModifierData)Modifier_Data).Amount * multiplier);
                }
                else
                {
                    Target.GetUnit().AddMagicalDefenseBonusPerc(((StatModifierData)Modifier_Data).Amount * multiplier);
                }
                break;
            case StatEnum.Speed:
                if (condition)
                {
                    Target.GetUnit().SubtractSpeed(((StatModifierData)Modifier_Data).Amount * multiplier);
                }
                else
                {
                    Target.GetUnit().AddSpeed(((StatModifierData)Modifier_Data).Amount * multiplier);
                }
                break;
            case StatEnum.Lifesteal:
                if (condition)
                {
                    Target.GetUnit().SubtractLifesteal(((StatModifierData)Modifier_Data).FloatAmount * multiplier);
                }
                else
                {
                    Target.GetUnit().AddLifesteal(((StatModifierData)Modifier_Data).FloatAmount * multiplier);
                }
                break;
            case StatEnum.EnergyPerAtk:
                if (condition)
                {
                    Target.GetUnit().SubtractEnergyPerAtk(((StatModifierData)Modifier_Data).Amount * multiplier);
                }
                else
                {
                    Target.GetUnit().AddEnergyPerAtk(((StatModifierData)Modifier_Data).Amount * multiplier);
                }
                break;
            case StatEnum.CriticalChance:
                if (condition)
                {
                    Target.GetUnit().SubtractCriticalChance(((StatModifierData)Modifier_Data).FloatAmount * multiplier);
                }
                else
                {
                    Target.GetUnit().AddCriticalChance(((StatModifierData)Modifier_Data).FloatAmount * multiplier);
                }
                break;
            case StatEnum.CriticalMultiplier:
                if (condition)
                {
                    Target.GetUnit().SubtractCriticalMultiplier(((StatModifierData)Modifier_Data).FloatAmount * multiplier);
                }
                else
                {
                    Target.GetUnit().AddCriticalMultiplier(((StatModifierData)Modifier_Data).FloatAmount * multiplier);
                }
                break;
            case StatEnum.ArmorPenetration:
                if (condition)
                {
                    Target.GetUnit().SubtractArmorPenetration(((StatModifierData)Modifier_Data).Amount * multiplier);
                }
                else
                {
                    Target.GetUnit().AddArmorPenetration(((StatModifierData)Modifier_Data).Amount * multiplier);
                }
                break;
            case StatEnum.MagicalDefensePenetration:
                if (condition)
                {
                    Target.GetUnit().SubtractMagicalDefensePenetration(((StatModifierData)Modifier_Data).Amount * multiplier);
                }
                else
                {
                    Target.GetUnit().AddMagicalDefensePenetration(((StatModifierData)Modifier_Data).Amount * multiplier);
                }
                break;
            case StatEnum.Thorns:
                if (condition)
                {
                    Target.GetUnit().SubtractMagicalDefensePenetration(((StatModifierData)Modifier_Data).Amount * multiplier);
                }
                else
                {
                    Target.GetUnit().AddMagicalDefensePenetration(((StatModifierData)Modifier_Data).Amount * multiplier);
                }
                break;
            case StatEnum.BleedResist:
                if (condition)
                {
                    Target.GetUnit().SubtractBleedResist(((StatModifierData)Modifier_Data).Amount * multiplier);
                }
                else
                {
                    Target.GetUnit().AddBleedResist(((StatModifierData)Modifier_Data).Amount * multiplier);
                }
                break;
            case StatEnum.BurnResist:
                if (condition)
                {
                    Target.GetUnit().SubtractBurnResist(((StatModifierData)Modifier_Data).Amount * multiplier);
                }
                else
                {
                    Target.GetUnit().AddBurnResist(((StatModifierData)Modifier_Data).Amount * multiplier);
                }
                break;
            case StatEnum.PoisonResist:
                if (condition)
                {
                    Target.GetUnit().SubtractPoisonResist(((StatModifierData)Modifier_Data).Amount * multiplier);
                }
                else
                {
                    Target.GetUnit().AddPoisonResist(((StatModifierData)Modifier_Data).Amount * multiplier);
                }
                break;
            case StatEnum.EnergyBonusPerAtk:
                if (condition)
                {
                    Target.GetUnit().SubtractBonusEnergyPerAtk(((StatModifierData)Modifier_Data).Amount * multiplier);
                }
                else
                {
                    Target.GetUnit().AddBonusEnergyPerAtk(((StatModifierData)Modifier_Data).Amount * multiplier);
                }
                break;
            case StatEnum.EnergyBonusOnDmgTaken:
                if (condition)
                {
                    Target.GetUnit().SubtractBonusEnergyOnDmgTaken(((StatModifierData)Modifier_Data).Amount * multiplier);
                }
                else
                {
                    Target.GetUnit().AddBonusEnergyOnDmgTaken(((StatModifierData)Modifier_Data).Amount * multiplier);
                }
                break;
            case StatEnum.SpeedBonusPerc:
                if (condition)
                {
                    Target.GetUnit().SubtractSpeedBonusPerc(((StatModifierData)Modifier_Data).Amount * multiplier);
                }
                else
                {
                    Target.GetUnit().AddSpeedBonusPerc(((StatModifierData)Modifier_Data).Amount * multiplier);
                }
                break;
        }
    }
}
