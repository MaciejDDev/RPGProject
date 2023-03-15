using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class StatsManager : MonoBehaviour
{
    [SerializeField]  StatType[] _allStatTypes;

    //Dictionary<StatType, StatData> _myStatDatas = new Dictionary<StatType, StatData>();
    //Dictionary<StatType, List<StatMod>> _myStatMods = new Dictionary<StatType, List<StatMod>>();
    Dictionary<StatType, Stat> _stats = new Dictionary<StatType, Stat>();
    List<StatData> _statDatas;


    public bool Bound { get; private set; }
    public static StatsManager Instance { get; private set; }
    void OnValidate() => _allStatTypes = Extensions.GetAllInstances<StatType>();
    void Awake() => Instance = this;

    public int GetStatValue(StatType StatType)
    {
        var stat = GetStat(StatType);
        return stat.GetValue();
    }

    Stat GetStat(StatType statType)
    {
        return _stats[statType];
    }

    public void Bind(List<StatData> statDatas)
    {
        _statDatas = statDatas;
        foreach (var statType in _allStatTypes)
        {
            var data = _statDatas.FirstOrDefault(t => t.Name == statType.name);
            if (data == null)
            {
                var statData = new StatData { Value = 0, Name = statType.name };
                _statDatas.Add(statData);
            }
            _stats.Add(statType, new Stat(statType, data));
        }
        Bound = true;
    }

    public void Modify(StatType statType, int amount)
    {
        GetStat(statType).ModifyStatData(amount);
    }

    public IEnumerable<Stat> GetAll()
    {
        return _stats.Values;
    }

    public void AddStatMods(List<StatMod> statMods)
    {
        foreach (var statMod in statMods)
            GetStat(statMod.StatType).AddStatMod(statMod);
    }

    public void RemoveStatMods(List<StatMod> statMods)
    {
        foreach (var statMod in statMods)
            GetStat(statMod.StatType).RemoveStatMod(statMod);
    }
}