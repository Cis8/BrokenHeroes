using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddButton : MonoBehaviour
{
    public void ChosenHeroToAdd()
    {
        //////Disabled.Log("ChosenHeroToAdd");
        HomeEventSystem.current.ChosenHeroToAdd(gameObject.transform.parent.GetComponent<HeroSlot>().HeroName);
    }
}
