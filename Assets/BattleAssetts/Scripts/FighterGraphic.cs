using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.U2D.Animation;

public enum FighterState { IDLE, ATTACKING, ABILITYING, DAMAGED, DEAD};

public class FighterGraphic : MonoBehaviour
{
    public SpriteLibrary spriteLibrary;
    public SpriteResolver spriteResolver;
    public SpriteRenderer spriteRenderer;
    private HitImageSO hitImageSO;
    private Fighter fighter;
    private Animation ownAanimation;

    private string currentCategory;

    FighterState currentState;

    public HitImageSO HitImageSO { get => hitImageSO; set => hitImageSO = value; }

    private void Start()
    {
        currentCategory = "Stances";
        spriteLibrary = this.gameObject.GetComponent<SpriteLibrary>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        fighter = gameObject.GetComponent<Fighter>();
        ownAanimation = gameObject.GetComponent<Animation>();

        BattleEventSystem.current.OnFighterTookDamage += TakeDamage;
        BattleEventSystem.current.OnFighterResurrected += Resurrection;
        BattleEventSystem.current.OnStateModifierApplied += ShowStateModifierIcon;
        //Resources.LoadAll<Sprite>();
        //Resources.LoadAsync<SpriteLibrary>("Fighters/" + gameObject.GetComponent<Fighter>().fighterName + "SpritesLibrary").completed += (AsyncOperation op) => { op. };
        // needs to be loaded from addressables so that animations have the correct sprite hashes
        Addressables.LoadAssetAsync<SpriteLibraryAsset>(gameObject.GetComponent<Fighter>().fighterName + "/SpritesLibrary").Completed += handle =>
        {
            spriteLibrary.spriteLibraryAsset = handle.Result;
            spriteResolver.SetCategoryAndLabel(currentCategory, "idle");
            //spriteResolver = this.gameObject.GetComponent<SpriteResolver>();
            //LoadFighterStances();
        };
        Addressables.LoadAssetAsync<HitImageSO>(gameObject.GetComponent<Fighter>().fighterName + "HitImageSO").Completed += handle =>
        {
            HitImageSO = handle.Result;
        };


        /*parent = gameObject.GetComponent<Fighter>();
        Debug.Log("Parent null: " + parent);*/
    }

    private void ShowStateModifierIcon(FighterLogic.StateModifiers.StateModifierInfo info)
    {
        if(info.Target == gameObject.GetComponent<Fighter>())
        {
            switch (info.Type)
            {
                case StateModifiersEnum.STUN:

                    break;
                case StateModifiersEnum.SILENCE:
                    break;
                case StateModifiersEnum.TERROR:
                    break;
            }
        }
    }

    private void OnDestroy()
    {
        BattleEventSystem.current.OnFighterTookDamage -= TakeDamage;
        BattleEventSystem.current.OnFighterResurrected -= Resurrection;
    }

    private void LateUpdate()
    {
        if (gameObject.tag == "EnemyTeam")
            this.transform.localPosition = new Vector3(-1 * this.transform.localPosition.x, this.transform.localPosition.y, this.transform.localPosition.z);
        if (!fighter.fighterLogic.IsAlive())
        {
            spriteResolver.SetCategoryAndLabel(currentCategory, "dead");

            // disabled so that if the fighter was attacking, it stops immidiately
            ownAanimation.enabled = false;
        }
    }

    /*private void LoadFighterStances()
    {
        Addressables.LoadAssetsAsync<Sprite>(gameObject.GetComponent<Fighter>().fighterName + "Stances", null).Completed += handle =>
        {
            //Debug.Log("Sprites loaded: " + handle.Result.Count);
            foreach (Sprite s in handle.Result)
            {
                Debug.Log("Sprite: " + s.name);
                spriteLibrary.spriteLibraryAsset.AddCategoryLabel(s, "Stances", s.name);
            }
            this.spriteResolver = gameObject.AddComponent<SpriteResolver>();
            spriteResolver.spriteLibrary.spriteLibraryAsset = spriteLibrary.spriteLibraryAsset;
            if (spriteResolver.spriteLibrary.spriteLibraryAsset.GetSprite("Stances", "loadingAbility") == null)
            {
                throw new System.Exception("MISSING SPRITE");
            }
            
            spriteResolver.SetCategoryAndLabel("Stances", "idle");
        };
    }*/

    private void IdleStanceGraphic()
    {
        spriteResolver.SetCategoryAndLabel(currentCategory, "idle");
        currentState = FighterState.IDLE;
    }

    public void AttackingState()
    {
        // label and category are set in the animation
        SetStateCategory("movingForward");
    }

    public void AbilityingState()
    {
        // label and category are set in the animation
        SetStateCategory("loadingAbility");
    }

    private void DamagedStanceGraphic()
    {
        spriteResolver.SetCategoryAndLabel(currentCategory, "damaged");
        SetStateCategory("damaged");
    }

    public void DeadStanceGraphic()
    {
        spriteResolver.SetCategoryAndLabel(currentCategory, "dead");
        SetStateCategory("dead");
    }

    public IEnumerator DamagedStance()
    {
        if(currentState == FighterState.IDLE)
        {
            string category = spriteResolver.GetCategory();
            string label = spriteResolver.GetLabel();
            DamagedStanceGraphic();
            Color oldColor = spriteRenderer.color;
            spriteRenderer.color = new Color(1f, 0.3f, 0.3f);
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.color = oldColor;
            yield return new WaitForSeconds(0.3f);
            if(currentState != FighterState.ATTACKING && currentState != FighterState.ABILITYING)
            {
                spriteResolver.SetCategoryAndLabel(category, label);
                SetStateCategory(label);
            }
        }
    }

    private void SetStateCategory(string label)
    {
        switch(label)
        {
            case "idle":
                currentState = FighterState.IDLE;
                break;
            case "attacking":
                currentState = FighterState.ATTACKING;
                break;
            case "movingForward":
                currentState = FighterState.ATTACKING;
                break;
            case "movingBackward":
                currentState = FighterState.ATTACKING;
                break;
            case "loadingAbility":
                currentState = FighterState.ABILITYING;
                break;
            case "slashingAbility":
                currentState = FighterState.ABILITYING;
                break;
            case "damaged":
                currentState = FighterState.DAMAGED;
                break;
            case "dead":
                currentState = FighterState.DEAD;
                break;
        }
    }

    public void TakeDamage(Fighter f, DmgInfo info)
    {
        if (f.fighterGraphic == this)
        {
            if (f.GetUnit().CurrentHP <= 0)
            {
                DeadStanceGraphic();
            }
            else
            {
                StartCoroutine(DamagedStance());
            }
        }
    }


    public void SetStance(string stanceName)
    {
        spriteResolver.SetCategoryAndLabel(currentCategory, stanceName);
    }




    private void Resurrection(Fighter f)
    {
        if(f == gameObject.GetComponent<Fighter>())
        {
            List<string> categories = new List<string>(spriteLibrary.spriteLibraryAsset.GetCategoryNames());
            if (categories.Contains("Resurrected" + gameObject.GetComponent<Fighter>().fighterLogic.ResurrectionCounter))
                currentCategory = "Resurrected" + gameObject.GetComponent<Fighter>().fighterLogic.ResurrectionCounter;
            
            spriteResolver.SetCategoryAndLabel(currentCategory, "idle");
            gameObject.GetComponent<Animation>().enabled = true;
        }
    }
}
