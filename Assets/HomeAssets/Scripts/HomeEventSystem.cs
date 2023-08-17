using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeEventSystem : MonoBehaviour
{
    /*private static HomeEventSystem _current;

    public static HomeEventSystem current
    {
        get
        {
            if (_current == null) _current = (Instantiate(Resources.Load("HomeEventSystem")) as GameObject).GetComponent<HomeEventSystem>();
            return _current;
        }
    }*/

    public static HomeEventSystem current;

    private void Awake()
    {
        current = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // HERO FROM INVENTORY ADDED TO CHOSEN ONES
    public delegate void HeroToAdd(FighterName hero);
    public event HeroToAdd OnHeroChosenToAdd;
    public void ChosenHeroToAdd(FighterName hero)
    {
        OnHeroChosenToAdd?.Invoke(hero);
    }

    // HERO TO BE REMOVED FROM the CHOSEN HEROES
    public delegate void HeroToRemove(FighterName hero);
    public event HeroToRemove OnHeroChosenToRemove;
    public void ChosenHeroToRemove(FighterName hero)
    {
        OnHeroChosenToRemove?.Invoke(hero);
    }
}
