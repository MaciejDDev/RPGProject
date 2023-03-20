using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsPanel : ToggleablePanel
{

    [SerializeField] StatEntry _statEntryPrefab;
    [SerializeField] Transform _statPanel;
    List<StatEntry> _statEntries = new List<StatEntry>();

    public void Bind(StatsManager statsManager)
    {
        foreach (var entry in _statEntries)
            Destroy(entry.gameObject);
        _statEntries.Clear();

        var allStats = statsManager.GetAll();
        foreach (var stat in allStats)
        {
            var statEntry = Instantiate(_statEntryPrefab, _statPanel);
            statEntry.Bind(stat);
            _statEntries.Add(statEntry);
        }
    }
}
