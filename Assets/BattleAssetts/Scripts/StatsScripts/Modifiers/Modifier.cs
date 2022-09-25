using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Modifier
{
    protected string modifierName;
    protected int duration;
    protected int stacks;
    private ModifierData modifierData;
    protected Fighter target;
    protected Fighter appliedBy;

    private CalcEngine.CalcEngine scalingCalc = new CalcEngine.CalcEngine();
    public Modifier()
    {
        modifierName = "NULL_MODIFIER";
    }
    protected Modifier(int duration, int stacks, ModifierData modifierData, Fighter target, Fighter appliedBy)
    {
        //Disabled.Log("The modifier name is " + ModifierName);
        ModifierName = modifierData.name;
        Duration = duration;
        this.stacks = stacks;
        this.Modifier_Data = modifierData;
        Duration = duration;
        Target = target;
        AppliedBy = appliedBy;
    }

    ~Modifier()
    {
    }

    protected virtual void SubscribeTick()
    {
        BattleEventSystem.current.OnFighterTurnEnded += Tick;
    }

    public void Apply()
    {
        List<Modifier> modifiers = Target.fighterLogic.Modifiers;

        if (Modifier_Data.IsStackable)
        {
            Modifier m = modifiers.Find((mod) => mod.ModifierName == ModifierName);
            if (m == null)
            {
                modifiers.Add(this);
                SubscribeTick();
                ApplyEffect();
                BattleEventSystem.current.ModifierHasBeenApplied(this);
            }
            else
            {
                //Disabled.Log("Modifier to stack was found, name: " + ModifierName);
                bool modifiedSomething = false;
                if(Modifier_Data.IsEffectStackable)
                {
                    // Adding stacks
                    m.stacks++;
                    ApplyEffect();
                    BattleEventSystem.current.ModifierHasBeenApplied(m);
                    modifiedSomething = true;
                }

                switch (Modifier_Data.IsDurationStackable)
                {
                    case ModifierData.DurationStackType.NONE:
                        // left blank, do nothing
                        break;
                    case ModifierData.DurationStackType.ADD:
                        // Add the duration
                        m.duration += Duration;
                        modifiedSomething = true;
                        break;
                    case ModifierData.DurationStackType.RESET:
                        // Reset the duration
                        m.duration = Duration;
                        modifiedSomething = true;
                        break;
                }

                if (!modifiedSomething)
                {
                    throw new System.Exception("Modifier is defined Stackable but no propery is Stackable.");
                }
            }
        }
        else
        {
            modifiers.Add(this);
            SubscribeTick();
            ApplyEffect();
            BattleEventSystem.current.ModifierHasBeenApplied(this);
        }
    }

    protected virtual void Tick(Fighter f)
    {
        if(f == appliedBy)
        {
            ApplyTick();
            duration--;
            if (duration == 0)
            {
                End();
                Remove();
            }
        }
    }

    protected void BaseTickLogic()
    {
        ApplyTick();
        duration--;
        if (duration == 0)
        {
            End();
            Remove();
        }
    }

    protected virtual void ApplyTick()
    {

    }

    public void Remove()
    {
        UnsubscribeTick();
        Target.fighterLogic.Modifiers.Remove(this);
        BattleEventSystem.current.ModifierHasBeenRemoved(this);
    }

    protected virtual void UnsubscribeTick()
    {
        BattleEventSystem.current.OnFighterTurnEnded -= Tick;
    }

    protected abstract void ApplyEffect();
    public abstract void End();

    protected void SubtractDuration(int amount)
    {
        if(duration != -1)
        {
            duration -= amount;
            if (duration < 0)
                duration = 0;
        }
    }
    protected void AddDuration(int amount)
    {
        if(duration != -1)
            duration += amount;
    }
    public int Duration { get => duration; set => duration = value; }
    public int Stacks { get => stacks;  }
    public string ModifierName { get => modifierName; set => modifierName = value; }
    public Fighter Target { get => target; set => target = value; }
    public Fighter AppliedBy { get => appliedBy; set => appliedBy = value; }
    public ModifierData Modifier_Data { get => modifierData; set => modifierData = value; }
    public CalcEngine.CalcEngine ScalingCalc { get => scalingCalc; set => scalingCalc = value; }
}
