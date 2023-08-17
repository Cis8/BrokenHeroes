using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D.Animation;
using TMPro;
using System.Linq;

public class HeroSelection : MonoBehaviour
{
    GameObject heroSlotPrefab;

    private void Awake()
    {
        heroSlotPrefab = Resources.Load<GameObject>("Prefabs/HeroSelection/SlotCharacterSelect");
    }

    // Start is called before the first frame update
    void Start()
    {


    }

    public void LoadPortraits()
    {
        //GameObject heroesInventoryGO = gameObject.transform.parent.Find("Heroes").gameObject;
        gameObject.GetComponent<HeroesInventory>().opHandle.Completed += handle => {
            ////Disabled.Log("3) Starting sprites reading.");
            foreach (FighterName h in GameAssets.current.OwnedHeroes.Select(us => us.FighterName))
            {
                GameObject hs = GameObject.Instantiate(heroSlotPrefab, transform);
                hs.GetComponent<SlotCharacterSelect>().Hero = h;
                /*Sprite portraitSprite = gameObject.GetComponent<SpriteLibrary>().spriteLibraryAsset.GetSprite("Portraits", h.Name);
                hs.transform.Find("Character").Find("HeroPortrait").GetComponent<Image>().sprite = portraitSprite;
                hs.transform.Find("CharacterInfo").Find("NameTxt").GetComponent<TextMeshProUGUI>().text= portraitSprite.name;
                hs.GetComponent<SlotCharacterSelect>().Hero = h;*/
                hs.GetComponent<SlotCharacterSelect>().Initialize(h);
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
