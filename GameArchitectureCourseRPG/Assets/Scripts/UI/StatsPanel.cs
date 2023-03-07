using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsPanel : ToggleablePanel
{

    [SerializeField] StatEntry _statEntryPrefab;
    [SerializeField] Transform _statPanel;


    
    IEnumerator Start()
    {
        while (!StatsManager.Instance.Bound)
            yield return null;  
     
        var allStats = StatsManager.Instance.GetAll();
        foreach (var stat in allStats)
        {
            var statEntry = Instantiate(_statEntryPrefab, _statPanel);
            statEntry.Bind(stat);
        }
    }




}
