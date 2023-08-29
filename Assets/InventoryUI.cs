using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _contents;
    [SerializeField]
    private FramelessItem _framelessItemPrefab;
    [SerializeField]
    private Character _hero;
    [SerializeField]
    private HeroInventoryController _controller;
    private Dictionary<string, FramelessItem> _items;

    // Start is called before the first frame update
    void Awake()
    {
        _items = new Dictionary<string, FramelessItem>();
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        foreach (FramelessItem item in _items.Values)
        {
            item.DisableCheck();
        }
    }

    public void AddItem(Item item)
    {
        if(_items.ContainsKey(item.Name))
        {
            _items[item.Name].SetAmount(item.Amount);
        }
        else
        {
            if (item.Category == ItemCategoryEnum.Equipment)
            {
                FramelessItem newItem = Instantiate(_framelessItemPrefab, _contents.transform);
                newItem.Init(item, _controller);
                if (_hero.Hero.Weapon == item.Name
                    || _hero.Hero.Helmet == item.Name
                    || _hero.Hero.Chestplate == item.Name
                    || _hero.Hero.Gloves == item.Name
                    || _hero.Hero.Boots == item.Name)
                {
                    newItem.ToggleCheck();
                }
                _items.Add(item.Name, newItem);
            }
        }
    }

    public void UpdateToggles()
    {
        foreach (FramelessItem item in _items.Values)
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

    public void ToggleItem(string name, bool setActive)
    {
        if (setActive)
            _items[name].ActivateCheck();
        else
            _items[name].DisableCheck();
    }
}
