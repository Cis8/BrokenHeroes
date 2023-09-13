using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class HeroInventoryController : AbstractController
{
    [SerializeField]
    HeroInventoryUI _inventoryUI;
    [SerializeField]
    Character _hero;

    public HeroInventoryUI InventoryUI { get => _inventoryUI; set => _inventoryUI = value; }
    public Dictionary<string, Item> Inventory { get => _inventory; set => _inventory = value; }

    // Start is called before the first frame update
    void Start()
    {
        HomeEventSystem.current.OnEquippedItem += EnableCheckOnEquipmentUI;
        HomeEventSystem.current.OnUnequippedItem += DisableCheckOnEquipmentUI;
        HomeEventSystem.current.OnHeroEquipPanelInitialized += CheckTogglesOnHeroLoad;
        Init();
    }

    public void EquipItem(string equipment)
    {
        if(_inventory[equipment] is Equipment)
        {
            ((Equipment)_inventory[equipment]).EquipTo(_hero.Hero);
        }
    }

    public void UnquipItem(string equipment)
    {
        if (_inventory[equipment] is Equipment)
        {
            ((Equipment)_inventory[equipment]).UnequipFrom(_hero.Hero);
        }
    }

    private void EnableCheckOnEquipmentUI(string equipment)
    {
        _inventoryUI.ToggleItem(equipment, true);
    }

    private void DisableCheckOnEquipmentUI(string equipment)
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

    private void CheckTogglesOnHeroLoad() {
        _inventoryUI.UpdateToggles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
