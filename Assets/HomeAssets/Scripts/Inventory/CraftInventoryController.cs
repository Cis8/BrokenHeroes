using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class CraftInventoryController : AbstractController
{
    [SerializeField]
    CraftInventoryUI _inventoryUI;
    /*[SerializeField]
    RecipeSpec _selectedRecipe;*/ // TODO define Recipe

    public CraftInventoryUI InventoryUI { get => _inventoryUI; set => _inventoryUI = value; }
    public Dictionary<string, Item> Inventory { get => _inventory; set => _inventory = value; }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }


    // should be called when an event OnRecipeSelected is emitted and the recipe product name is different from the current one?
    public void DisableCheckOnEquipmentUI(string equipment)
    {
        _inventoryUI.ToggleItem(equipment, false);
    }

    protected override void Init()
    {
        base.Init();
        foreach (Item i in _inventory.Values)
        {
            _inventoryUI.AddItem(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
