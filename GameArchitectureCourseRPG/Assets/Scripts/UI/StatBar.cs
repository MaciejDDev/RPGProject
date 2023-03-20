using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatBar : MonoBehaviour
{
    [SerializeField] StatType _statType;
    [SerializeField] TMP_Text _valueText;
    [SerializeField] Image _fill;
    StatsManager _statsManager;

    private void Awake()
    {
        _statsManager =FindObjectOfType<StatsManager>();
    }
    private void Update()
    {
        if (Player.ActivePlayer == null || Player.ActivePlayer.StatsManager == null)
            return;    
            
        var value = Player.ActivePlayer.StatsManager.GetStatValue(_statType);
        
        _valueText.SetText(value.ToString("N" + _statType.AllowedDecimals));

        var percent = value / _statsManager.GetStatValue(_statType.Maximum);
        _fill.fillAmount = percent;
    }
}
