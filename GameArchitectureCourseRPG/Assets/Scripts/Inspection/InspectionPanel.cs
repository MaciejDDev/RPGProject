using System;
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
    [SerializeField] TMP_Text _completedInspectionText;


    private void OnEnable()
    {
        _hintText.enabled = false;
        _completedInspectionText.enabled = false;
        Inspectable.InspectablesInRangeChanged += UpdateTextState;
        Inspectable.AnyInspectionComplete += ShowCompletedInspectionText;
    }

    private void ShowCompletedInspectionText(Inspectable inspectable, string completedInspectionMessage)
    {
        _completedInspectionText.SetText(completedInspectionMessage);
        _completedInspectionText.enabled= true;
        float messageTime = completedInspectionMessage.Length / 5f;
        messageTime= Mathf.Clamp(messageTime, 3f, 10f);
        StartCoroutine(FadecompletedText(messageTime));
    }

    IEnumerator FadecompletedText(float messageTime)
    {
        _completedInspectionText.alpha= 1f;
        while (_completedInspectionText.alpha > 0)
        {
            yield return null;
            _completedInspectionText.alpha -= Time.deltaTime / messageTime;
        }
        _completedInspectionText.enabled= false;
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
