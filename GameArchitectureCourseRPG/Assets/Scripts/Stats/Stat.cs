using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Stat
{
    public StatType StatType { get; private set; }
    StatData _statData;
    List<StatMod> _mods = new List<StatMod>();

    public string Name => StatType.name;


    public Stat( StatType statType, StatData statData)
    {
        _statData = statData;
        StatType = statType;
        
    }

    public float GetValue()
    {
        var totalValue = _mods.Sum(t => t.Value) + _statData.Value;
        totalValue = Math.Max(totalValue, StatType.MinimumValue);
        if (StatType.AllowedDecimals < 1)
            return Mathf.RoundToInt(totalValue);

        return totalValue;
    }

    public void AddStatMod(StatMod statMod)
    {
        _mods.Add(statMod);
    }

    public void RemoveStatMod(StatMod statMod)
    {
        _mods.Remove(statMod);
    }

    public void ModifyStatData(float amount)
    {
        var newValue = _statData.Value + amount;
        if (StatType.Maximum != null)
        {
            var maxValue = StatsManager.Instance.GetStatValue(StatType.Maximum);
            if (newValue > maxValue) 
                newValue = maxValue;
        }
        if (newValue < StatType.MinimumValue)
            newValue = StatType.MinimumValue;

        _statData.Value = newValue;
    }
}
