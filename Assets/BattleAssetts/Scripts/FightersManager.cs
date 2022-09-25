using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightersManager : MonoBehaviour
{
    public static FightersManager current;

    public FightersList fighters;

    public HeroesList heroesToAdd;


    // TODO: replace the battle stations with the definitive array of n*2 battle stations' transform (n = team size)
    public List<Transform> heroesBattleStations;
    public Transform enemy1BattleStation;
    public Transform enemy2BattleStation;

    //TODO: replace this public reference to specific fighters with the heroes team chosen by the player and the enemies of the selected level
    public GameObject hero1Prefab;
    public GameObject enemy1Prefab;

    private void Awake()
    {
        current = this;
        fighters = new FightersList();
    }

    // Start is called before the first frame update
    void Start()
    {
        BattleEventSystem.current.OnFighterTookDamage += CheckIfFighterIsStillAlive;
        BattleEventSystem.current.OnTurnEnded += RemoveDeadFighters;

        AddFightersToTheBattle(heroesToAdd.GetHeroes(), heroesBattleStations);

        GameObject enemyFromLibrary = FightersLibrary.current.GetBattleFighter("VI", enemy1BattleStation);
        Fighter e = enemyFromLibrary.GetComponent<Fighter>();
        e.GetUnit().Position =0;
        e.GetComponent<SpriteRenderer>().sortingOrder = 2;
        e.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        BattleEventSystem.current.FighterInstantiated(e);
        fighters.AddFighter(e);


        GameObject enemy1FromLibrary = FightersLibrary.current.GetBattleFighter("Doombot", enemy2BattleStation);
        Fighter e1 = enemy1FromLibrary.GetComponent<Fighter>();
        e1.GetUnit().Position = 1;
        e1.GetComponent<SpriteRenderer>().sortingOrder = 1;
        e1.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        BattleEventSystem.current.FighterInstantiated(e1);
        fighters.AddFighter(e1);

        /*GameObject heroFromLibrary = FightersLibrary.current.GetBattleFighter("VI", heroesBattleStations[0]);
        Fighter h = heroFromLibrary.GetComponent<Fighter>();
        h.GetUnit().position = 0;
        BattleEventSystem.current.FighterInstantiated(h);
        fighters.AddFighter(h);*/

    }

    public void AddFighterToTheBattle(string name, Transform parent, int position)
    {
        GameObject heroFromLibrary = FightersLibrary.current.GetBattleFighter(name, parent);
        Fighter f = heroFromLibrary.GetComponent<Fighter>();
        f.GetUnit().Position = position;
        f.GetComponent<SpriteRenderer>().sortingOrder = 3 - ((position + 1) % 3);
        fighters.AddFighter(f);
        BattleEventSystem.current.FighterInstantiated(f);
    }
    public void AddFightersToTheBattle(List<string> names, List<Transform> parents)
    {
        if (names.Count > parents.Count)
            throw new System.Exception("More fighters than platforms.");
        for (int i = 0; i < parents.Count && i < names.Count; i++) {
            AddFighterToTheBattle(names[i], parents[i], i);
        }
    }

    private void CheckIfFighterIsStillAlive(Fighter f, DmgInfo info)
    {
        //////////Disabled.Log("Checking if " + f.name + " is still alive.");
        if (f.GetUnit().CurrentHP <= 0)
        {
            //////////Disabled.Log("Added " + f.name + " to the be removed list.");
            if (f.GetUnit().CurrentRemainingResurrections == 0)
                fighters.ToBeRemoved(f);
            if (!fighters.IsAnyoneAlive(f.tag))
            {
                if (f.tag == "PlayerTeam")
                {
                    BattleEventSystem.current.TeamPlayerHasDied();
                }
                else
                {
                    BattleEventSystem.current.TeamEnemyHasDied();
                }
            }
        }
    }

    private void RemoveDeadFighters(int t)
    {
        List<Fighter> removedFighters = fighters.RemoveFighters();
        foreach(Fighter f in removedFighters)
        {
            BattleEventSystem.current.RemovedFighter(f);
            //////////Disabled.Log("Removed fighter: " + f.name);
            Destroy(f.gameObject);
        }
        
    }
}
