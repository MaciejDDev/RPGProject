using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    [SerializeField]  Stat[] _allStats;

    Dictionary<Stat, StatData> _myStats = new Dictionary<Stat, StatData>();
    List<StatData> _statDatas;


    public bool Bound { get; private set; }
    public static StatsManager Instance { get; private set; }
    void OnValidate() => _allStats = Extensions.GetAllInstances<Stat>();
    void Awake() => Instance = this;

    public int GetStatValue(Stat stat)
    {
        if (_myStats.TryGetValue(stat, out var statData)) 
            return statData.Value;
        
        return 0;
    }

    public void Bind(List<StatData> statDatas)
    {
        _statDatas = statDatas;
        foreach (var stat in _allStats)
        {
            var data = _statDatas.FirstOrDefault(t => t.Name == stat.name);
            if (data != null)
            {
                _myStats[stat] = data;
            }
            else
            {
                var statData = new StatData { Value = stat.DefaultValue, Name = stat.name };
                _statDatas.Add(statData);
                _myStats[stat] = statData;
            }
        }
        Bound = true;
    }

    public void Modify(Stat stat, int amount)
    {
        if(_myStats.TryGetValue(stat, out var statData))
        {
            statData.Value += amount;
        }
    }

    public IEnumerable<StatData> GetAll()
    {
        return _myStats.Values;
    }
}