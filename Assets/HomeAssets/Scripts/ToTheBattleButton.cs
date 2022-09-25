using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToTheBattleButton : MonoBehaviour
{
    [SerializeField]
    HeroesList chosenHeroes;
    // Start is called before the first frame update
    void Start()
    {
        chosenHeroes.EraseHeroes();
        HomeEventSystem.current.OnHeroChosenToAdd += AddHero;
        HomeEventSystem.current.OnHeroChosenToRemove += RemoveHero;
    }

    private void OnDestroy()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AddHero(string name)
    {
        //////Disabled.Log("Hero added " + name);
        chosenHeroes.AddHero(name);
    }

    private void RemoveHero(string name)
    {
        chosenHeroes.RemoveHero(name);
    }

    public void LaunchBattle()
    {
        SceneManager.LoadScene("BattleScene");
    }
}
