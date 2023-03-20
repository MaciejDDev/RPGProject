using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatEffectArea : MonoBehaviour
{
    [SerializeField] List<StatMod> _statMods = new List<StatMod>();


    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        var statsManager = other.GetComponent<StatsManager>();
        if (statsManager)
        statsManager.AddStatMods(_statMods);
    
    }


    void OnTriggerExit(Collider other)
    {
        var statsManager = other.GetComponent<StatsManager>();
        if (statsManager)
            statsManager.RemoveStatMods(_statMods);

    }
}
