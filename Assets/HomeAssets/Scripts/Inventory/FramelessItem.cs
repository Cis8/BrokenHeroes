using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FramelessItem : MonoBehaviour
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
    [SerializeField]
    private AmountFramelessItem _text;
    [SerializeField]
    GameObject _selected;

    RarityEnum rarity;

    public string Name { get => _itemName; private set => _itemName = value; }
    public AbstractController Controller { get => _controller; private set => _controller = value; }
    public Image Image { get => _image; set => _image = value; }
    public Image Frame { get => _frame; set => _frame = value; }
    public RarityEnum Rarity { get => rarity; set => rarity = value; }

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
        Rarity = item.Rarity;
        switch (Rarity)
        {
            case RarityEnum.Common:
                Frame.sprite = _commonFrame;
                break;
            case RarityEnum.Rare:
                Frame.sprite = _rareFrame;
                break;
            case RarityEnum.Mythic:
                Frame.sprite = _mythicFrame;
                break;
            case RarityEnum.Legendary:
                Frame.sprite = _legendaryFrame;
                break;
            case RarityEnum.Broken:
                Frame.sprite = _brokenFrame;
                break;
        }
        Image.sprite = item.Sprite;
        Controller = controller;
        _itemName = item.Name;
        SetAmount(item.Amount);
    }

    public void SetAmount(int amount)
    {
        if (amount <= 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
            if(_text != null)
                _text.SetAmount(amount);
        }
    }

    public void ToggleCheck()
    {
        _selected.SetActive(!_selected.activeSelf);
    }

    public void ActivateCheck()
    {
        _selected.SetActive(true);
    }

    public void DisableCheck()
    {
        _selected.SetActive(false);
    }
}
