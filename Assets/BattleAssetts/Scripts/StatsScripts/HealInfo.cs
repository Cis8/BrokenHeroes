using UnityEditor;
using UnityEngine;

public enum HealSourceEnum { Attack, Ability, Lifesteal, Passives, Equipment, Resurrection }

public class HealSource
{
    // if it is reactable, means that passives and status that react to dmgTaken are activated by this dmgSource. Otherwise, not
    bool _isReactable;

    HealSourceEnum _healSource;

    public HealSource(HealSourceEnum healSource, bool isReactable = true)
    {
        _isReactable = isReactable;
        HealSourceEnum = healSource;
    }
    public bool IsReactable { get => _isReactable; set => _isReactable = value; }
    public HealSourceEnum HealSourceEnum { get => _healSource; set => _healSource = value; }
}

public class HealInfo
{
    private int _amount;
    HealSource _source;
    Fighter _dealerFighter;
    Fighter _healedFighter;

    public HealInfo(int amount,HealSource source, Fighter dealerFighter, Fighter healedFighter)
    {
        Amount = amount;
        Source = source;
        DealerFighter = dealerFighter;
        HealedFighter = healedFighter;
    }

    public Fighter HealedFighter { get => _healedFighter; set => _healedFighter = value; }
    public Fighter DealerFighter { get => _dealerFighter; set => _dealerFighter = value; }
    public HealSource Source { get => _source; set => _source = value; }
    public int Amount { get => _amount; set => _amount = value; }
}