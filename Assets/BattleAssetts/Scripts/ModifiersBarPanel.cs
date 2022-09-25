using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ModifiersBarPanel : MonoBehaviour
{
    public Fighter boundFighter;
    Dictionary<string, GameObject> iconSlots;
    GameObject imagePrefab;
    Sprite buffArrow;
    Sprite debuffArrow;
    //ModifiersIconLibrary modifiersIconLibrary;

    private void Awake()
    {
        iconSlots = new Dictionary<string, GameObject>();
        buffArrow = Resources.Load<Sprite>("Sprites/arrow-up");
        debuffArrow = Resources.Load<Sprite>("Sprites/arrow-down");
        //modifiersIconLibrary = new ModifiersIconLibrary();
        BattleEventSystem.current.OnFighterRemoved += (Fighter f) => { if (f == boundFighter) { 
                BattleEventSystem.current.OnModifierApplied -= SetIcon;
                BattleEventSystem.current.OnModifierRemoved -= RemoveIcon;
                Destroy(this.gameObject); } 
        };
    }

    // Start is called before the first frame update
    void Start()
    {
        imagePrefab = Resources.Load<GameObject>("Prefabs/ModifierIcon");
        BattleEventSystem.current.OnModifierApplied += SetIcon;
        BattleEventSystem.current.OnModifierRemoved += RemoveIcon;
    }

    private void Update()
    {
        transform.position = boundFighter.transform.position + (new Vector3(0.1f, 1.65f, 0));
    }


    public void SetIcon(Modifier m)
    {
        if (m.Target == boundFighter)
        {
            string iconName = m.Modifier_Data.Icon.name;
            if (iconSlots.ContainsKey(iconName))
            {
                ChangeTextStacks(iconName, 1);
            }
            else
            {
                Sprite s = m.Modifier_Data.Icon;
                GameObject iconSlot = Instantiate(imagePrefab, gameObject.transform);
                iconSlot.GetComponent<Image>().sprite = s;
                iconSlots.Add(iconName, iconSlot);
            }
            ShowAddition(iconSlots[iconName]);
        }
        /*switch (m)
        {
            // CASE PhysicalAttackModifier
            case PhysicalAttackModifier:
                string iconName;
                if (m.ModifierData.IsPositive)
                    iconName = "PhiAtkBuff";
                else
                    iconName = "PhiAtkDebuff";
                if (iconSlots.ContainsKey(iconName))
                    ChangeTextStacks(iconName, 1);
                else
                {
                    Sprite s;
                    /*modifiersIconLibrary.GetIcon(iconName, result => {
                        s = result;
                        if (iconSlots.ContainsKey(iconName))
                            ChangeTextStacks(iconName, 1);
                        else
                        {
                            GameObject iconSlot = Instantiate(imagePrefab, gameObject.transform);
                            iconSlot.GetComponent<Image>().sprite = s;
                            iconSlots.Add(iconName, iconSlot);
                        }
                    });*//*
                    s = m.ModifierData.Icon;
                    if (iconSlots.ContainsKey(iconName))
                        ChangeTextStacks(iconName, 1);
                    else
                    {
                        GameObject iconSlot = Instantiate(imagePrefab, gameObject.transform);
                        iconSlot.GetComponent<Image>().sprite = s;
                        iconSlots.Add(iconName, iconSlot);
                    }
                }
                break;
                // NEXT HERE
        }*/
    }

    private void ShowAddition(GameObject iconSlot)
    {
        iconSlot.GetComponent<ModifierIcon>().FlashBigToSmall();
    }

    public void RemoveIcon(Modifier m)
    {
        if (m.Target == boundFighter)
        {
            string iconName = m.Modifier_Data.Icon.name;
            if (iconSlots.ContainsKey(iconName))
            {
                if (ChangeTextStacks(iconName, -m.Stacks) <= 0)
                {
                    iconSlots[iconName].GetComponent<ModifierIcon>().DropIcon();
                    iconSlots.Remove(iconName);
                }
            }
        }

        /*switch (m)
        {
            // CASE PhysicalAttackModifier
            case PhysicalAttackModifier:
                string iconName;
                if (m.ModifierData.IsPositive)
                    iconName = "PhiAtkBuff";
                else
                    iconName = "PhiAtkDebuff";
                if (iconSlots.ContainsKey(iconName))
                {
                    if (ChangeTextStacks(iconName, -1) <= 0)
                    {
                        Destroy(iconSlots[iconName]);
                        //iconSlots.Remove(iconName);
                    }
                }
                else
                {
                    Sprite s;
                    /*modifiersIconLibrary.GetIcon(iconName, result =>
                    {
                        s = result;
                        if (iconSlots.ContainsKey(iconName))
                        {
                            if (ChangeTextStacks(iconName, -1) <= 0)
                            {
                                Destroy(iconSlots[iconName]);
                                //iconSlots.Remove(iconName);
                            }
                        }
                        else
                        {
                            GameObject iconSlot = Instantiate(imagePrefab, gameObject.transform);
                            iconSlot.GetComponent<Image>().sprite = s;
                            iconSlots.Add(iconName, iconSlot);
                        }
                    });*//*
                    s = m.ModifierData.Icon;
                    if (iconSlots.ContainsKey(iconName))
                    {
                        if (ChangeTextStacks(iconName, -1) <= 0)
                        {
                            Destroy(iconSlots[iconName]);
                            //iconSlots.Remove(iconName);
                        }
                    }
                    else
                    {
                        GameObject iconSlot = Instantiate(imagePrefab, gameObject.transform);
                        iconSlot.GetComponent<Image>().sprite = s;
                        iconSlots.Add(iconName, iconSlot);
                    }
                }
                break;
        }*/

                // NEXT HERE
    }

    int ChangeTextStacks(string iconName, int amount)
    {
        Text comp = iconSlots[iconName].transform.Find("StacksText").GetComponent<Text>();
        int nStacks = int.Parse(comp.text);
        nStacks += amount;
        comp.text = nStacks.ToString();
        return nStacks;
    }


}
