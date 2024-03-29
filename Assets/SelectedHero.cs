using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedHero : MonoBehaviour
{
    [SerializeField]
    Image _portrait;
    FighterName _name;
    bool _isEmpty = true;

    public FighterName Name { get => _name; private set => _name = value; }
    public bool IsEmpty { get => _isEmpty; private set => _isEmpty = value; }

    public void Init(FighterName name)
    {
        Name = name;
        _portrait.enabled = true;
        _portrait.sprite = GameAssets.GetHeroPortrait(name).Result;
        IsEmpty = false;
    }

    public void Free()
    {
        Name = FighterName.None;
        _portrait.enabled = false;
        _portrait.sprite = null;
        IsEmpty = true;
    }

    public void ButtonToRemoveTap()
    {
        HomeEventSystem.current.ChosenHeroToRemove(_name);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
