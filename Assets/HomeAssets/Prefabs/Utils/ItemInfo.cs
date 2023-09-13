using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;


public class ItemInfo : MonoBehaviour
{
    static float fadeInTime = 0.1f;
    static float fadeOutTime = 0.05f;
    static float waitFadeInTime = 0.2f;
    static float waitFadeOutTime = 0.05f;
    bool _hidden = true;
    [SerializeField]
    CanvasGroup _renderer;
    [SerializeField]
    StatModificationsDescriptions _statModificationsDescriptions;
    [SerializeField]
    TMP_Text _itemName;
    [SerializeField]
    TMP_Text _itemCategories;
    [SerializeField]
    FramelessIcon _framelessIcon;
    [SerializeField]
    Color commonColor;
    [SerializeField]
    Color rareColor;
    [SerializeField]
    Color mythicColor;
    [SerializeField]
    Color legendaryColor;
    [SerializeField]
    Color brokenColor;
    [SerializeField]
    float remainingWaitFadeInTime = waitFadeInTime;
    float remainingWaitFadeOuttime = waitFadeOutTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Hide()
    {
        remainingWaitFadeOuttime = waitFadeOutTime;
        _hidden = true;
    }

    public void Show()
    {
        remainingWaitFadeOuttime = waitFadeInTime;
        _hidden = false;
        gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        _statModificationsDescriptions.DisableStatModificationDescriptions();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;
        if (_hidden)
        {
            remainingWaitFadeOuttime -= Time.deltaTime;
            if(remainingWaitFadeOuttime <= 0)
            {
                float newAlpha = _renderer.alpha - (Time.deltaTime / fadeOutTime);
                _renderer.alpha = newAlpha;
                if (newAlpha <= 0)
                {
                    gameObject.SetActive(false);
                }
            }
        } else
        {
            remainingWaitFadeInTime -= Time.deltaTime;
            if (remainingWaitFadeInTime <= 0)
            {
                _renderer.alpha = _renderer.alpha + (Time.deltaTime / fadeInTime);
            }
        }
    }

    public void SetupFramelessItem(Sprite icon, RarityEnum rarity, string name)
    {
        _framelessIcon.Init(icon, rarity);
        List<ItemCategoryEnum> categories = GameAssets.current.Items[name].Categories;
        if (categories.Contains(ItemCategoryEnum.Equipment)) {
            _statModificationsDescriptions.gameObject.SetActive(true);
            _statModificationsDescriptions.SetupStatisticDescriptionsOf(name);
        }
        transform.position = Input.mousePosition;
        _itemName.text = name;
        switch (rarity)
        {
            case RarityEnum.Common:
                _itemName.color = commonColor;
                break;
            case RarityEnum.Rare:
                _itemName.color = rareColor;
                break;
            case RarityEnum.Mythic:
                _itemName.color = mythicColor;
                break;
            case RarityEnum.Legendary:
                _itemName.color = legendaryColor;
                break;
            case RarityEnum.Broken:
                _itemName.color = brokenColor;
                break;
        }
        _itemCategories.text = $"{string.Join("/", categories.Select(cat => cat.ToString()))}";
    }
}
