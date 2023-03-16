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

    private void Update()
    {
        var value = StatsManager.Instance.GetStatValue(_statType);
        _valueText.SetText(value.ToString("N" + _statType.AllowedDecimals));

        var percent = value / StatsManager.Instance.GetStatValue(_statType.Maximum);
        _fill.fillAmount = percent;
    }
}
