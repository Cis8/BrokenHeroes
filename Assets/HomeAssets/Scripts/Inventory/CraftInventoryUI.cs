using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftInventoryUI : InventoryUI
{
    public override void AddItemSideEffect(FramelessItem newItem, Item item)
    {
        
    }

    public override bool ItemPredicate(List<ItemCategoryEnum> categories)
    {
        return categories.Contains(ItemCategoryEnum.Material);
    }
}
