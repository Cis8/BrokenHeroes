using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
public class DmgPopup : MonoBehaviour
{
    //STATIC FIELDS
    [SerializeField]
    static GameObject hpDmgPopupPrefab;
    [SerializeField]
    TMP_Text _amount;
    [SerializeField]
    Sprite _physicalCritSprite;
    [SerializeField]
    Sprite _magicalCritSprite;
    [SerializeField]
    SpriteRenderer _critSpriteRenderer;

    // CLASS INSTANCE FIELDS
    private Color textColor;
    private Color critColor;
    private Fighter boundFighter;
    private string fighterTag;
    private float randomOffsetX;
    private float randomOffsetY;
    private float randomSpeedReductionX;
    private float randomSpeedReductionY;

    private float currentXSpeed;
    private float currentYSpeed;
    private float currentLifespan;

    [SerializeField] private float yspeed = 6f;
    [SerializeField] private float xspeed = 2f;
    [SerializeField] private float lifeSpan = 1f;
    [SerializeField] private float disappearSpeed = 3f;
    [SerializeField] private float yDisappearSpeed = 3f;
    [SerializeField] private float xDisappearSpeed = 3f;



    public static DmgPopup CreateHpDmgPopup(Fighter f, DmgInfo info)
    {
        //DmgPopup dmgPopup = CreatePopupAndGetDmgPopupComponent(f);
        DmgPopup dmgPopup = GetPooledDmgPopup(f).GetComponent<DmgPopup>();
        dmgPopup.SetupDmg(info);
        return SetupPopup(f, dmgPopup);
    }

    public static DmgPopup CreateHpHealPopup(Fighter f, HealInfo info)
    {
        //DmgPopup dmgPopup = CreatePopupAndGetDmgPopupComponent(f);
        DmgPopup dmgPopup = GetPooledDmgPopup(f).GetComponent<DmgPopup>();
        dmgPopup.SetupHeal(info);
        return SetupPopup(f, dmgPopup);
    }

    private static DmgPopup CreatePopupAndGetDmgPopupComponent(Fighter f)
    {
        Transform damagePopupTransform = Instantiate(hpDmgPopupPrefab.transform, f.transform.position - new Vector3(0f, 1f, 0f), Quaternion.identity);
        return damagePopupTransform.GetComponent<DmgPopup>();
    }

    private static GameObject GetPooledDmgPopup(Fighter f)
    {
        GameObject popup = ObjectPool.SharedInstance.GetPooledObject();
        if (popup != null)
        {
            popup.transform.position = f.transform.position - new Vector3(0f, 1f, 0f);
            popup.SetActive(true);
        }
        return popup;
    }

    public static DmgPopup SetupPopup(Fighter f, DmgPopup dmgPopup)
    {
        dmgPopup.textColor.a = 1;
        dmgPopup.critColor.a = 1;
        dmgPopup.currentLifespan = dmgPopup.lifeSpan;
        dmgPopup.currentXSpeed = dmgPopup.xspeed;
        dmgPopup.currentYSpeed = dmgPopup.yspeed;
        dmgPopup.boundFighter = f;
        dmgPopup.fighterTag = dmgPopup.boundFighter.tag;
        dmgPopup.randomOffsetX = Random.Range(0f, 1f);
        dmgPopup.randomOffsetY = Random.Range(0f, 1f);
        dmgPopup.randomSpeedReductionX = Random.Range(0f, 1f);
        dmgPopup.randomSpeedReductionY = Random.Range(0f, 1f);
        dmgPopup.transform.position += new Vector3(dmgPopup.randomOffsetX, dmgPopup.randomOffsetY, 0f);
        return dmgPopup;
    }

    private void Awake()
    {
        Addressables.LoadAssetAsync<GameObject>("HpPopup").Completed += handle => { hpDmgPopupPrefab = handle.Result; };
    }
    public void SetupDmg(DmgInfo info)
    {
        if (info.IsCritical)
        {
            critColor = new Color(1f, 1f, 1f);
            _amount.SetText("-" + info.Amount.ToString() + " !");
            if (info.Type == DmgTypeEnum.Physical)
            {
                //critImage.color = new Color(0.85f, 0f, 0f);
                _critSpriteRenderer.enabled = true;
                _critSpriteRenderer.sprite = _physicalCritSprite;
                textColor = new Color(0.85f, 0f, 0f);
            }
            else if (info.Type == DmgTypeEnum.Magical)
            {
                //critImage.color = new Color(1f, 0f, 1f);
                _critSpriteRenderer.enabled = true;
                _critSpriteRenderer.sprite = _magicalCritSprite;
                textColor = new Color(1f, 0f, 1f);
            }  
            else
                textColor = new Color(1f, 1f, 1f);
        }
        else
        {
            _critSpriteRenderer.enabled = false;
            _amount.SetText("-" + info.Amount.ToString());
            if (info.Type == DmgTypeEnum.Physical)
                textColor = new Color(0.75f, 0.4f, 0f);
            else if (info.Type == DmgTypeEnum.Magical)
                textColor = new Color(0.4f, 0.4f, 0.87f);
            else
                textColor = new Color(1f, 1f, 1f);
        }
        //textMesh.SetText("-" + textMesh.text);
        _amount.color = textColor;
        _critSpriteRenderer.color = critColor;
    }

    public void SetupHeal(HealInfo info)
    {
        _amount.SetText("+" + info.Amount.ToString());
        textColor = new Color(0.1f, 0.9f, 0f);
        _amount.color = textColor;
    }



    private void Update()
    {
        int direction;
        if (fighterTag == "PlayerTeam")
            direction = -1;
        else
            direction = 1;
        transform.position += new Vector3(direction * (currentXSpeed - randomSpeedReductionX), currentYSpeed - randomSpeedReductionY) * Time.deltaTime;
        currentYSpeed -= yDisappearSpeed * Time.deltaTime;
        currentXSpeed -= xDisappearSpeed * Time.deltaTime;
        currentLifespan -= Time.deltaTime;
        if(currentLifespan <= 0)
        {
            float deltaAlpha = disappearSpeed * Time.deltaTime;
            critColor.a -= deltaAlpha;
            textColor.a -= deltaAlpha;
            _critSpriteRenderer.color = critColor;
            _amount.color = textColor;
            //if (textColor.a <= 0) Destroy(gameObject);
            if (textColor.a <= 0) gameObject.SetActive(false);
        }
    }
}
