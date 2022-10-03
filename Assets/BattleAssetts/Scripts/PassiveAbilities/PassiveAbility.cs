using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PassiveAbility
{
    Fighter parent;
    string name;

    public PassiveAbility(Fighter parent, string passiveName)
    {
        this.parent = parent;
        this.name = passiveName;
    }

    public Fighter Parent { get => parent; set => parent = value; }

    public abstract void InitializeAbility();

    public abstract void TerminateAbility();
}
