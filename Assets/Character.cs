using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AddressableAssets;
using UnityEngine.U2D.Animation;



public class Character : MonoBehaviour
{
    private UnitSpec _hero;

    [SerializeField]
    Slider _expSlider;
    [SerializeField]
    Image _heroImage;
    [SerializeField]
    TMP_Text _expText;
    [SerializeField]
    TMP_Text _levelText;

    public UnitSpec Hero { get => _hero; private set => _hero = value; }

    private void Awake()
    {
        HomeEventSystem.current.OnHeroSelectedHeroFromInventory += SetupHero;
    }

    private void Start()
    {

    }

    private void Update()
    {
        _expSlider.maxValue = Hero.CurrentLvlRequiredExp();
        _expSlider.value = Hero.Exp;
        _levelText.text = Hero.Lvl.ToString();
        _expText.text = $"{Hero.Exp}/{Hero.CurrentLvlRequiredExp()}";
    }

    public void AddExp(int amount)
    {
        Hero.AddExp(amount);
    }

    private void SetupHero(FighterName hero)
    {
        Hero = GameAssets.current.GetHeroUnitSpec(hero);
        _expSlider.maxValue = Hero.CurrentLvlRequiredExp();
        _expSlider.value = Hero.Exp;
        Addressables.LoadAssetAsync<SpriteLibraryAsset>(hero + "SpritesLibrary").Completed += handle => { _heroImage.sprite = handle.Result.GetSprite("Stances", "idle"); };
        HomeEventSystem.current.HeroEquipPanelHasBeenInitialized();
    }

    private void OnDestroy()
    {
        HomeEventSystem.current.OnHeroSelectedHeroFromInventory -= SetupHero;
    }
}
