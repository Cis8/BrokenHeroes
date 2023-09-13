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
    SlotCharacter heroSlotPrefab;
    List<SlotCharacter> _heroes = new List<SlotCharacter>();
    private bool heroesLoaded = false;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        HomeEventSystem.current.OnHeroLevelledUp += UpdateLevel;
    }

    private void OnDestroy()
    {
        HomeEventSystem.current.OnHeroLevelledUp -= UpdateLevel;
    }

    private void UpdateLevel(FighterName heroName)
    {
        SlotCharacter hero = _heroes.Find(sc => sc.Name == heroName);
        if (hero != null)
            hero.UpdateLevel();
    }

    private void OnEnable()
    {
        if (heroesLoaded)
        {
            foreach(SlotCharacter sc in _heroes)
                sc.UpdateLevel();
        }
    }

    public void LoadPortraits()
    {
        //GameObject heroesInventoryGO = gameObject.transform.parent.Find("Heroes").gameObject;
        gameObject.GetComponent<HeroesInventory>().opHandle.Completed += handle => {
            LoadPortraitsData();
            heroesLoaded = true;
        };
    }

    private void LoadPortraitsData()
    {
        foreach (FighterName h in GameAssets.current.OwnedHeroes.Select(us => us.FighterName))
        {
            SlotCharacter hs = Instantiate(heroSlotPrefab, transform);
            hs.GetComponent<SlotCharacter>().Name = h;
            hs.GetComponent<SlotCharacter>().Init(h);
            _heroes.Add(hs);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
