using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // TODO: add counter to wait some time before starting to erase the lostHpFill
    public Fighter Parent;
    LostFillScript lostHpBar;
    public Slider hpBar;
    private void Awake()
    {
        BattleEventSystem.current.OnFighterTookDamage += CheckUpdateDmgHP;
        BattleEventSystem.current.OnFighterHealed += CheckUpdateHealHP;
        BattleEventSystem.current.OnFighterRemoved += (Fighter f) => { if (f == Parent) Destroy(this.gameObject); };
    }

    private void Start()
    {

    }

    private void Update()
    {
        transform.position = Parent.transform.position + (new Vector3(0, 2f, 0));
    }

    public void SetColor(string _tag)
    {
        if (_tag == "EnemyTeam")
            this.transform.Find("Fill").GetComponent<Image>().color = new Color(0.854902f, 0.4649376f, 0.05098038f, 1);
        else
            this.transform.Find("Fill").GetComponent<Image>().color = new Color(0.09779923f, 0.6886792f, 0.08705943f, 1);
    }

    public void CheckUpdateDmgHP(Fighter f, DmgInfo info)
    {
        if (Parent == f)
        {
            SetHP(f.GetUnit().CurrentHP);
        }
    }

    public void CheckUpdateHealHP(Fighter f, HealInfo info)
    {
        if (Parent == f)
        {
            SetHP(f.GetUnit().CurrentHP);
        }
    }

    public void Initialize(int hp, Fighter f)
    {
        //hpBar = transform.Find("Fill").GetComponent<Slider>();
        lostHpBar = transform.Find("Fill/EmptyBar/LostFill").GetComponent<LostFillScript>();
        lostHpBar.Initialize(hp);
        hpBar.maxValue = hp;
        hpBar.value = hp;
        Parent = f;

    }

    public void SetHP(int value)
    {
        // TODO: Add GainedHp for green recover from health
        lostHpBar.LostHp((int)hpBar.value - value);
        hpBar.value = value;
    }
}
