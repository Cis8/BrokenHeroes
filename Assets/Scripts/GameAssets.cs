using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;

public class GameAssets : MonoBehaviour
{
    private UnitSpec[] _ownedHeroes;

    private Dictionary<string, Item> _items;

    public UnitSpec[] OwnedHeroes { get => _ownedHeroes; set => _ownedHeroes = value; }

    private static GameAssets _current;

    public static GameAssets current
    {
        get {
            if (_current == null) _current = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>();
            return _current;
        }
    }

    public Dictionary<string, Item> Items { get => _items; set => _items = value; }

    private void Awake()
    {
        _ownedHeroes = Resources.LoadAll<UnitSpec>("Heroes");
        Items = new Dictionary<string, Item>();
        foreach (Item i in Resources.LoadAll<Item>("Inventory/Items"))
        {
            Items.Add(i.Name, i);
        }
    }

    private void Start()
    {

    }

    public static AsyncOperationHandle<Sprite> GetHeroPortrait(FighterName heroName)
    {
        return Addressables.LoadAssetAsync<Sprite>(heroName + "Portrait");
    }

    public static AsyncOperationHandle<Sprite> GetClassIcon(ClassEnum className)
    {
        return Addressables.LoadAssetAsync<Sprite>(className.ToString() + "Icon");
    }

    public UnitSpec GetHeroUnitSpec(FighterName name)
    {
        return Array.Find<UnitSpec>(OwnedHeroes, us => us.FighterName == name);
    }

    public Transform hpDmgPopup;

    public SpriteLibrary heroPortraits;
}
