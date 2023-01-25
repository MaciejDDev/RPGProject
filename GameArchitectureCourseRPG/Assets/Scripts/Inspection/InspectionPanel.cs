using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InspectionPanel : MonoBehaviour
{
    [SerializeField] TMP_Text _hintText;
    //[SerializeField] TMP_Text _progressText;
    [SerializeField] Image _progressBarFilledImage;
    [SerializeField] GameObject _progressBar;


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
        {
            //_progressText.SetText(InspectionManager.InspectionProgress.ToString());
            _progressBarFilledImage.fillAmount= InspectionManager.InspectionProgress;
            _progressBar.SetActive(true);
        }
        else if(_progressBar.activeSelf)
        {
            _progressBar.SetActive(false);
        }
    }
}
