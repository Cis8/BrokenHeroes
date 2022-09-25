using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChosenTeamPanel : MonoBehaviour
{
    [SerializeField]
    GameObject chosenHeroSlotPrefab;
    Dictionary<string, GameObject> chosenHeroesSlots;

    // Start is called before the first frame update
    void Start()
    {
        HomeEventSystem.current.OnHeroChosenToAdd += AddHeroSlot;
        HomeEventSystem.current.OnHeroChosenToRemove += RemoveHeroSlot;

        chosenHeroesSlots = new Dictionary<string, GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddHeroSlot(string name)
    {
        GameObject chosenHero = Instantiate<GameObject>(chosenHeroSlotPrefab, transform);
        chosenHero.transform.Find("HeroPortrait").GetComponent<Image>().sprite = gameObject.transform.parent.Find("Heroes").GetComponent<HeroesInventory>().GetPortrait(name);
        chosenHero.GetComponent<ChosenHeroSlot>().ChosenHeroName = name;
        chosenHeroesSlots.Add(name, chosenHero);
    }

    public void RemoveHeroSlot(string name)
    {
        GameObject.Destroy(chosenHeroesSlots[name]);
        chosenHeroesSlots.Remove(name);
    }
}
