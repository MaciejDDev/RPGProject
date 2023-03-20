using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsPanel : ToggleablePanel
{

    [SerializeField] StatEntry _statEntryPrefab;
    [SerializeField] Transform _statPanel;


    
    IEnumerator Start()
    {
        var statsManager = FindObjectOfType<StatsManager>();
        while (!statsManager.Bound)
            yield return null;  
     
        var allStats = statsManager.GetAll();
        foreach (var stat in allStats)
        {
            var statEntry = Instantiate(_statEntryPrefab, _statPanel);
            statEntry.Bind(stat);
        }
    }




}
