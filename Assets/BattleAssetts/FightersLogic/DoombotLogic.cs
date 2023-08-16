using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoombotLogic : FighterLogic
{

    public DoombotLogic(Unit unit, Fighter parent, string team) : base(unit, parent, team)
    {

    }

    protected override void AttackLogic(List<Fighter> targets)
    {
        base.AttackLogic(targets);
        //int amountToHeal = (int)((1f - ((float)Unit.CurrentHP / (float)Unit.MaxHp)) * Unit.AP * 3.0f);
        int amountToHeal = (int)(((float)Unit.LostHP) * 0.03);
        HealFlat(new HealInfo(
            amountToHeal,
            new HealSource(HealSourceEnum.Attack, true),
            Parent,
            Parent
            ));
    }


}