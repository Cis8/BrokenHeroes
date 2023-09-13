using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _contents;
    [SerializeField]
    private FramelessItem _framelessItemPrefab;
    [SerializeField]
    private AbstractController _controller;
    private Dictionary<string, FramelessItem> _items;

    public Dictionary<string, FramelessItem> Items { get => _items; set => _items = value; }
    public FramelessItem FramelessItemPrefab { get => _framelessItemPrefab; set => _framelessItemPrefab = value; }
    public GameObject Contents { get => _contents; set => _contents = value; }
    public AbstractController Controller { get => _controller; set => _controller = value; }

    private void Awake()
    {
        Items = new Dictionary<string, FramelessItem>();
    }

    public abstract bool ItemPredicate(List<ItemCategoryEnum> categories);

    public abstract void AddItemSideEffect(FramelessItem newItem, Item item);

    public void AddItem(Item item)
    {
        if (Items.ContainsKey(item.Name))
        {
            Items[item.Name].SetAmount(item.Amount);
        }
        else
        {
            if (ItemPredicate(item.Categories))
            {
                FramelessItem newItem = Instantiate(FramelessItemPrefab, Contents.transform);
                newItem.Init(item, Controller);
                AddItemSideEffect(newItem, item);
                Items.Add(item.Name, newItem);
            }
        }
    }

    public void ToggleItem(string name, bool setActive)
    {
        if (setActive)
            Items[name].ActivateCheck();
        else
            Items[name].DisableCheck();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
