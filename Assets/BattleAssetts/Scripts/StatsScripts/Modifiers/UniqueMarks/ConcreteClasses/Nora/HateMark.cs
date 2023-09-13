using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.BattleAssetts.Scripts;
using System.Linq;

public class HateMark : Modifier
{
    private float scaling;
    
    public HateMark(int duration, int stacks, HateMarkData modifierData, Fighter target, Fighter appliedBy) : base(duration, stacks, modifierData, target, appliedBy)
    {
        modifierName = "HateMark";
        //scaling = System.Convert.ToSingle(ScalingCalc.Evaluate(((HateMarkData)modifierData).NoraScalings.dataArray.Where(s => s.Name == "Immortal Hate").FirstOrDefault().Base_Formula));
        scaling = CalcEngineUtil.FloatDamageCalculator(((HateMarkData)modifierData).NoraScalings.dataArray.Where(s => s.Name == "Immortal Hate").FirstOrDefault().Base_Formula);
        Debug.Log("scaling hate mark is: " + scaling);
    }

    public float Scaling { get => scaling; set => scaling = value; }

    public override void End()
    {
        BattleEventSystem.current.OnFighterAboutToTakeDamage -= CheckIfAugmentDamage;
    }

    protected override void ApplyEffect(bool firstApplication)
    {
        if (firstApplication)
            BattleEventSystem.current.OnFighterAboutToTakeDamage += CheckIfAugmentDamage;
    }

    private void CheckIfAugmentDamage(Fighter f, DmgInfo info)
    {
        if (f.fighterLogic.Modifiers.Exists(mod => mod.ModifierName == "HateMark") &&
            info.DealerFighter == AppliedBy &&
            info.DamagedFighter == Target &&
            (info.Source.DmgSourceEnum == DmgSourceEnum.Attack || info.Source.DmgSourceEnum == DmgSourceEnum.Ability))
        {
            info.AddPercModifier(Scaling);
        }
    }
}
