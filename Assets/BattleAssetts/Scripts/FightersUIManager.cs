using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;

public class FightersUIManager : MonoBehaviour
{
    public HealthBar hpBar;
    public EnergyBar energyBar;
    public ModifiersBarPanel modifiersBar;
    public StateModifierIcon stateModifierIconStun;
    public StateModifierIcon stateModifierIconSilence;
    public StateModifierIcon stateModifierIconTerror;
    [SerializeField]
    private HitImage hitImagePrefab;
    private Sprite stunSprite;
    private Sprite silenceSprite;
    private Sprite terrorSprite;
    private Sprite bleedSprite;
    private Sprite burnSprite;
    private Sprite poisonSprite;
    UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<IList<Sprite>> callback;


    private void Awake()
    {
        BattleEventSystem.current.OnFighterInstantiated += AddHpBarToFighter;
        BattleEventSystem.current.OnFighterTookDamage += ShowHpDmgPopUp;
        BattleEventSystem.current.OnFighterTookDamage += ShowHitImage;
        BattleEventSystem.current.OnFighterHealed += ShowHpHealPopUp;
        BattleEventSystem.current.OnFighterInstantiated += AddModifiersBarToFighter;
        BattleEventSystem.current.OnFighterInstantiated += AddStateModifiersIcons;
    }

    private void OnDestroy()
    {
        BattleEventSystem.current.OnFighterInstantiated -= AddHpBarToFighter;
        BattleEventSystem.current.OnFighterTookDamage -= ShowHpDmgPopUp;
        BattleEventSystem.current.OnFighterTookDamage -= ShowHitImage;
        BattleEventSystem.current.OnFighterHealed -= ShowHpHealPopUp;
        BattleEventSystem.current.OnFighterInstantiated -= AddModifiersBarToFighter;
        BattleEventSystem.current.OnFighterInstantiated -= AddStateModifiersIcons;
    }

    private void Start()
    {
        callback = Addressables.LoadAssetsAsync<Sprite>("StateModifier", null);
        callback.Completed += handle =>
        {
            foreach(Sprite s in handle.Result)
            {
                switch (s.name)
                {
                    case "Stun":
                        stunSprite = s;
                        break;
                    case "Silence":
                        silenceSprite = s;
                        break;
                    case "Terror":
                        terrorSprite = s;
                        break;
                }
            }



                /*Image comp = stateModifierIconStun.gameObject.GetComponent<Image>();
                bool oldState = stateModifierIconStun.gameObject.GetComponent<Image>().IsActive();
                stateModifierIconStun.gameObject.GetComponent<Image>().enabled = true;
                stateModifierIconStun.gameObject.GetComponent<Image>().sprite = handle.Result;
                stateModifierIconStun.gameObject.GetComponent<Image>().enabled = oldState;*/
        };

        callback = Addressables.LoadAssetsAsync<Sprite>("DoTSprites", null);
        callback.Completed += handle =>
        {
            foreach (Sprite s in handle.Result)
            {
                switch (s.name)
                {
                    case "Poison":
                        poisonSprite = s;
                        break;
                    case "Bleed":
                        bleedSprite = s;
                        break;
                    case "Burn":
                        burnSprite = s;
                        break;
                }
            }
        };
    }

    private void AddStateModifiersIcons(Fighter f)
    {
        if (stunSprite != null)
        {
            InstantiateStun(f);
        }
        else
            callback.Completed += handle =>
            {
                callback.Completed += handle =>
                    InstantiateStun(f);
            };
            
        if (silenceSprite != null)
            InstantiateSilence(f);
        else
            callback.Completed += handle =>
                InstantiateSilence(f);

        if (terrorSprite != null)
            InstantiateTerror(f);
        else
            callback.Completed += handle =>
                InstantiateTerror(f);
    }

    private void InstantiateStun(Fighter f)
    {
        stateModifierIconStun = Instantiate(stateModifierIconStun, f.transform.parent.position, Quaternion.identity, transform);
        stateModifierIconStun.Type = StateModifiersEnum.STUN;
        stateModifierIconStun.XOffset = 0.85f;
        stateModifierIconStun.YOffset = 1.7f;
        stateModifierIconStun.Parent = f;
        stateModifierIconStun.gameObject.GetComponent<Image>().sprite = Instantiate(stunSprite);
    }

