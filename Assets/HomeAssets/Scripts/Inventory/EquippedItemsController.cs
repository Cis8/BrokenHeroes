using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class EquippedItemsController : AbstractController
{
    [SerializeField]
    EquipFrameEmpty _weaponEmptyFrame;
    [SerializeField]
    EquipFrameEmpty _helmetEmptyFrame;
    [SerializeField]
    EquipFrameEmpty _chestplateEmptyFrame;
    [SerializeField]
    EquipFrameEmpty _glovesEmptyFrame;
    [SerializeField]
    EquipFrameEmpty _bootsEmptyFrame;
    [SerializeField]
    FramelessEquippedItem _equipmentPrefab;
    [SerializeField]
    Character _selectedHero;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        HomeEventSystem.current.OnUnequippedItem += CheckUnequippedItem;
        HomeEventSystem.current.OnEquippedItem += EquipItem;
        HomeEventSystem.current.OnHeroEquipPanelInitialized += Init;
        Init();
    }

    private void EquipItem(string equipment)
    {
        AddItem(equipment);
    }

    private void CheckUnequippedItem(string equipment)
    {
        if (_weaponEmptyFrame.IsItemWithName(equipment))
        {
            DisableEquippedItem(_weaponEmptyFrame.ManagedEquipment);
        } else if (_helmetEmptyFrame.IsItemWithName(equipment))
        {
            DisableEquippedItem(_helmetEmptyFrame.ManagedEquipment);
        }
        else if (_chestplateEmptyFrame.IsItemWithName(equipment))
        {
            DisableEquippedItem(_chestplateEmptyFrame.ManagedEquipment);
        }
        else if (_glovesEmptyFrame.IsItemWithName(equipment))
        {
            DisableEquippedItem(_glovesEmptyFrame.ManagedEquipment);
        }
        else if (_bootsEmptyFrame.IsItemWithName(equipment))
        {
            DisableEquippedItem(_bootsEmptyFrame.ManagedEquipment);
        }
    }

    protected override void Init()
    {
        base.Init();
        AddItem(_selectedHero.Hero.Weapon);
        AddItem(_selectedHero.Hero.Helmet);
        AddItem(_selectedHero.Hero.Chestplate);
        AddItem(_selectedHero.Hero.Gloves);
        AddItem(_selectedHero.Hero.Boots);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddItem(string name)
    {
        if (name != "" && _inventory[name] is Equipment)
        {
            switch (((Equipment)_inventory[name]).PieceKind)
            {
                case EquipmentPieceEnum.Weapon:
                    _weaponEmptyFrame.SetupEquipment(_inventory[name], this);
                    break;
                case EquipmentPieceEnum.Helmet:
                    _helmetEmptyFrame.SetupEquipment(_inventory[name], this);
                    break;
                case EquipmentPieceEnum.Chestplate:
                    _chestplateEmptyFrame.SetupEquipment(_inventory[name], this);
                    break;
                case EquipmentPieceEnum.Gloves:
                    _glovesEmptyFrame.SetupEquipment(_inventory[name], this);
                    break;
                case EquipmentPieceEnum.Boots:
                    _bootsEmptyFrame.SetupEquipment(_inventory[name], this);
                    break;
            }
        }
    }

    private void DisableEquippedItem(FramelessEquippedItem item)
    {
        switch (((Equipment)_inventory[item.Name]).PieceKind)
        {
            case EquipmentPieceEnum.Weapon:
                _weaponEmptyFrame.DisableEquipment();
                break;
            case EquipmentPieceEnum.Helmet:
                _helmetEmptyFrame.DisableEquipment();
                break;
            case EquipmentPieceEnum.Chestplate:
                _chestplateEmptyFrame.DisableEquipment();
                break;
            case EquipmentPieceEnum.Gloves:
                _glovesEmptyFrame.DisableEquipment();
                break;
            case EquipmentPieceEnum.Boots:
                _bootsEmptyFrame.DisableEquipment();
                break;
        }
    }

    private void OnDisable()
    {
        if(_weaponEmptyFrame.ManagedEquipment.Name != null)
            DisableEquippedItem(_weaponEmptyFrame.ManagedEquipment);
        if (_helmetEmptyFrame.ManagedEquipment.Name != null)
            DisableEquippedItem(_helmetEmptyFrame.ManagedEquipment);
        if (_chestplateEmptyFrame.ManagedEquipment.Name != null)
            DisableEquippedItem(_chestplateEmptyFrame.ManagedEquipment);
        if (_glovesEmptyFrame.ManagedEquipment.Name != null)
            DisableEquippedItem(_glovesEmptyFrame.ManagedEquipment);
        if (_bootsEmptyFrame.ManagedEquipment.Name != null)
            DisableEquippedItem(_bootsEmptyFrame.ManagedEquipment);
    }

    public void RemoveItem(string itemName)
    {
        if (_inventory[itemName] is Equipment)
        {
            _selectedHero.Hero.UnequipItem(((Equipment)_inventory[itemName]).PieceKind);
            ((Equipment)_inventory[itemName]).UnequipFrom(_selectedHero.Hero);
        }
    }
}
