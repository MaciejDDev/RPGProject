using System.Collections;
using TMPro;
using UnityEngine;

public class InspectionPanel : MonoBehaviour
{
    [SerializeField] TMP_Text _hintText;
    [SerializeField] TMP_Text _progressText;


    private void OnEnable()
    {
        _hintText.enabled = false;
        Inspectable.InspectablesInRangeChanged += UpdateTextState;
    }

    void OnDisable() => Inspectable.InspectablesInRangeChanged -= UpdateTextState;

    void UpdateTextState(bool enableHint) => _hintText.enabled = enableHint;

    private void Update()
    {
        if(InspectionManager.Inspecting)
        _progressText.SetText(InspectionManager.InspectionProgress.ToString());
    }
}
