using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class SquaredItem : MonoBehaviour
{
    [SerializeField]
    TMP_Text _amountText;
    private int _amount;
    private string _name;
    [SerializeField]
    Image _halo;
    [SerializeField]
    Image _frame;
    [SerializeField]
    Image _icon;
    private Color _zeroAmountColor = new Color(0.3f, 0.3f, 0.3f);
    private Color _standardColor = new Color(1, 1, 1);

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
    [SerializeField]
    Sprite _commonHalo;
    [SerializeField]
    Sprite _rareHalo;
    [SerializeField]
    Sprite _mythicHalo;
    [SerializeField]
    Sprite _legendaryHalo;
    [SerializeField]
    Sprite _brokenHalo;

    public int Amount { get => _amount; private set => _amount = value; }
    public string Name { get => _name; private set => _name = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetItem(Item item)
    {
        Amount = item.Amount;
        _icon.sprite = item.Sprite;
        _name = item.Name;
        SetAmount();
        switch (item.Rarity)
        {
            case RarityEnum.Common:
                _frame.sprite = _commonFrame;
                _halo.sprite = _commonHalo;
                break;
            case RarityEnum.Rare:
                _frame.sprite = _rareFrame;
                _halo.sprite = _rareHalo;

                break;
            case RarityEnum.Mythic:
                _frame.sprite = _mythicFrame;
                _halo.sprite = _mythicHalo;

                break;
            case RarityEnum.Legendary:
                _frame.sprite = _legendaryFrame;
                _halo.sprite = _legendaryHalo;

                break;
            case RarityEnum.Broken:
                _frame.sprite = _brokenFrame;
                _halo.sprite = _brokenHalo;
                break;
        }
    }

    private void SetAmount()
    {
        _amountText.text = Amount.ToString();
        if(Amount <= 0)
        {
            _icon.color = _zeroAmountColor;
        }
        else
        {
            _icon.color = _standardColor;
        }
    }

    public void DecreaseAmount(int amount)
    {
        Amount -= Mathf.Abs(amount);
        if (amount <= 0)
        {
            Amount = 0;
        }
        SetAmount();
    }
}
