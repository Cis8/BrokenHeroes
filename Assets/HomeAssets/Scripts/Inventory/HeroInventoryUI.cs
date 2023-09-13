using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroInventoryUI : InventoryUI
{
    [SerializeField]
    private Character _hero;


    private void OnDisable()
    {
        foreach (FramelessItem item in Items.Values)
        {
            item.DisableCheck();
        }
    }

    public override bool ItemPredicate(List<ItemCategoryEnum> categories)
    {
        return categories.Contains(ItemCategoryEnum.Equipment);
    }

    public void UpdateToggles()
    {
        foreach (FramelessItem item in Items.Values)
        {
            if (_hero.Hero.Weapon == item.Name
                || _hero.Hero.Helmet == item.Name
                || _hero.Hero.Chestplate == item.Name
                || _hero.Hero.Gloves == item.Name
                || _hero.Hero.Boots == item.Name)
            {
                item.ActivateCheck();
            }
        }
    }

    public override void AddItemSideEffect(FramelessItem newItem, Item item)
    {
        if (_hero.Hero.Weapon == item.Name
            || _hero.Hero.Helmet == item.Name
            || _hero.Hero.Chestplate == item.Name
            || _hero.Hero.Gloves == item.Name
            || _hero.Hero.Boots == item.Name)
        {
            newItem.ToggleCheck();
        }
    }
}
