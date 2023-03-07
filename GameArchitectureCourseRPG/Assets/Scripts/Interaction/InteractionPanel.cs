using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionPanel : MonoBehaviour
{
    [SerializeField] TMP_Text _conditionText;
    [SerializeField] TMP_Text _beforeText;
    [SerializeField] TMP_Text _duringText;
    [SerializeField] TMP_Text _completedText;
    //[SerializeField] TMP_Text _progressText;
    [SerializeField] Image _progressBarFilledImage;
    [SerializeField] GameObject _progressBar;


    private void OnEnable()
    {
        _beforeText.enabled = false;
        _completedText.enabled = false;
        //Interactable.InteractablesInRangeChanged += UpdateTextState;
        FindObjectOfType<InteractionManager>().CurrentInteractableChanged += UpdateInteractionText;
        Interactable.AnyInteractionComplete += ShowCompletedInspectionText;
    }

    void UpdateInteractionText(Interactable interactable)
    {
        if (interactable == null)
        {
            _conditionText.enabled = false;
            _beforeText.enabled = false;
        }
        else
        {
            var interactionType = interactable.InteractionType;
            _beforeText.SetText($"{interactionType.HotKey} - {interactionType.BeforeInteraction}");
            _beforeText.enabled = true;
            _duringText.SetText(interactionType.DuringInteraction);
            _conditionText.enabled = true;
            _conditionText.SetText(interactable.ConditionMessage);
        }
    }

    private void ShowCompletedInspectionText(Interactable inspectable, string completedInspectionMessage)
    {
        _completedText.SetText(completedInspectionMessage);
        _completedText.enabled= true;
        float messageTime = completedInspectionMessage.Length / 5f;
        messageTime= Mathf.Clamp(messageTime, 3f, 10f);
        StartCoroutine(FadecompletedText(messageTime));
    }

    IEnumerator FadecompletedText(float messageTime)
    {
        _completedText.alpha= 1f;
        while (_completedText.alpha > 0)
        {
            yield return null;
            _completedText.alpha -= Time.deltaTime / messageTime;
        }
        _completedText.enabled= false;
    }

    void OnDisable() => Interactable.InteractablesInRangeChanged -= UpdateTextState;

    void UpdateTextState(bool enableHint) => _beforeText.enabled = enableHint;

    private void Update()
    {
        if(InteractionManager.Interacting)
        {
            //_progressText.SetText(InspectionManager.InspectionProgress.ToString());
            _progressBarFilledImage.fillAmount= InteractionManager.InteractionProgress;
            _progressBar.SetActive(true);
        }
        else if(_progressBar.activeSelf)
        {
            _progressBar.SetActive(false);
        }
    }
}
