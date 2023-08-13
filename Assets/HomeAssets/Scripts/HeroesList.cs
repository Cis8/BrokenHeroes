using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HeroesList : ScriptableObject
{
    [SerializeField]
    List<HeroState> heroes;

    public void AddHero(string hero)
    {
        heroes.Add(GameAssets.current.GetHeroState(hero));
    }

    public void RemoveHero(string heroToRemove)
    {
        heroes.Remove(heroes.Find(h => h.Name == heroToRemove));
    }

    public void EraseHeroes()
    {
        heroes = new List<HeroState>();
    }

    public HeroState GetHero(string name)
    {
        return heroes.Find(h => h.Name == name);
    }

    public List<HeroState> GetHeroes()
    {
        return heroes;
    }
}
