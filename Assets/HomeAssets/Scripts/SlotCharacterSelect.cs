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
    HeroState _heroState;

    public HeroState Hero { get => _heroState; set => _heroState = value; }

    public void Initialize(HeroState heroState)
    {
        _heroState = heroState;
        gameObject.transform.Find("Character").Find("HeroPortrait").GetComponent<Image>().sprite = GameAssets.GetHeroPortrait(_heroState.Name).Result;
        GameAssets.GetClassIcon(_heroState.Class).Completed += handle =>
        {
            gameObject.transform.Find("CharacterInfo").Find("RoleIcon").GetComponent<Image>().sprite = handle.Result;
        };
        /*Addressables.LoadAssetAsync<Sprite>(_heroState.Class + "Icon").Completed += handle =>
        {
            gameObject.transform.Find("CharacterInfo").Find("RoleIcon").GetComponent<Image>().sprite = handle.Result;
        };*/
        gameObject.transform.Find("CharacterInfo").Find("NameTxt").GetComponent<TextMeshProUGUI>().text = _heroState.Name;
        gameObject.transform.Find("CharacterInfo").Find("LvlTxt").GetComponent<TextMeshProUGUI>().text = _heroState.Level.ToString();

    }

    public void AddOrRemoveHero()
    {
        if (!checkMark.activeSelf)
            HomeEventSystem.current.ChosenHeroToAdd(Hero.Name);
        else
            HomeEventSystem.current.ChosenHeroToRemove(Hero.Name);
    }

    private void ToggleCheckMark(string hero)
    {
        if(hero == Hero.Name)
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
