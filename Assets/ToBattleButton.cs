using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ToBattleButton : MonoBehaviour
{
    [SerializeField]
    ChosenHeroes _chosenHeroes;
    [SerializeField]
    SelectedParty _selectedParty;

    public ChosenHeroes ChosenHeroes { get => _chosenHeroes; private set => _chosenHeroes = value; }
    public SelectedParty SelectedParty { get => _selectedParty; private set => _selectedParty = value; }

    private void Awake()
    {
        _chosenHeroes.ChosenHeroesForTheBattle = new List<FighterName>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //HomeEventSystem.current.OnHeroChosenToAdd += AddHero;
        //HomeEventSystem.current.OnHeroChosenToRemove += RemoveHero;
    }

    private void AddHero(FighterName hero)
    {
        ChosenHeroes.ChosenHeroesForTheBattle.Add(hero);
    }

    private void RemoveHero(FighterName name)
    {
        ChosenHeroes.ChosenHeroesForTheBattle.Remove(name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LaunchBattle()
    {
        bool atLeastOneAdded = false;
        SelectedParty.SelectedHeroes.ForEach(hero => {
            if (hero.Name != FighterName.None)
            {
                AddHero(hero.Name);
                atLeastOneAdded = true;
            }
        });
        if(atLeastOneAdded)
            SceneManager.LoadScene("BattleScene");
    }
}
