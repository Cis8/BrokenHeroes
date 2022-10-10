using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PassiveAbility
{
    Fighter parent;
    string name;
    bool isActive = false;
    public PassiveAbility(Fighter parent, string passiveName)
    {
        this.parent = parent;
        this.name = passiveName;
    }

    public Fighter Parent { get => parent; set => parent = value; }
    public bool IsActive { get => isActive; set => isActive = value; }

    public void CheckInitialize()
    {
        if (!IsActive)
        {
            IsActive = true;
            InitializeAbility();
        }
    }

    public void CheckTerminate()
    {
        if (parent.GetUnit().CurrentRemainingResurrections <= 0)
        {
            IsActive = false;
            TerminateAbility();
        }
    }

    public abstract void InitializeAbility();

    public abstract void TerminateAbility();
}
