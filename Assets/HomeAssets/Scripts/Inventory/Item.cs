using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ItemCategoryEnum { Material, Equipment }
public enum RarityEnum { Common, Rare, Mythic, Legendary, Broken }

[CreateAssetMenu(menuName = "Items/Item")]
public class Item : ScriptableObject
{
    [SerializeField]
    private string _name;
    [SerializeField]
    private int _amount = 0;
    [SerializeField]
    private Sprite _sprite;
    [SerializeField]
    private RarityEnum _rarity;
    [SerializeField]
    private List<ItemCategoryEnum> _categories;
    [SerializeField]
    private int _sellValue;

    public string Name { get => _name; set => _name = value; }
    public int Amount { get => _amount; private set => _amount = value; }
    public Sprite Sprite { get => _sprite; set => _sprite = value; }
    public RarityEnum Rarity { get => _rarity; set => _rarity = value; }
    public List<ItemCategoryEnum> Categories { get => _categories; set => _categories = value; }
    public int SellValue { get => _sellValue; set => _sellValue = value; }

    public void DecreaseAmount(int amount)
    {
        _amount -= Math.Abs(amount);
        if(_amount < 0)
        {
            Debug.LogException(new Exception($"Amount of { _name } item has gone < 0!"));
            new Exception($"Amount of { _name } item has gone < 0!");
            _amount = 0;
        }
    }

    public void AddAmount(int amount)
    {
        _amount += Math.Abs(amount);
    }
}
