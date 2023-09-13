using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MeltingDarkBlade : Modifier
{
    float meltingBladeDmgMultiplier;
    string atkDmgMultiplierFormula;
    //private ModifierData atkMultiplierModifierData;
    public MeltingDarkBlade(int duration, int stacks, MeltingDarkBladeData modifierData, Fighter target, Fighter appliedBy) : base(duration, stacks, modifierData, target, appliedBy)
    {
        modifierName = "MeltingDarkBlade";
        atkDmgMultiplierFormula = ((MeltingDarkBladeData)modifierData).GorathScalings.dataArray.Where(s => s.Name == "MeltingDarkBlade").FirstOrDefault().Base_Formula;
        //atkMultiplierModifierData = GameObject.Instantiate(Resources.Load<ModifierData>("Fighters/Gorath/ModifiersData/MeltingDarkBladeData"));
        //atkMultiplierModifierData = new MeltingDarkBladeData();
    }

    public override void End()
    {
        BattleEventSystem.current.OnFighterAboutToTakeDamage -= MultiplyAtkDmg;
    }

    protected override void ApplyEffect(bool firstApplication)
    {
        if (firstApplication)
            BattleEventSystem.current.OnFighterAboutToTakeDamage += MultiplyAtkDmg;
    }

    private void MultiplyAtkDmg(Fighter f, DmgInfo dmg)
    {
        if(dmg.DealerFighter == Target && dmg.Source .DmgSourceEnum == DmgSourceEnum.Attack)
        {
            meltingBladeDmgMultiplier = System.Convert.ToSingle(ScalingCalc.Evaluate(atkDmgMultiplierFormula));
            dmg.Amount = (int)(((float)Stacks * meltingBladeDmgMultiplier) * dmg.Amount);
        }
    }
}
