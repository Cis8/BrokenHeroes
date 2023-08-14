using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AtkAbilTargetSpecification
{
    [SerializeField]
    int numberOfTargets = 1;
    [SerializeField]
    string scalingFormulaAttacker;
    [SerializeField]
    string scalingFormulaTarget;
    [SerializeField]
    DmgTypeEnum damageType;
    [SerializeField]
    AttackStrategyEnum strategy;

}
