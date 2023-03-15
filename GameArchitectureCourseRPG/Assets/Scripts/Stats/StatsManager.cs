using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    [SerializeField]  Stat[] _allStats;

    Dictionary<Stat, StatData> _myStatDatas = new Dictionary<Stat, StatData>();
    Dictionary<Stat, List<StatMod>> _myStatMods = new Dictionary<Stat, List<StatMod>>();
    List<StatData> _statDatas;


    public bool Bound { get; private set; }
    public static StatsManager Instance { get; private set; }
    void OnValidate() => _allStats = Extensions.GetAllInstances<Stat>();
    void Awake() => Instance = this;

    public int GetStatValue(Stat stat)
    {
        var totalValue = 0;
        int modValue = 0;
        if (_myStatMods.ContainsKey(stat))
            modValue = _myStatMods[stat].Sum(t => t.Value);

        if (_myStatDatas.TryGetValue(stat, out var statData)) 
            totalValue = statData.Value + modValue + stat.DefaultValue;
        else
            totalValue = modValue + stat.DefaultValue;

        return Math.Max(totalValue, stat.MinimumValue);
    }

    public void Bind(List<StatData> statDatas)
    {
        _statDatas = statDatas;
        foreach (var stat in _allStats)
        {
            var data = _statDatas.FirstOrDefault(t => t.Name == stat.name);
            if (data != null)
            {
                _myStatDatas[stat] = data;
            }
            else
            {
                var statData = new StatData { Value = 0, Name = stat.name };
                _statDatas.Add(statData);
                _myStatDatas[stat] = statData;
            }
        }
        Bound = true;
    }

    public void Modify(Stat stat, int amount)
    {
        if(_myStatDatas.TryGetValue(stat, out var statData))
        {
            statData.Value += amount;
        }
    }

    public IEnumerable<StatData> GetAll()
    {
        return _myStatDatas.Values;
    }

    public void AddStatMods(List<StatMod> statMods)
    {
        foreach (var statMod in statMods)
        {
            if (_myStatMods.TryGetValue(statMod.StatType, out var existingMods))
                existingMods.Add(statMod);
            else
                _myStatMods.Add(statMod.StatType, new List<StatMod> { statMod });
        }
    }

    public void RemoveStatMods(List<StatMod> statMods)
    {
        foreach (var statMod in statMods)
        {
            if (_myStatMods.TryGetValue(statMod.StatType, out var existingMods))
                existingMods.Remove(statMod);
        }
    }
}