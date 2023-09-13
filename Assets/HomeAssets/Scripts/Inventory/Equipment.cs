using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentPieceEnum { Weapon, Helmet, Chestplate, Gloves, Boots }

[CreateAssetMenu(menuName = "Items/Equipment")]
public class Equipment : Item
{
    [SerializeField]
    private EquipmentPieceEnum _pieceKind;
    [SerializeField]
    List<FighterName> _heroesEquippedWithThisItem = new List<FighterName>();
    [SerializeField]
    private EquipmentStatisticModifications _statisticsModifications;
    [SerializeField]
    private List<string> _passiveAbilitiesNames; // with reflection?


    public EquipmentStatisticModifications StatisticsModifications { get => _statisticsModifications; private set => _statisticsModifications = value; }
    public List<string> PassiveAbilitiesNames { get => _passiveAbilitiesNames; private set => _passiveAbilitiesNames = value; }
    public EquipmentPieceEnum PieceKind { get => _pieceKind; private set => _pieceKind = value; }
    //[SerializeField] TODO Add Reciepe
    //private Reciepe _reciepe;

    void Reset()
    {
        Categories = new List<ItemCategoryEnum>();
        Categories.Add(ItemCategoryEnum.Equipment);
    }

    private bool IsEquippable()
    {
        return _heroesEquippedWithThisItem.Count < Amount;
    }

    public void EquipTo(UnitSpec hero)
    {
        if (IsEquippable() && !_heroesEquippedWithThisItem.Contains(hero.FighterName))
        {
            _heroesEquippedWithThisItem.Add(hero.FighterName);
            hero.EquipItem(PieceKind, Name);
            HomeEventSystem.current.ItemHasBeenEquipped(Name);
        }
    }

    public void UnequipFrom(UnitSpec hero)
    {
        if (_heroesEquippedWithThisItem.Contains(hero.FighterName))
        {
            _heroesEquippedWithThisItem.Remove(hero.FighterName);
            hero.UnequipItem(_pieceKind);
            HomeEventSystem.current.ItemHasBeenUnequipped(Name);
        }
    }
}
