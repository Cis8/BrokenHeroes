using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FramelessEquippedItem : MonoBehaviour
{

    [SerializeField]
    private Image _frame;
    [SerializeField]
    private Image _image;
    [SerializeField]
    Sprite _commonFrame;
    [SerializeField]
    Sprite _rareFrame;
    [SerializeField]
    Sprite _mythicFrame;
    [SerializeField]
    Sprite _legendaryFrame;
    [SerializeField]
    Sprite _brokenFrame;
    AbstractController _controller;
    string _itemName;
    EquipmentPieceEnum _kind;

    public string Name { get => _itemName; private set => _itemName = value; }
    public AbstractController Controller { get => _controller; private set => _controller = value; }
    public EquipmentPieceEnum Kind { get => _kind; private set => _kind = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Init(Item item, AbstractController controller)
    {
        switch (item.Rarity)
        {
            case RarityEnum.Common:
                _frame.sprite = _commonFrame;
                break;
            case RarityEnum.Rare:
                _frame.sprite = _rareFrame;
                break;
            case RarityEnum.Mythic:
                _frame.sprite = _mythicFrame;
                break;
            case RarityEnum.Legendary:
                _frame.sprite = _legendaryFrame;
                break;
            case RarityEnum.Broken:
                _frame.sprite = _brokenFrame;
                break;
        }
        _image.sprite = item.Sprite;
        Controller = controller;
        _itemName = item.Name;
        _kind = ((Equipment)item).PieceKind;
    }

    public void UnequipItem()
    {
        ((EquippedItemsController)Controller).RemoveItem(Name);
    }
}
