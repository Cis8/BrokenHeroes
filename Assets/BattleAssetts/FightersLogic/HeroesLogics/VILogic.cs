using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class VILogic : FighterLogic
{


    StatModifierData atkOnTurnStart;
    StatModifierData atkOnDmgDealt;
    StatModifierData atkPercOnTurnBegin;

    DoTModifierData bleedOnAttack;
    public VILogic(Unit unit, Fighter parent, string team) : base(unit, parent, team)
    {
        bleedOnAttack = Resources.Load<DoTModifierData>("Fighters/VI/ModifiersData/BleedOnAttack");
        PassiveAbilities.Add(new VIlifeStealOnTurnStart(Parent, "Lifesteal on turn start"));
    }

    protected override void AttackLogic(List<Fighter> targets)
    {
        base.AttackLogic(targets);
        foreach(Fighter t in targets)
        {
            if(t.fighterLogic.IsAlive())
                bleedOnAttack.InitializeModifier(t, Parent).Apply();
        }
    }

    private void IncrementAttackOnTurnStart(int turn)
    {
        atkOnTurnStart.InitializeModifier(Parent, Parent).Apply();
    }

    private void IncrementAttackPercOnTurnStart(int turn)
    {
        atkPercOnTurnBegin.InitializeModifier(Parent, Parent).Apply();
    }

    private void IncrementAttackOnFighterTookDamage(Fighter f, DmgInfo info)
    {
        if(info.Source.DmgSourceEnum == DmgSourceEnum.Attack && info.DealerFighter == Parent)
        {
            atkOnDmgDealt.Amount = (int)(((float)info.Amount / (float)info.DamagedFighter.GetUnit().MaxHP) * 100.0f);
            atkOnDmgDealt.InitializeModifier(Parent, Parent).Apply();
        }
    }

}
