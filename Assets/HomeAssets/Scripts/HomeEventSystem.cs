using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeEventSystem : MonoBehaviour
{
    /*private static HomeEventSystem _current;

    public static HomeEventSystem current
    {
        get
        {
            if (_current == null) _current = (Instantiate(Resources.Load("HomeEventSystem")) as GameObject).GetComponent<HomeEventSystem>();
            return _current;
        }
    }*/

    public static HomeEventSystem current;

    private void Awake()
    {
        current = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // HERO FROM INVENTORY ADDED TO CHOSEN ONES
    public delegate void HeroToAdd(FighterName hero);
    public event HeroToAdd OnHeroChosenToAdd;
    public void ChosenHeroToAdd(FighterName hero)
    {
        OnHeroChosenToAdd?.Invoke(hero);
    }

    // HERO TO BE REMOVED FROM the CHOSEN HEROES
    public delegate void HeroToRemove(FighterName hero);
    public event HeroToRemove OnHeroChosenToRemove;
    public void ChosenHeroToRemove(FighterName hero)
    {
        OnHeroChosenToRemove?.Invoke(hero);
    }

    // -------------- HERO PANEL ---------------
    public delegate void SelectedHeroFromInventory(FighterName hero);
    public event SelectedHeroFromInventory OnHeroSelectedHeroFromInventory;
    public void HeroSelectedHeroFromInventory(FighterName hero)
    {
        OnHeroSelectedHeroFromInventory?.Invoke(hero);
    }

    public delegate void HeroEquipPanelInitialized();
    public event HeroEquipPanelInitialized OnHeroEquipPanelInitialized;
    public void HeroEquipPanelHasBeenInitialized()
    {
        OnHeroEquipPanelInitialized?.Invoke();
    }

    public delegate void EquippedItem(string equipment);
    public event EquippedItem OnEquippedItem;
    public void ItemHasBeenEquipped(string equipment)
    {
        OnEquippedItem?.Invoke(equipment);
    }

    public delegate void UnequippedItem(string equipment);
    public event UnequippedItem OnUnequippedItem;
    public void ItemHasBeenUnequipped(string equipment)
    {
        OnUnequippedItem?.Invoke(equipment);
    }

    public delegate void HeroLevelledUp(FighterName hero);
    public event HeroLevelledUp OnHeroLevelledUp;
    public void HeroHasLevelledUp(FighterName hero)
    {
        OnHeroLevelledUp?.Invoke(hero);
    }

    // -------------- PANELS TOGGLING ---------------
    public delegate void PanelToToggle(string panelObjectName);
    public event PanelToToggle OnPanelToBeToggled;
    public void PanelMustBeToggled(string panelObjectName)
    {
        OnPanelToBeToggled?.Invoke(panelObjectName);
    }
}
