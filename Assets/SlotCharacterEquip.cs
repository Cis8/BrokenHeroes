using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotCharacterEquip : SlotCharacter
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectedHero()
    {
        HomeEventSystem.current.PanelMustBeToggled("HeroEquipmentPanel");
        HomeEventSystem.current.HeroSelectedHeroFromInventory(Name);
    }
}
