using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HeroesList : ScriptableObject
{
    [SerializeField]
    List<string> heroes;

    public void AddHero(string name)
    {
        heroes.Add(name);
    }

    public void RemoveHero(string name)
    {
        heroes.Remove(name);
    }

    public void EraseHeroes()
    {
        heroes = new List<string>();
    }

    public List<string> GetHeroes()
    {
        return heroes;
    }
}
