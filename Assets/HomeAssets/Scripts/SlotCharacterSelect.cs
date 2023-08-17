using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D.Animation;
using TMPro;
using UnityEngine.AddressableAssets;

public class SlotCharacterSelect : MonoBehaviour
{
    [SerializeField]
    GameObject checkMark;
    FighterName _hero;

    public FighterName Hero { get => _hero; set => _hero = value; }

    public void Initialize(FighterName hero)
    {
        _hero = hero;
        gameObject.transform.Find("Character").Find("HeroPortrait").GetComponent<Image>().sprite = GameAssets.GetHeroPortrait(_hero).Result;
        UnitSpec heroSpec = GameAssets.current.GetHeroUnitSpec(hero);
        GameAssets.GetClassIcon(heroSpec.Class).Completed += handle =>
        {
            gameObject.transform.Find("CharacterInfo").Find("RoleIcon").GetComponent<Image>().sprite = handle.Result;
        };
        /*Addressables.LoadAssetAsync<Sprite>(_heroState.Class + "Icon").Completed += handle =>
        {
            gameObject.transform.Find("CharacterInfo").Find("RoleIcon").GetComponent<Image>().sprite = handle.Result;
        };*/
        gameObject.transform.Find("CharacterInfo").Find("NameTxt").GetComponent<TextMeshProUGUI>().text = _hero.ToString();
        gameObject.transform.Find("CharacterInfo").Find("LvlTxt").GetComponent<TextMeshProUGUI>().text = heroSpec.Lvl.ToString();

    }

    public void AddOrRemoveHero()
    {
        if (!checkMark.activeSelf)
            HomeEventSystem.current.ChosenHeroToAdd(Hero);
        else
            HomeEventSystem.current.ChosenHeroToRemove(Hero);
    }

    private void ToggleCheckMark(FighterName hero)
    {
        if(hero == Hero)
        {
            if (checkMark.activeSelf)
                checkMark.SetActive(false);
            else
                checkMark.SetActive(true);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        HomeEventSystem.current.OnHeroChosenToAdd += ToggleCheckMark;
        HomeEventSystem.current.OnHeroChosenToRemove += ToggleCheckMark;
    }

    private void OnDestroy()
    {
        HomeEventSystem.current.OnHeroChosenToAdd -= ToggleCheckMark;
        HomeEventSystem.current.OnHeroChosenToRemove -= ToggleCheckMark;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
