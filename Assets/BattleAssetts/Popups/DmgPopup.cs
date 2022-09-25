using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class DmgPopup : MonoBehaviour
{
    private TextMeshPro textMesh;
    private Color textColor;
    private Fighter boundFighter;
    private string fighterTag;
    private float randomOffsetX;
    private float randomOffsetY;
    private float randomSpeedReductionX;
    private float randomSpeedReductionY;

    [SerializeField]  private float yspeed = 6f;
    [SerializeField] private float xspeed = 2f;
    [SerializeField] private float lifeSpan = 1f;
    [SerializeField] private float disappearSpeed = 3f;
    [SerializeField] private float yDisappearSpeed = 3f;
    [SerializeField] private float xDisappearSpeed = 3f;


    public static DmgPopup CreateHpDmgPopup(Fighter f, DmgInfo info)
    {
        DmgPopup dmgPopup = GetDmgPopupComponent(f);
        dmgPopup.SetupDmg(info);
        return SetupPopup(f, dmgPopup);
    }

    public static DmgPopup CreateHpHealPopup(Fighter f, HealInfo info)
    {
        DmgPopup dmgPopup = GetDmgPopupComponent(f);
        dmgPopup.SetupHeal(info);
        return SetupPopup(f, dmgPopup);
    }

    private static DmgPopup GetDmgPopupComponent(Fighter f)
    {
        Transform damagePopupTransform = Instantiate(GameAssets.current.hpDmgPopup, f.transform.position - new Vector3(0f, 1f, 0f), Quaternion.identity);
        return damagePopupTransform.GetComponent<DmgPopup>();
    }

    public static DmgPopup SetupPopup(Fighter f, DmgPopup dmgPopup)
    {
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
        textMesh = transform.GetComponent<TextMeshPro>();   
    }
    public void SetupDmg(DmgInfo info)
    {
        if (info.IsCritical)
        {
            textMesh.SetText("-" + info.Amount.ToString() + " !");
            //SpriteRenderer critImage = gameObject.transform.Find("Critical").GetComponent<SpriteRenderer>();
            //critImage.enabled = true;
            if (info.Type == DmgTypeEnum.Physical)
            {
                //critImage.color = new Color(0.85f, 0f, 0f);
                textColor = new Color(0.85f, 0f, 0f);
            }
            else if (info.Type == DmgTypeEnum.Magical)
            {
                //critImage.color = new Color(1f, 0f, 1f);
                textColor = new Color(1f, 0f, 1f);
            }  
            else
                textColor = new Color(1f, 1f, 1f);
        }
        else
        {
            textMesh.SetText("-" + info.Amount.ToString());
            if (info.Type == DmgTypeEnum.Physical)
                textColor = new Color(0.75f, 0.4f, 0f);
            else if (info.Type == DmgTypeEnum.Magical)
                textColor = new Color(0.23f, 0.23f, 0.73f);
            else
                textColor = new Color(1f, 1f, 1f);
        }
        //textMesh.SetText("-" + textMesh.text);
        textMesh.color = textColor;
    }

    public void SetupHeal(HealInfo info)
    {
        textMesh.SetText("+" + info.Amount.ToString());
        textColor = new Color(0.1f, 0.9f, 0f);
        textMesh.color = textColor;
    }

    private void Update()
    {
        int direction;
        if (fighterTag == "PlayerTeam")
            direction = -1;
        else
            direction = 1;
        transform.position += new Vector3(direction * (xspeed - randomSpeedReductionX), yspeed - randomSpeedReductionY) * Time.deltaTime;
        yspeed -= yDisappearSpeed * Time.deltaTime;
        xspeed -= xDisappearSpeed * Time.deltaTime;
        lifeSpan -= Time.deltaTime;
        if(lifeSpan <= 0)
        {
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a <= 0) Destroy(gameObject);
        }
    }
}
