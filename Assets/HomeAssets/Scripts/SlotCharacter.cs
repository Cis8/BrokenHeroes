using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using UnityEngine.U2D.Animation;

public class SlotCharacter : MonoBehaviour
{
    [SerializeField]
    Image _heroPortrait;
    [SerializeField]
    Image _heroRoleIcon;
    [SerializeField]
    TextMeshProUGUI _heroNameText;
    [SerializeField]
    TextMeshProUGUI _heroLvlText;
    UnitSpec _hero;
    FighterName _heroName;

    public FighterName Name { get => _heroName; set => _heroName = value; }

    public void Init(FighterName hero)
    {
        Name = hero;
        _heroPortrait.sprite = GameAssets.GetHeroPortrait(Name).Result;
        _hero = GameAssets.current.GetHeroUnitSpec(hero);
        GameAssets.GetClassIcon(_hero.Class).Completed += handle =>
        {
            _heroRoleIcon.sprite = handle.Result;
        };
        _heroNameText.text = Name.ToString();
        UpdateLevel();
    }

    // called to refresh the level when the roster of heroes is enabled again
    public void UpdateLevel()
    {
        _heroLvlText.text = _hero.Lvl.ToString();
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
