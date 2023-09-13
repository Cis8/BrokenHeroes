using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SlotCharacterSelect : SlotCharacter
{
    [SerializeField]
    GameObject checkMark;

    public GameObject CheckMark { get => checkMark; protected set => checkMark = value; }
    public void AddOrRemoveHero()
    {
        if (!CheckMark.activeSelf)
            HomeEventSystem.current.ChosenHeroToAdd(Name);
        else
            HomeEventSystem.current.ChosenHeroToRemove(Name);
    }

    private void ToggleCheckMark(FighterName hero)
    {
        if(hero == Name)
        {
            if (CheckMark.activeSelf)
                CheckMark.SetActive(false);
            else
                CheckMark.SetActive(true);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        HomeEventSystem.current.OnHeroChosenToAdd += ToggleCheckMark;
        HomeEventSystem.current.OnHeroChosenToRemove += ToggleCheckMark;
    }

    private void OnDestroy()
    {
        HomeEventSystem.current.OnHeroChosenToAdd -= ToggleCheckMark;
        HomeEventSystem.current.OnHeroChosenToRemove -= ToggleCheckMark;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
