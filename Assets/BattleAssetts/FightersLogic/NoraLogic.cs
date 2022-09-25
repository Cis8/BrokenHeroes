using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 0) scalings
using System.Linq;
using UnityEngine.AddressableAssets;

public class NoraLogic : FighterLogic
{
    // 1) scalings
    private NoraScalings scalings;
    private float critAugmentFromHateMark = 0f;
    public NoraLogic(Unit unit, Fighter parent, string team) : base(unit, parent, team)
    {
        // 2) scalings
        Addressables.LoadAssetAsync<NoraScalings>(parent.fighterName + "Scalings").Completed += handle => {
            Scalings = handle.Result;
            PassiveAbilities.Add(new Assets.BattleAssetts.Scripts.PassiveAbilities.Nora.NoraAttack(Parent));
            PassiveAbilities.Add(new Assets.BattleAssetts.Scripts.PassiveAbilities.Nora.NoraAbility(Parent));
        };

        //passiveCalculator.DataContext = nr;
        
        PassiveAbilities.Add(new NoraResurrection(Parent, "Immortal Hate"));
        
        
    }

    protected override void AttackBase(List<Fighter> enemiesToAttack)
    {
        foreach (Fighter en in enemiesToAttack)
        {
            bool isCritical;
            bool hasHateMark = en.fighterLogic.Modifiers.Exists(m => m.ModifierName == "HateMark");
            Debug.Log("Has hate mark: " + hasHateMark.ToString());
            if (hasHateMark)
                isCritical = CastDiceForCritical();
            else
                isCritical = base.CastDiceForCritical();
            GenerateDmgInfoForAtk(en, isCritical);
        }
    }

    protected override void AbilityBase(List<Fighter> enemiesToAttack)
    {
        foreach (Fighter en in enemiesToAttack)
        {
            bool isCritical;
            bool hasHateMark = en.fighterLogic.Modifiers.Exists(m => m.ModifierName == "HateMark");
            Debug.Log("Has hate mark: " + hasHateMark.ToString());
            if (hasHateMark)
                isCritical = CastDiceForCritical();
            else
                isCritical = base.CastDiceForCritical();
            GenerateDmgInfoForAbility(en, isCritical);
        }
    }

    protected override bool CastDiceForCritical()
    {
        Debug.Log("Crit required " + (1f - Unit.CurrentCriticalChance - CritAugmentFromHateMark));
        return (1f - Unit.CurrentCriticalChance - CritAugmentFromHateMark) < UnityEngine.Random.Range(0f, 1f);
    }

    public NoraScalings Scalings { get => scalings; set => scalings = value; }
    public float CritAugmentFromHateMark { get => critAugmentFromHateMark; set => critAugmentFromHateMark = value; }
}
