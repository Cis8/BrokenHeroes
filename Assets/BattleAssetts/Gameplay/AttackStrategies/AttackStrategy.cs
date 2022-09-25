using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AttackStrategyEnum { Standard, LessLife };

public abstract class AttackStrategy
{
    public abstract List<Fighter> ChooseTargets(int numberOfTargets, string attackingFighterTag);
}
