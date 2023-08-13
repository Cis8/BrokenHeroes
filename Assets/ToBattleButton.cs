using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ToBattleButton : MonoBehaviour
{
    [SerializeField]
    HeroesList _chosenHeroesSO;
    [SerializeField]
    SelectedParty _selectedParty;

    public HeroesList ChosenHeroesSO { get => _chosenHeroesSO; private set => _chosenHeroesSO = value; }
    public SelectedParty SelectedParty { get => _selectedParty; private set => _selectedParty = value; }

    // Start is called before the first frame update
    void Start()
    {
        ChosenHeroesSO.EraseHeroes();
        //HomeEventSystem.current.OnHeroChosenToAdd += AddHero;
        //HomeEventSystem.current.OnHeroChosenToRemove += RemoveHero;
    }

    private void AddHero(string hero)
    {
        ChosenHeroesSO.AddHero(hero);
    }

    private void RemoveHero(string name)
    {
        ChosenHeroesSO.RemoveHero(name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LaunchBattle()
    {
        SelectedParty.SelectedHeroes.ForEach(hero => {
            if (hero.Name != null)
                AddHero(hero.Name);
        });
        SceneManager.LoadScene("BattleScene");
    }

    private void OnDestroy()
    {
        HomeEventSystem.current.OnHeroChosenToAdd -= AddHero;
        HomeEventSystem.current.OnHeroChosenToRemove -= RemoveHero;
    }
}
