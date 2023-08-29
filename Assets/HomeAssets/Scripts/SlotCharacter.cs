using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using UnityEngine.U2D.Animation;

public class SlotCharacter : MonoBehaviour
{

    FighterName _hero;

    public FighterName Hero { get => _hero; set => _hero = value; }

    public void Init(FighterName hero)
    {
        Hero = hero;
        gameObject.transform.Find("Character").Find("HeroPortrait").GetComponent<Image>().sprite = GameAssets.GetHeroPortrait(Hero).Result;
        UnitSpec heroSpec = GameAssets.current.GetHeroUnitSpec(hero);
        GameAssets.GetClassIcon(heroSpec.Class).Completed += handle =>
        {
            gameObject.transform.Find("CharacterInfo").Find("RoleIcon").GetComponent<Image>().sprite = handle.Result;
        };
        /*Addressables.LoadAssetAsync<Sprite>(_heroState.Class + "Icon").Completed += handle =>
        {
            gameObject.transform.Find("CharacterInfo").Find("RoleIcon").GetComponent<Image>().sprite = handle.Result;
        };*/
        gameObject.transform.Find("CharacterInfo").Find("NameTxt").GetComponent<TextMeshProUGUI>().text = Hero.ToString();
        gameObject.transform.Find("CharacterInfo").Find("LvlTxt").GetComponent<TextMeshProUGUI>().text = heroSpec.Lvl.ToString();

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
