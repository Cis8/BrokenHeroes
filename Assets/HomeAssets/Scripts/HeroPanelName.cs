using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeroPanelName : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _heroName;

    // Start is called before the first frame update
    private void Start()
    {
        HomeEventSystem.current.OnHeroSelectedHeroFromInventory += SetupHeroName;
        // FIXME WHEN THE SELECTION OF THE HERO FROM INVENTORY FEAT IS ADDED
        SetupHeroName(FighterName.Nora);
    }

    private void SetupHeroName(FighterName hero)
    {
        _heroName.text = hero.ToString();
    }

    private void OnDestroy()
    {
        HomeEventSystem.current.OnHeroSelectedHeroFromInventory -= SetupHeroName;
    }
}
