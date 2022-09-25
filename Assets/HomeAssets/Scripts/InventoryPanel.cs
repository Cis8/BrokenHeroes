using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D.Animation;

public class InventoryPanel : MonoBehaviour
{
    GameObject heroSlotPrefab;

    private void Awake()
    {
        heroSlotPrefab = Resources.Load<GameObject>("Prefabs/HeroSlot");
    }

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    public void LoadPortraits()
    {
        GameObject heroesInventoryGO = gameObject.transform.parent.Find("Heroes").gameObject;
        heroesInventoryGO.GetComponent<HeroesInventory>().opHandle.Completed += handle => {
            ////Disabled.Log("3) Starting sprites reading.");
            foreach (string h in heroesInventoryGO.GetComponent<HeroesInventory>().OwnedHeroes.GetHeroes())
            {
                GameObject hs = GameObject.Instantiate(heroSlotPrefab, transform);
                hs.transform.Find("HeroPortrait").GetComponent<Image>().sprite = heroesInventoryGO.GetComponent<SpriteLibrary>().spriteLibraryAsset.GetSprite("Portraits", h);
                hs.GetComponent<HeroSlot>().HeroName = h;
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
