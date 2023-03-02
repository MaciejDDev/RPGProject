using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    [SerializeField]  Stat[] _allStats;

    Dictionary<Stat, int> _myStats = new Dictionary<Stat, int>();

    
    
    
    public static StatsManager Instance { get; private set; }
    void OnValidate() => _allStats = Extensions.GetAllInstances<Stat>();
    void Awake()
    {
        Instance = this;
        foreach (Stat stat in _allStats)
        {
            _myStats.Add(stat, stat.DefaultValue);
        }
    }

    public int GetStatValue(Stat stat)
    {
        if (_myStats.TryGetValue(stat, out int value)) 
            return value;
        
        return 0;
    }

}