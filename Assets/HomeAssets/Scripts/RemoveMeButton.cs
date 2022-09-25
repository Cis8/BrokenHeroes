using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoveMeButton : MonoBehaviour
{
    public void ChosenHeroToRemove()
    {
        HomeEventSystem.current.ChosenHeroToRemove(gameObject.transform.parent.GetComponent<ChosenHeroSlot>().ChosenHeroName);
    }
}
