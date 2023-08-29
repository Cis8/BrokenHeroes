using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeroStatsDisplay : MonoBehaviour
{
    [SerializeField]
    Character _hero;
    [SerializeField]
    TMP_Text _maxHp;
    [SerializeField]
    TMP_Text _phyAtk;
    [SerializeField]
    TMP_Text _magAtk;
    [SerializeField]
    TMP_Text _armor;
    [SerializeField]
    TMP_Text _magDef;
    [SerializeField]
    TMP_Text _speed;

    private void Start()
    {
        HomeEventSystem.current.OnEquippedItem += RefreshStats;
        HomeEventSystem.current.OnUnequippedItem += RefreshStats;
        HomeEventSystem.current.OnHeroLevelledUp += RefreshStats;
        RefreshStats("");
    }

    private void Update()
    {

    }

    private void RefreshStats(string _)
    {
        RefreshStats();
    }

    private void RefreshStats(FighterName _)
    {
        RefreshStats();
    }

    private void RefreshStats()
    {
        UnitConverter.ConvertEquipment(_hero.Hero);
        _maxHp.text = UnitConverter.ConvertMaxHp(_hero.Hero).ToString();
        _phyAtk.text = UnitConverter.ConvertPhysicalAtk(_hero.Hero).ToString();
        _magAtk.text = UnitConverter.ConvertMagicalAtk(_hero.Hero).ToString();
        _armor.text = UnitConverter.ConvertArmor(_hero.Hero).ToString();
        _magDef.text = UnitConverter.ConvertMagicalDef(_hero.Hero).ToString();
        _speed.text = UnitConverter.ConvertSpeed(_hero.Hero).ToString();
    }

    private void OnDestroy()
    {
        HomeEventSystem.current.OnEquippedItem -= RefreshStats;
        HomeEventSystem.current.OnEquippedItem -= RefreshStats;
    }
}
