using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedParty : MonoBehaviour
{
    [SerializeField]
    List<SelectedHero> _selectedHeroes;

    public List<SelectedHero> SelectedHeroes { get => _selectedHeroes; private set => _selectedHeroes = value; }

    private void Awake()
    {
        SelectedHeroes = new List<SelectedHero>(6);
        foreach (Transform child in gameObject.transform)
        {
            SelectedHeroes.Add(child.GetComponent<SelectedHero>());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        HomeEventSystem.current.OnHeroChosenToAdd += AddHeroSlot;
        HomeEventSystem.current.OnHeroChosenToRemove += RemoveHeroSlot;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddHeroSlot(FighterName hero)
    {
        int index = SelectedHeroes.FindIndex(h => h.IsEmpty);
        if(index != -1)
        {
            SelectedHeroes[index].enabled = true;
            SelectedHeroes[index].Init(hero);
        }
    }

    public void RemoveHeroSlot(FighterName hero)
    {
        int index = SelectedHeroes.FindIndex(h => h.Name == hero);
        if (index != -1)
        {
            SelectedHeroes[index].enabled = false;
            SelectedHeroes[index].Free();
        }
    }

    private void OnDestroy()
    {
        HomeEventSystem.current.OnHeroChosenToAdd -= AddHeroSlot;
        HomeEventSystem.current.OnHeroChosenToRemove -= RemoveHeroSlot;
    }
}
