using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    // TODO: add counter to wait some time before starting to erase the lostHpFill
    public Fighter boundFighter;
    public Slider energyBar;
    private void Awake()
    {
        //BattleEventSystem.current.OnFighterTookDamage += CheckUpdateHP;
        BattleEventSystem.current.OnFighterRemoved += (Fighter f) => { if (f == boundFighter) Destroy(this.gameObject); };
    }

    private void Start()
    {

    }

    private void Update()
    {
        transform.position = boundFighter.transform.position + (new Vector3(0, 1.80f, 0));
        CheckUpdateEnergy();

    }

    public void SetColor()
    {
        this.transform.Find("Fill").GetComponent<Image>().color = new Color(1f, 1f, 0f, 1);
    }

    public void CheckUpdateEnergy()
    {
        SetEnergy(boundFighter.GetUnit().CurrentEnergy);
    }

    public void Initialize(Fighter f)
    {
        boundFighter = f;
        energyBar.maxValue = 100;
        energyBar.value = boundFighter.GetUnit().CurrentEnergy;
    }

    public void SetEnergy(int value)
    {
        energyBar.value = value;
    }
}
