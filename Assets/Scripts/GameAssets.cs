using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _current;

    public static GameAssets current
    {
        get {
            if (_current == null) _current = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>();
            return _current;
        }
    }

    public Transform hpDmgPopup;

    public SpriteLibrary heroPortraits;
}
