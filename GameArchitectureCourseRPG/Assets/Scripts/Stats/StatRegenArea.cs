using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatRegenArea : MonoBehaviour
{
    [SerializeField] StatType _stat;
    [SerializeField] float _amountPerSecond = 1;

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;


        var amount = Time.deltaTime * _amountPerSecond;
        var statsManager = other.GetComponent<StatsManager>();
        if (statsManager)
            statsManager.Modify(_stat, amount);

    }
}
