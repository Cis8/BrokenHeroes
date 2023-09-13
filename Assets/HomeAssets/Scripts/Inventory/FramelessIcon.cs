using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FramelessIcon : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(Sprite icon, RarityEnum rarity)
    {
        switch (rarity)
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
        _image.sprite = icon;
    }
}
