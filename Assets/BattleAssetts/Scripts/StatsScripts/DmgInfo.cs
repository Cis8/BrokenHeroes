using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DmgTypeEnum { Physical, Magical, True }

public enum DmgSourceEnum { Attack, Ability, Thorns, Passives, Equipment, Bleed, Burn, Poison }
public class DmgSource
{
    // if it is reactable, means that passives and status that react to dmgTaken are activated by this dmgSource. Otherwise, not
    bool _isReactable;

    string _sourceName; // used for ad hoc controls

    DmgSourceEnum _dmgSource;

    public DmgSource(DmgSourceEnum dmgSource, bool isReactable = true)
    {
        _isReactable = isReactable;
        _sourceName = "";
        _dmgSource = dmgSource;
    }
    public DmgSource(DmgSourceEnum dmgSource, string sourceName, bool isReactable = true)
    {
        _isReactable = isReactable;
        SourceName = sourceName;
        _dmgSource = dmgSource;
    }

    public bool IsReactable { get => _isReactable; set => _isReactable = value; }
    public DmgSourceEnum DmgSourceEnum { get => _dmgSource; set => _dmgSource = value; }
    public string SourceName { get => _sourceName; set => _sourceName = value; }
}

public class DmgInfo
{
    private int _amount;

    // are applied after the percAmountModifications are applied to _amount
    private List<int> flatAmountModifications = new List<int>();

    // multiply _amount. Are written in the form of float. Each modification is of type 0.2f, -0.9f, ...
    // the percentage of 0.2 and -0.9 will be: 1.0 + 0.2 - 0.9 = 0.3. So the output will be 0.3 * _amount.
    private List<float> percAmountModifications = new List<float>();
    DmgTypeEnum _type;
    DmgSource _source;
    Fighter _dealerFighter;
    Fighter _damagedFighter;
    bool _isCritical;
    float _critModifier;

    public DmgInfo(int amount, DmgTypeEnum type, DmgSource source, Fighter dealerFighter, Fighter damagedFighter, bool isCritical, float critModifier)
    {
        _amount = amount;
        _type = type;
        _source = source;
        _dealerFighter = dealerFighter;
        DamagedFighter = damagedFighter;
        _isCritical = isCritical;
        _critModifier = critModifier;
    }

    public float GetPercentModification()
    {
        float accum = 0;
        foreach (float f in percAmountModifications)
        {
            accum += f;
        }
        float temp = 1f + accum;
        if (temp < 0)
            temp = 0;
        return temp;
    }

    public int GetFlatModification()
    {
        int acc = 0;
        foreach (int v in flatAmountModifications)
            acc += v;
        return acc;
    }

    public void AddFlatModifier(int value)
    {
        flatAmountModifications.Add(value);
    }

    public void AddPercModifier(float value)
    {
        percAmountModifications.Add(value);
    }

    public int Amount { get => _amount; set => _amount = value; }
    public DmgTypeEnum Type { get => _type; set => _type = value; }
    public DmgSource Source { get => _source; set => _source = value; }
    public bool IsCritical { get => _isCritical; set => _isCritical = value; }
    public float CritModifier { get => _critModifier; set => _critModifier = value; }
    public Fighter DealerFighter { get => _dealerFighter; set => _dealerFighter = value; }
    public Fighter DamagedFighter { get => _damagedFighter; set => _damagedFighter = value; }
}
