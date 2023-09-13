using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System.Linq;

public class StatModificationsDescriptions : MonoBehaviour
{
    [SerializeField]
    StatModificationDescription maxHp;
    [SerializeField]
    StatModificationDescription maxPercHP;
    [SerializeField]
    StatModificationDescription phyAtk;
    [SerializeField]
    StatModificationDescription phyPercAtk;
    [SerializeField]
    StatModificationDescription magAtk;
    [SerializeField]
    StatModificationDescription magPercAtk;
    [SerializeField]
    StatModificationDescription armor;
    [SerializeField]
    StatModificationDescription armorPerc;
    [SerializeField]
    StatModificationDescription magicalDef;
    [SerializeField]
    StatModificationDescription magicalDefPerc;
    [SerializeField]
    StatModificationDescription armorPen;
    [SerializeField]
    StatModificationDescription armorPercPen;
    [SerializeField]
    StatModificationDescription magicalDefPen;
    [SerializeField]
    StatModificationDescription magicalDefPercPen;
    [SerializeField]
    StatModificationDescription speed;
    [SerializeField]
    StatModificationDescription speedPerc;
    [SerializeField]
    StatModificationDescription bleedResist;
    [SerializeField]
    StatModificationDescription burnResist;
    [SerializeField]
    StatModificationDescription poisonResist;
    [SerializeField]
    StatModificationDescription healBonusPerc;
    [SerializeField]
    StatModificationDescription critChance;
    [SerializeField]
    StatModificationDescription critDmg;
    [SerializeField]
    StatModificationDescription lifesteal;
    [SerializeField]
    StatModificationDescription energyPerAtk;
    [SerializeField]
    StatModificationDescription atksPerAction;
    [SerializeField]
    StatModificationDescription initialEnergy;
    [SerializeField]
    StatModificationDescription remainingResurrections;
    [SerializeField]
    StatModificationDescription thorns;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupStatisticDescriptionsOf(string itemName)
    {
        EquipmentStatisticModifications modifications = ((Equipment)GameAssets.current.Items[itemName]).StatisticsModifications;
        if (modifications.ExtraMaxHP != 0)
        {
            maxHp.gameObject.SetActive(true);
            maxHp.SetupText("Max HP", modifications.ExtraMaxHP, false);
        }
        else {
            maxHp.gameObject.SetActive(false);
        }
        if (modifications.ExtraPercMaxHP != 0)
        {
            maxPercHP.gameObject.SetActive(true);
            maxPercHP.SetupText("Max HP %", modifications.ExtraPercMaxHP, true);
        }
        else {
            maxPercHP.gameObject.SetActive(false);
        }
        if (modifications.ExtraPhysicalAttack != 0)
        {
            phyAtk.gameObject.SetActive(true);
            phyAtk.SetupText("Physical Atk", modifications.ExtraPhysicalAttack, false);
        }
        else {
            phyAtk.gameObject.SetActive(false);
        }
        if (modifications.ExtraPercPhysicalAttack != 0)
        {
            phyPercAtk.gameObject.SetActive(true);
            phyPercAtk.SetupText("Physical Atk %", modifications.ExtraPercPhysicalAttack, true);
        }
        else {
            phyPercAtk.gameObject.SetActive(false);
        }
        if (modifications.ExtraMagicalAttack != 0)
        {
            magAtk.gameObject.SetActive(true);
            magAtk.SetupText("Magical Atk", modifications.ExtraMagicalAttack, false);
        }
        else {
            magAtk.gameObject.SetActive(false);
        }
        if (modifications.ExtraPercMagicalAttack != 0)
        {
            magPercAtk.gameObject.SetActive(true);
            magPercAtk.SetupText("Magical Atk %", modifications.ExtraPercMagicalAttack, true);
        }
        else {
            magPercAtk.gameObject.SetActive(false);
        }
        if (modifications.ExtraArmor != 0)
        {
            armor.gameObject.SetActive(true);
            armor.SetupText("Armor", modifications.ExtraArmor, false);
        }
        else {
            armor.gameObject.SetActive(false);
        }
        if (modifications.ExtraPercArmor != 0)
        {
            armorPerc.gameObject.SetActive(true);
            armorPerc.SetupText("Armor %", modifications.ExtraPercArmor, true);
        }
        else {
            armorPerc.gameObject.SetActive(false);
        }
        if (modifications.ExtraMagicalDef != 0)
        {
            magicalDef.gameObject.SetActive(true);
            magicalDef.SetupText("Magical Def", modifications.ExtraMagicalDef, false);
        }
        else {
            magicalDef.gameObject.SetActive(false);
        }
        if (modifications.ExtraPercMagicalDef != 0)
        {
            magicalDefPerc.gameObject.SetActive(true);
            magicalDefPerc.SetupText("Magical Def %", modifications.ExtraPercMagicalDef, true);
        }
        else {
            magicalDefPerc.gameObject.SetActive(false);
        }
        if (modifications.ExtraArmorPen != 0)
        {
            armorPen.gameObject.SetActive(true);
            armorPen.SetupText("Armor Penetration", modifications.ExtraArmorPen, false);
        }
        else {
            armorPen.gameObject.SetActive(false);
        }
        if (modifications.ExtraPercArmorPen != 0)
        {
            armorPercPen.gameObject.SetActive(true);
            armorPercPen.SetupText("Armomr Penetration %", modifications.ExtraPercArmorPen, true);
        }
        else {
            armorPercPen.gameObject.SetActive(false);
        }
        if (modifications.ExtraMagicalDefPen != 0)
        {
            magicalDefPen.gameObject.SetActive(true);
            magicalDefPen.SetupText("Magical Def Penetration", modifications.ExtraMagicalDefPen, false);
        }
        else {
            magicalDefPen.gameObject.SetActive(false);
        }
        if (modifications.ExtraPercMagicalDefPen != 0)
        {
            magicalDefPercPen.gameObject.SetActive(true);
            magicalDefPercPen.SetupText("Magical Def Penetration %", modifications.ExtraPercMagicalDefPen, true);
        }
        else {
            magicalDefPercPen.gameObject.SetActive(false);
        }
        if (modifications.ExtraSpeed != 0)
        {
            speed.gameObject.SetActive(true);
            speed.SetupText("Speed", modifications.ExtraSpeed, false);
        }
        else {
            speed.gameObject.SetActive(false);
        }
        if (modifications.ExtraPercSpeed != 0)
        {
            speedPerc.gameObject.SetActive(true);
            speedPerc.SetupText("Speed %", modifications.ExtraPercSpeed, true);
        }
        else {
            speedPerc.gameObject.SetActive(false);
        }
        if (modifications.ExtraBleedResist != 0)
        {
            bleedResist.gameObject.SetActive(true);
            bleedResist.SetupText("Bleed Resist", modifications.ExtraBleedResist, false);
        }
        else {
            bleedResist.gameObject.SetActive(false);
        }
        if (modifications.ExtraBurnResist != 0)
        {
            burnResist.gameObject.SetActive(true);
            burnResist.SetupText("Burn Resist", modifications.ExtraBurnResist, false);
        }
        else {
            burnResist.gameObject.SetActive(false);
        }
        if (modifications.ExtraPoisonResist != 0)
        {
            poisonResist.gameObject.SetActive(true);
            poisonResist.SetupText("Poison Resist", modifications.ExtraPoisonResist, false);
        }
        else {
            poisonResist.gameObject.SetActive(false);
        }
        if (modifications.ExtraHealBonusPerc != 0)
        {
            healBonusPerc.gameObject.SetActive(true);
            healBonusPerc.SetupText("Received Healings", modifications.ExtraHealBonusPerc, true);
        }
        else {
            healBonusPerc.gameObject.SetActive(false);
        }
        if (modifications.ExtraCriticalChance != 0)
        {
            critChance.gameObject.SetActive(true);
            critChance.SetupText("Crit Chance", (int)(modifications.ExtraCriticalChance*100), true);
        }
        else {
            critChance.gameObject.SetActive(false);
        }
        if (modifications.ExtraCriticalMultiplier != 0)
        {
            critDmg.gameObject.SetActive(true);
            critDmg.SetupText("Crit Dmg", (int)(modifications.ExtraCriticalMultiplier * 100), true);
        }
        else {
            critDmg.gameObject.SetActive(false);
        }
        if (modifications.ExtraLifesteal != 0)
        {
            lifesteal.gameObject.SetActive(true);
            lifesteal.SetupText("Lifesteal", (int)(modifications.ExtraLifesteal * 100), true);
        }
        else {
            lifesteal.gameObject.SetActive(false);
        }
        if (modifications.ExtraEnergyPerAttack != 0)
        {
            energyPerAtk.gameObject.SetActive(true);
            energyPerAtk.SetupText("Energy per Atk", modifications.ExtraEnergyPerAttack, false);
        }
        else {
            energyPerAtk.gameObject.SetActive(false);
        }
        if (modifications.ExtraAttacksPerAction != 0)
        {
            atksPerAction.gameObject.SetActive(true);
            atksPerAction.SetupText("Attacks per Action", modifications.ExtraAttacksPerAction, false);
        }
        else {
            atksPerAction.gameObject.SetActive(false);
        }
        if (modifications.ExtraInitialEnergy != 0)
        {
            initialEnergy.gameObject.SetActive(true);
            initialEnergy.SetupText("Initial Energy", modifications.ExtraInitialEnergy, false);
        }
        else {
            initialEnergy.gameObject.SetActive(false);
        }
        if (modifications.ExtraRemainingResurrections != 0)
        {
            remainingResurrections.gameObject.SetActive(true);
            remainingResurrections.SetupText("Attacks per Action", modifications.ExtraRemainingResurrections, false);
        }
        else {
            remainingResurrections.gameObject.SetActive(false);
        }
        if (modifications.ExtraThorns != 0)
        {
            thorns.gameObject.SetActive(true);
            thorns.SetupText("Thorns", modifications.ExtraThorns, true);
        }
        else {
            thorns.gameObject.SetActive(false);
        }
    }

