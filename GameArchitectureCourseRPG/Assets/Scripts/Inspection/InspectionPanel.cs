using System.Collections;
using TMPro;
using UnityEngine;

public class InspectionPanel : MonoBehaviour
{
    [SerializeField] TMP_Text _hintText;

    private void OnEnable()
    {
        _hintText.enabled = false;
        Inspectable.InspectablesInRangeChanged += UpdateTextState;
    }

    void OnDisable() => Inspectable.InspectablesInRangeChanged -= UpdateTextState;

    void UpdateTextState(bool enableHint) => _hintText.enabled = enableHint;
}
