using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GameAssets : MonoBehaviour
{
    [SerializeField]
    private HeroesList _ownedHeroes;

    public HeroesList OwnedHeroes { get => _ownedHeroes; set => _ownedHeroes = value; }

    private static GameAssets _current;

    public static GameAssets current
    {
        get {
            if (_current == null) _current = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>();
            return _current;
        }
    }

    public static AsyncOperationHandle<Sprite> GetHeroPortrait(string heroName)
    {
        return Addressables.LoadAssetAsync<Sprite>(heroName + "Portrait");
    }

    public static AsyncOperationHandle<Sprite> GetClassIcon(ClassEnum className)
    {
        return Addressables.LoadAssetAsync<Sprite>(className.ToString() + "Icon");
    }

    public HeroState GetHeroState(string name)
    {
        return OwnedHeroes.GetHero(name);
    }

    public Transform hpDmgPopup;

    public SpriteLibrary heroPortraits;
}
