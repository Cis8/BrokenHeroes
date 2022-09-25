using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeroes : ScriptableObject
{
    [SerializeField]
    private List<string> ownedHeroes;

    public List<string> OwnedHeroes { get => ownedHeroes; set => ownedHeroes = value; }
}