    private void InstantiateSilence(Fighter f)
    {
        stateModifierIconSilence = Instantiate(stateModifierIconSilence, f.transform.parent.position, Quaternion.identity, transform);
        stateModifierIconSilence.Type = StateModifiersEnum.SILENCE;
        stateModifierIconSilence.XOffset = 0.85f;
        stateModifierIconSilence.YOffset = 1.9f;
        stateModifierIconSilence.Parent = f;
        stateModifierIconSilence.gameObject.GetComponent<Image>().sprite = Instantiate(silenceSprite);
    }

    private void InstantiateTerror(Fighter f)
    {
        stateModifierIconTerror = Instantiate(stateModifierIconTerror, f.transform.parent.position, Quaternion.identity, transform);
        stateModifierIconTerror.Type = StateModifiersEnum.TERROR;
        stateModifierIconTerror.XOffset = 0.85f;
        stateModifierIconTerror.YOffset = 2.1f;
        stateModifierIconTerror.Parent = f;
        stateModifierIconTerror.gameObject.GetComponent<Image>().sprite = Instantiate(terrorSprite);
    }


    // TODO: this should be extended with all the hud
    private void AddHpBarToFighter(Fighter f)
    {
        //////////////Disabled.Log("Fighter enriched with HpBar");
        hpBar = Instantiate(hpBar, f.transform.parent.position + new Vector3(0, 2.5f, 0), Quaternion.identity, transform);
        energyBar = Instantiate(energyBar, f.transform.parent.position + new Vector3(0, 0f, 0), Quaternion.identity, transform);
        //hpBar.transform.SetParent(f.transform);
        //hpBar.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
        hpBar.SetColor(f.transform.tag);
        hpBar.Initialize(f.GetUnit().MaxHp, f);
        energyBar.SetColor();
        energyBar.Initialize(f);
    }

    private void ShowHpDmgPopUp(Fighter f, DmgInfo info)
    {
        DmgPopup.CreateHpDmgPopup(f, info);
    }

    private void ShowHitImage(Fighter f, DmgInfo info)
    {
        HitImage hitImage;
        if (info.Source.DmgSourceEnum == DmgSourceEnum.Attack || info.Source.DmgSourceEnum == DmgSourceEnum.Ability)
        {
            /*if (f != null)
            {
                hitImage = Instantiate<HitImage>(hitImagePrefab, f.transform.position, Quaternion.identity, gameObject.transform);
                hitImage.SetSprite(info.DealerFighter.fighterGraphic.HitImageSO.Target);
                hitImage.SetFighter(f);
            }
            if (info.DealerFighter != null && info.DealerFighter.fighterGraphic.HitImageSO.Self != null)
            {
                hitImage = Instantiate<HitImage>(hitImagePrefab, info.DealerFighter.transform.position, Quaternion.identity, gameObject.transform);
                hitImage.SetSprite(info.DealerFighter.fighterGraphic.HitImageSO.Self);
                hitImage.SetFighter(info.DealerFighter);
            }*/
        }
        else
        {
            switch(info.Source.DmgSourceEnum)
            {
                case DmgSourceEnum.Bleed:
                    hitImage = Instantiate<HitImage>(hitImagePrefab, f.transform.position, Quaternion.identity, gameObject.transform);
                    hitImage.SetSprite(bleedSprite);
                    hitImage.SetFighter(f);
                    hitImage.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    break;
                case DmgSourceEnum.Burn:
                    hitImage = Instantiate<HitImage>(hitImagePrefab, f.transform.position, Quaternion.identity, gameObject.transform);
                    hitImage.SetSprite(burnSprite);
                    hitImage.SetFighter(f);
                    hitImage.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    break;
                case DmgSourceEnum.Poison:
                    hitImage = Instantiate<HitImage>(hitImagePrefab, f.transform.position, Quaternion.identity, gameObject.transform);
                    hitImage.SetSprite(poisonSprite);
                    hitImage.SetFighter(f);
                    hitImage.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    break;
            }
        }
    }

    private void ShowHpHealPopUp(Fighter f, HealInfo info)
    {
        if(info.Amount > 0)
            DmgPopup.CreateHpHealPopup(f, info);
    }

    private void AddModifiersBarToFighter(Fighter f)
    {
        modifiersBar = Instantiate(modifiersBar, f.transform.parent.position + new Vector3(0, 2.75f, 0), Quaternion.identity, transform);
        modifiersBar.boundFighter = f;
    }
}
