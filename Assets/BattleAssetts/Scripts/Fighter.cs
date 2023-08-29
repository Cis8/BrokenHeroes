using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using System.Linq;
using System;

[System.Serializable]
public class Fighter : MonoBehaviour
{
    //[SerializeField]
    //public Unit toBeReadOnlyUnit;
    [SerializeField]
    public FighterLogic fighterLogic;
    //private bool alive = true;
    public Animator animator;
    public FighterGraphic fighterGraphic;
    public string fighterName;


    private void Awake()
    {
        //SetUnit(Resources.Load<Unit>("Fighters/" + fighterLogic.Name() + "/" + fighterLogic.Name()));
        
    }

    private void Start()
    {
        
    }

    public void SetupNameAndLogic(string name)
    {
        fighterName = name;
        string fighterHeader = "Heroes/";
        UnitSpec us = Resources.Load<UnitSpec>(fighterHeader + name);
        Unit u = UnitConverter.ConvertUnit(us, this.transform);
        var type = Type.GetType(name + "Logic");
        fighterLogic = (FighterLogic)Activator.CreateInstance(type, u, this, this.tag);
        //fighterLogic = FighterLogicFactory.GetFighterLogic(name, gameObject.GetComponent<Fighter>());
    }

    // returns true if the unit is still alive after having taken the damage, false otherwise
    public bool TakeDamage(DmgInfo info)
    {
        fighterLogic.TakeDamage(info);
        return fighterLogic.IsAlive();
    }

   

    public IEnumerator StartAttack()
    {
        string clipName;
        switch (fighterLogic.ActionToPerformNow())
        {
            case "baseAtk":
                clipName = "AttackAnim";
                break;
            case "ability":
                clipName = "AbilityAnim";
                break;
            case "moveForward":
                clipName = "MovingForwardAttackAnim";
                break;
            case "moveBack":
                clipName = "MovingBackAttackAnim";
                break;
            case "nothing":
                clipName = "";
                break;
            default:
                clipName = "";
                break;
        }



        if (!fighterLogic.StateModifs.IsStunned())
        {
            if(clipName == "AbilityAnim")
            {
                gameObject.GetComponent<Animation>().Play(clipName);
                yield return new WaitUntil(() => !gameObject.GetComponent<Animation>().IsPlaying(clipName));
                //yield return new WaitForSeconds(gameObject.GetComponent<Animation>().GetClip(clipName).averageDuration);
            }
            else
            {
                if (clipName == "MovingBackAttackAnim")
                {
                    gameObject.GetComponent<Animation>().Play(clipName);
                    yield return new WaitUntil(() => !gameObject.GetComponent<Animation>().IsPlaying(clipName));
                    //yield return new WaitForSeconds(gameObject.GetComponent<Animation>().GetClip(clipName).averageDuration);
                }
                else
                {
                    gameObject.GetComponent<Animation>().Play(clipName);
                    yield return new WaitUntil(() => !gameObject.GetComponent<Animation>().IsPlaying(clipName));
                    //yield return new WaitForSeconds(gameObject.GetComponent<Animation>().GetClip(clipName).averageDuration);
                    yield return StartCoroutine(StartAttack());
                }
            }
        }
    }

    // used in the animation
    public void ExecuteAttack()
    {
        fighterLogic.ExecuteAttack();
    }

    public void ExecuteAbility()
    {
        fighterLogic.ExecuteAbility();
    }


    public Unit GetUnit()
    {
        return fighterLogic.Unit;
    }

    public void SetUnit(Unit u)
    {
        fighterLogic.Unit = u;
    }

    
}
