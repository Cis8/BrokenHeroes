using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ChosenHeroes : ScriptableObject
{
    [SerializeField]
    List<FighterName >_chosenHeroesForTheBattle;

    public List<FighterName> ChosenHeroesForTheBattle { get => _chosenHeroesForTheBattle; set => _chosenHeroesForTheBattle = value; }
}
