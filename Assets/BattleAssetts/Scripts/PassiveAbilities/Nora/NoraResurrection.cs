using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

using System.Linq;

public class NoraResurrection : PassiveAbility
{
    HateMarkData hateMark;
    RisingHateData risingHate;
    public NoraResurrection(Fighter f, string passiveName) : base(f, passiveName)
    {
        hateMark = GameObject.Instantiate(Resources.Load<HateMarkData>("Fighters/Nora/ModifiersData/NoraHateMark"));
        risingHate = GameObject.Instantiate(Resources.Load<RisingHateData>("Fighters/Nora/ModifiersData/RisingHate"));
        InitializeAbility();
    }
    public override void InitializeAbility()
    {
        //BattleEventSystem.current.OnFighterResurrected += ApplyBuffsUponResurrection;
        BattleEventSystem.current.OnFighterResurrected += ApplyBuffsUponResurrection;
    }

    public override void TerminateAbility()
    {
        BattleEventSystem.current.OnFighterResurrected -= ApplyBuffsUponResurrection;
    }

    private void ApplyBuffsUponResurrection(Fighter f)
    {
        if(f == Parent)
        {
            string critFormula = ((NoraLogic)Parent.fighterLogic).Scalings.dataArray.Where(e => e.Name == "Immortal Hate Crit Augment").FirstOrDefault().Base_Formula;
            ((NoraLogic)Parent.fighterLogic).CritAugmentFromHateMark = System.Convert.ToSingle(((NoraLogic)Parent.fighterLogic).DmgCalculator.Evaluate(critFormula));
            risingHate.InitializeModifier(Parent, Parent).Apply();
            List<Fighter> targets;
            if (Parent.tag == "PlayerTeam")
            {
                targets = FightersManager.current.fighters.GetEnemyFightersBySpeed();
            }
            else
            {
                targets = FightersManager.current.fighters.GetPlayerFightersBySpeed();
            }
            
            foreach(Fighter t in targets)
            {
                hateMark.InitializeModifier(t, Parent).Apply();
            }
        }
    }

    /*private void ApplyBuffsUponResurrection(int t)
    {
        if (t == 1)
        {
            List<Fighter> targets;
            if (Parent.tag == "PlayerTeam")
            {
                targets = FightersManager.current.fighters.GetEnemyFightersBySpeed();
            }
            else
            {
                targets = FightersManager.current.fighters.GetPlayerFightersBySpeed();
            }

            foreach (Fighter tar in targets)
            {
                hateMark.InitializeModifier(tar, Parent).Apply();
            }
        }
    }*/
}
