using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatEffectArea : MonoBehaviour
{
    [SerializeField] Stat _stat;
    [SerializeField] int _amount;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"));
        return;

        StatsManager.Instance.Modify(_stat, _amount);
    
    }


    void OnTriggerExit(Collider other)
    {
        
    }
}
