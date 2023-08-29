using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PotionsController : AbstractController
{
    [SerializeField]
    PotionSquaredItem _commonPotion;
    [SerializeField]
    PotionSquaredItem _rarePotion;
    [SerializeField]
    PotionSquaredItem _mythicPotion;
    [SerializeField]
    PotionSquaredItem _legendaryPotion;
    [SerializeField]
    PotionSquaredItem _brokenPotion;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecreaseItemData(string itemName, int amount)
    {
        _inventory[itemName].DecreaseAmount(amount);
    }

    protected override void Init()
    {
        base.Init();
        foreach (Item i in _inventory.Values.Where(it => it.Name.Contains("Exp Potion")))
        {
            switch (i.Rarity)
            {
                case RarityEnum.Common:
                    _commonPotion.SetItem((ExpPotion)i);
                    break;
                case RarityEnum.Rare:
                    _rarePotion.SetItem((ExpPotion)i);
                    break;
                case RarityEnum.Mythic:
                    _mythicPotion.SetItem((ExpPotion)i);
                    break;
                case RarityEnum.Legendary:
                    _legendaryPotion.SetItem((ExpPotion)i);
                    break;
                case RarityEnum.Broken:
                    _brokenPotion.SetItem((ExpPotion)i);
                    break;
            }
        }
    }
}
