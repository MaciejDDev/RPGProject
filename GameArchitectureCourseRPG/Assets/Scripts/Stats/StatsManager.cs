using System;
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

    void Start()
    {
        foreach (var slot in Inventory.Instance.EquipmentSlots)
        {
            slot.Changed += HandleEquipSlotChanged;

            if (slot.Item != null)
                Add(slot.Item);
        }    
    }

    void HandleEquipSlotChanged(Item added, Item removed)
    {
        if (added == removed)
            return;

        if (removed)
            Remove(removed);
        if (added)
            Add(added);
    }

    private void Remove(Item removed)
    {
        foreach (var statMod in removed.StatMods)
            GetStat(statMod.StatType).RemoveStatMod(statMod);
    }

    private void Add(Item added)
    {
        foreach (var statMod in added.StatMods)
            GetStat(statMod.StatType).AddStatMod(statMod);
    }

    public float GetStatValue(StatType StatType) => GetStat(StatType).GetValue();

    Stat GetStat(StatType statType) => _stats[statType];

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

    public void Modify(StatType statType, float amount) => GetStat(statType).ModifyStatData(amount);

    public IEnumerable<Stat> GetAll() => _stats.Values;

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