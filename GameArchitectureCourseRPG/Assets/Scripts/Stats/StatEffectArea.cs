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

        StatsManager.Instance.AddStatMods(_statMods);
    
    }


    void OnTriggerExit(Collider other)
    {
        StatsManager.Instance.RemoveStatMods(_statMods);

    }
}
