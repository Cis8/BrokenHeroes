using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.AddressableAssets;

public class FightersLibrary
{
    private static FightersLibrary _current;
    private AnimationClip movingForwardAttackAnimationClip;
    private AnimationClip attackAnimationClip;
    private AnimationClip movingBackAttackAnimationClip;
    private AnimationClip attackAnimationClipEnemy;
    private AnimationClip abilityAnimationClip;
    private AnimationClip abilityAnimationClipEnemy;
    private GameObject fighterPrefab;
    //private AssetReference fighterAnimationClip;
    //private AssetReference fighterAnimationClipEnemy;

    private FightersLibrary()
    {
        movingForwardAttackAnimationClip = Resources.Load<AnimationClip>("Animations/StandsAnimations/MovingForwardAttackAnim");
        attackAnimationClip = Resources.Load<AnimationClip>("Animations/StandsAnimations/AttackAnim");
        movingBackAttackAnimationClip = Resources.Load<AnimationClip>("Animations/StandsAnimations/MovingBackAttackAnim");
        //attackAnimationClipEnemy = Resources.Load<AnimationClip>("Animations/StandsAnimations/AttackAnimEnemy");
        abilityAnimationClip = Resources.Load<AnimationClip>("Animations/StandsAnimations/AbilityAnim");
        //abilityAnimationClipEnemy = Resources.Load<AnimationClip>("Animations/StandsAnimations/AbilityAnimEnemy");
        fighterPrefab = Resources.Load("Prefabs/FighterPrefab") as GameObject;
    }
    public static FightersLibrary current
    {
        get
        {
            if (_current == null) _current = new FightersLibrary();
            return _current;
        }
    }


    private Dictionary<string, Sprite> portraits;

    public GameObject GetBattleFighter(string name, Transform parent)
    {
        string fighterHeader = "Fighters/" + name + "/";
        ////////Disabled.Log("Starting creation of " + name);
        ////////Disabled.Log("Fighter " + name + "'s unit is: " + u.fighterName);
        GameObject fighterGameObject;
        fighterGameObject = GameObject.Instantiate(fighterPrefab, parent);
        fighterGameObject.tag = parent.tag;
        fighterGameObject.GetComponent<Fighter>().SetupNameAndLogic(name);
        //////Disabled.Log("Fighter Component Logic: " + fighterGameObject.GetComponent<Fighter>().fighterLogic.GetUnit());
        /*SpriteLibraryAsset sla = Resources.Load(fighterHeader + name + "SpritesLibrary") as SpriteLibraryAsset;
        if (sla == null)
            //////Disabled.Log("Sprite Library Asset NOT FOUND!");
        else
            fighterGameObject.GetComponent<SpriteLibrary>().spriteLibraryAsset = sla;*/
        //fighterGameObject.GetComponent<SpriteResolver>().SetCategoryAndLabel("Stances", "id");

        fighterGameObject.GetComponent<Animation>().AddClip(movingForwardAttackAnimationClip, "MovingForwardAttackAnim");
        fighterGameObject.GetComponent<Animation>().AddClip(attackAnimationClip, "AttackAnim");
        fighterGameObject.GetComponent<Animation>().AddClip(movingBackAttackAnimationClip, "MovingBackAttackAnim");
        fighterGameObject.GetComponent<Animation>().AddClip(abilityAnimationClip, "AbilityAnim");
        return fighterGameObject;
    }

    public Sprite GetPortrait(string name)
    {
        return portraits[name];
    }
    

}
