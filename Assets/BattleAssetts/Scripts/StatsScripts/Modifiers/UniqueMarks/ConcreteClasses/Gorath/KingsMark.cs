using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class KingsMark : Modifier
{
    string trueDmgFormula;
    public KingsMark(int duration, int stacks, KingsMarkData modifierData, Fighter target, Fighter appliedBy) : base(duration, stacks, modifierData, target, appliedBy)
    {
        modifierName = "KingsMark";
        trueDmgFormula = ((KingsMarkData)modifierData).GorathScalings.dataArray.Where(s => s.Name == "KingsMark").FirstOrDefault().Base_Formula;
    }

    public override void End()
    {
        BattleEventSystem.current.OnFighterTookDamage -= ActivateMark;
    }

    protected override void ApplyEffect(bool firstApplication)
    {
        if (firstApplication)
        {
            BattleEventSystem.current.OnFighterTookDamage += ActivateMark;
        }
    }



    private void ActivateMark(Fighter f, DmgInfo info)
    {
        if (info.DealerFighter == AppliedBy && info.Source.DmgSourceEnum == DmgSourceEnum.Attack)
        {
            Target.TakeDamage(new DmgInfo(
                CalculateMarkDmg(),
                DmgTypeEnum.True, 
                new DmgSource(DmgSourceEnum.Passives, "KingsMark", false),
                AppliedBy, 
                Target, 
                false, 
                1f));
            if(Target.fighterLogic.IsAlive())
                Remove();
        }
    }

    private int CalculateMarkDmg()
    {
        ScalingCalc.DataContext = AppliedBy.GetUnit();
        return System.Convert.ToInt32(ScalingCalc.Evaluate(((KingsMarkData)Modifier_Data).GorathScalings.dataArray.Where(e => e.Name == "KingsMark").FirstOrDefault().Self_Formula));
    }
}