    public void DisableStatModificationDescriptions()
    {
        maxHp.gameObject.SetActive(false);
        maxPercHP.gameObject.SetActive(false);
        phyAtk.gameObject.SetActive(false);
        phyPercAtk.gameObject.SetActive(false);
        magAtk.gameObject.SetActive(false);
        magPercAtk.gameObject.SetActive(false);
        armor.gameObject.SetActive(false);
        armorPerc.gameObject.SetActive(false);
        magicalDef.gameObject.SetActive(false);
        magicalDefPerc.gameObject.SetActive(false);
        armorPen.gameObject.SetActive(false);
        armorPercPen.gameObject.SetActive(false);
        magicalDefPen.gameObject.SetActive(false);
        magicalDefPercPen.gameObject.SetActive(false);
        speed.gameObject.SetActive(false);
        speedPerc.gameObject.SetActive(false);
        bleedResist.gameObject.SetActive(false);
        burnResist.gameObject.SetActive(false);
        poisonResist.gameObject.SetActive(false);
        healBonusPerc.gameObject.SetActive(false);
        critChance.gameObject.SetActive(false);
        critDmg.gameObject.SetActive(false);
        lifesteal.gameObject.SetActive(false);
        energyPerAtk.gameObject.SetActive(false);
        atksPerAction.gameObject.SetActive(false);
        initialEnergy.gameObject.SetActive(false);
        remainingResurrections.gameObject.SetActive(false);
        thorns.gameObject.SetActive(false);
    }
}
