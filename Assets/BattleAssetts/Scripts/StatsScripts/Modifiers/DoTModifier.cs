using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoTModifier : Modifier
{
    static CalcEngine.CalcEngine DoTCalc = new CalcEngine.CalcEngine();

    DmgInfo dmgPerStack;
    public DoTModifier(int duration, int stacks, DoTModifierData modifierData, Fighter target, Fighter appliedBy) : base(duration, stacks, modifierData, target, appliedBy)
    {
        modifierName = modifierData.DotType.ToString();
        dmgPerStack = new DmgInfo(
                CalculateDamage(),
                DmgTypeEnum.True,
                new DmgSource(GetDoTType(), true),
                appliedBy,
                target,
                false,
                2);
    }

    protected override void SubscribeTick()
    {
        BattleEventSystem.current.OnFighterTurnStarted += Tick;
    }
    protected override void UnsubscribeTick()
    {
        BattleEventSystem.current.OnFighterTurnStarted -= Tick;
    }

    protected override void Tick(Fighter f)
    {
        if (f == target)
        {
            BaseTickLogic();
        }
    }

    protected override void ApplyTick()
    {
        if(((DoTModifierData)Modifier_Data).ApplicationType == DoTModifierData.DamageCalculationType.ON_TICK)
            dmgPerStack = GenerateDmgInfo();
        // we apply the damage
        for (int i = 0; i < stacks; i++)
        {
            if(((DoTModifierData)Modifier_Data).ApplicationType == DoTModifierData.DamageCalculationType.ON_STACK)
                dmgPerStack = GenerateDmgInfo();
            Target.TakeDamage(dmgPerStack);
        }
    }

    DmgInfo GenerateDmgInfo()
    {
        return new DmgInfo(
            CalculateDamage(),
            DmgTypeEnum.True,
            new DmgSource(GetDoTType(), true),
            appliedBy,
            target,
            false,
            2);
    }

    DmgSourceEnum GetDoTType()
    {
        DmgSourceEnum type = DmgSourceEnum.Bleed;
        switch (((DoTModifierData)Modifier_Data).DotType)
        {
            case DoTModifierData.DoT_Type.Burn:
                type = DmgSourceEnum.Burn;
                break;
            case DoTModifierData.DoT_Type.Bleed:
                type = DmgSourceEnum.Bleed;
                break;
            case DoTModifierData.DoT_Type.Poison:
                type = DmgSourceEnum.Poison;
                break;
        }
        return type;
    }

    int CalculateDamage()
    {
        int dmgAmount = 0;
        if (((DoTModifierData)Modifier_Data).BaseFormula != "")
        {
            dmgAmount += System.Convert.ToInt32(DoTCalc.Evaluate(((DoTModifierData)Modifier_Data).BaseFormula));
        }

        if (((DoTModifierData)Modifier_Data).ApplierFormula != "")
        {
            DoTCalc.DataContext = appliedBy.GetUnit();
            dmgAmount += System.Convert.ToInt32(DoTCalc.Evaluate(((DoTModifierData)Modifier_Data).ApplierFormula));
        }

        if (((DoTModifierData)Modifier_Data).TargetFormula != "")
        {
            DoTCalc.DataContext = target.GetUnit();
            dmgAmount += System.Convert.ToInt32(DoTCalc.Evaluate(((DoTModifierData)Modifier_Data).TargetFormula));
        }
        return dmgAmount;
    }

    public override void End()
    {

    }

    protected override void ApplyEffect()
    {

    }
}