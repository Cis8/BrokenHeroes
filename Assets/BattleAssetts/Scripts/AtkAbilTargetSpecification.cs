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

    public int NumberOfTargets { get => numberOfTargets; set => numberOfTargets = value; }
    public string ScalingFormulaAttacker { get => scalingFormulaAttacker; set => scalingFormulaAttacker = value; }
    public string ScalingFormulaTarget { get => scalingFormulaTarget; set => scalingFormulaTarget = value; }
    public DmgTypeEnum DamageType { get => damageType; set => damageType = value; }
    public AttackStrategyEnum Strategy { get => strategy; set => strategy = value; }
}
