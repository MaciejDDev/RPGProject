using System;
using System.Collections.Generic;
using System.Linq;

public class Stat
{
    StatType _statType;
    StatData _statData;
    List<StatMod> _mods = new List<StatMod>();

    public string Name => _statType.name;

    public Stat( StatType statType, StatData statData)
    {
        _statData = statData;
        _statType = statType;
        
    }

    public int GetValue()
    {
        var totalValue = _mods.Sum(t => t.Value) + _statType.DefaultValue + _statData.Value;
        return Math.Max(totalValue, _statType.MinimumValue);
    }

    public void AddStatMod(StatMod statMod)
    {
        _mods.Add(statMod);
    }

    public void RemoveStatMod(StatMod statMod)
    {
        _mods.Remove(statMod);
    }

    public void ModifyStatData(int amount)
    {
        _statData.Value += amount;
    }
}
