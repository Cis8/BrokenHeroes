using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D.Animation;
using TMPro;
using System.Linq;

public class HeroSelection : MonoBehaviour
{
    [SerializeField]
    GameObject heroSlotPrefab;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        //heroSlotPrefab = Resources.Load<SlotCharacterSelect>("Prefabs/HeroSelection/SlotCharacterSelect");
    }

    public void LoadPortraits()
    {
        //GameObject heroesInventoryGO = gameObject.transform.parent.Find("Heroes").gameObject;
        gameObject.GetComponent<HeroesInventory>().opHandle.Completed += handle => {
            ////Disabled.Log("3) Starting sprites reading.");
            foreach (FighterName h in GameAssets.current.OwnedHeroes.Select(us => us.FighterName))
            {
                GameObject hs = Instantiate(heroSlotPrefab, transform);
                hs.GetComponent<SlotCharacter>().Hero = h;
                hs.GetComponent<SlotCharacter>().Init(h);
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
