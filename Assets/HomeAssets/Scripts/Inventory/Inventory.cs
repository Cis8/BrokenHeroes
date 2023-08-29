using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[CreateAssetMenu]
public class Inventory : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField]
    List<Item> _serializedItems = new List<Item>();

    [SerializeField]
    List<string> _strings = new List<string>();

    Dictionary<string, Item> _items;

    public Dictionary<string, Item> Items { get => _items; set => _items = value; }

    public void OnAfterDeserialize()
    {
        _items = new Dictionary<string, Item>();

        foreach (Item i in _serializedItems)
        {
            if (i != null)
            {
                _items.Add(i.Name, i);
            }
        }
    }

    public void OnBeforeSerialize()
    {
        _serializedItems = _items.Values.ToList();
    }

    public void AddItemAmount(Item item, int amount)
    {
        Items[item.Name].AddAmount(amount);
    }

    public void RemoveItemAmount(Item item, int amount)
    {
        Items[item.Name].DecreaseAmount(amount);
    }
}
