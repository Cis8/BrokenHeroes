using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
public class RisingHate : Modifier
{
    private int lifeLoss;

    private StatModifierData bonusAtkRisingHate;
    public RisingHate(int duration, int stacks, RisingHateData modifierData, Fighter target, Fighter appliedBy) : base(duration, stacks, modifierData, target, appliedBy)
    {
        modifierName = "RisingHate";
        ScalingCalc.DataContext = target.GetUnit();
        string lifeLossFormula = ((RisingHateData)modifierData).NoraScalings.dataArray.Where(s => s.Name == "Rising Hate Life Loss").FirstOrDefault().Self_Formula;
        lifeLoss = System.Convert.ToInt32(ScalingCalc.Evaluate(lifeLossFormula));
        Debug.Log("scaling rising hate LIFE is: " + lifeLoss);
        bonusAtkRisingHate = GameObject.Instantiate(Resources.Load<StatModifierData>("Fighters/Nora/ModifiersData/BonusAtkRisingHate"));
    }

    public override void End()
    {
        BattleEventSystem.current.OnFighterTurnStarted -= ApplyRisingHateBonusAPTick;
        BattleEventSystem.current.OnFighterTurnEnded -= ApplyRisingHateLifeLossTick;
    }

    protected override void ApplyEffect()
    {
        BattleEventSystem.current.OnFighterTurnStarted += ApplyRisingHateBonusAPTick;
        BattleEventSystem.current.OnFighterTurnEnded += ApplyRisingHateLifeLossTick;
    }

    private void ApplyRisingHateBonusAPTick(Fighter f)
    {
        if(f == target)
        {
            bonusAtkRisingHate.InitializeModifier(target, target).Apply();
        }

    }

    private void ApplyRisingHateLifeLossTick(Fighter f)
    {
        if(f == target)
        {
            target.TakeDamage(new DmgInfo(lifeLoss,
                DmgTypeEnum.True,
                new DmgSource(DmgSourceEnum.Passives, true),
                target,
                target,
                false,
                2));
            }
    }
}
