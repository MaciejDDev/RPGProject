using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;
using System;

public class DialogController : MonoBehaviour
{
    [SerializeField] TextAsset _dialog;
    [SerializeField] TMP_Text _storyText;
    [SerializeField] Button[] _choiceButtons;


    Story _story;
    // Start is called before the first frame update
    [ContextMenu("Start Dialog")]
    public void StartDialog()
    {
        _story = new Story(_dialog.text);
        RefreshView();
    }

    private void RefreshView()
    {
        StringBuilder storyTextBuilder = new StringBuilder();
        while(_story.canContinue)
            storyTextBuilder.AppendLine(_story.Continue());

        _storyText.SetText(storyTextBuilder);

        for (int i = 0; i < _choiceButtons.Length; i++)
        {
            var button = _choiceButtons[i];
            button.gameObject.SetActive(i < _story.currentChoices.Count);
            button.onClick.RemoveAllListeners();
            if(i < _story.currentChoices.Count)
            {
                var choice = _story.currentChoices[i];
                button.GetComponentInChildren<TMP_Text>().SetText(choice.text);
                button.onClick.AddListener(call: () => 
                {
                    _story.ChooseChoiceIndex(choice.index);
                    RefreshView();
                });
            }
        }
    }

}
