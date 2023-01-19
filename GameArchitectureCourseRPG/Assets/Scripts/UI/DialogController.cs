using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;
using System;

public class DialogController : ToggleablePanel
{
    [SerializeField] TextAsset _dialog;
    [SerializeField] TMP_Text _storyText;
    [SerializeField] Button[] _choiceButtons;

    Story _story;
  

    // Start is called before the first frame update
    [ContextMenu("Start Dialog")]
    public void StartDialog(TextAsset dialog)
    {
        _story = new Story(dialog.text);
        RefreshView();
        Show();
    }

    

    private void RefreshView()
    {
        StringBuilder storyTextBuilder = new StringBuilder();
        while(_story.canContinue)
        {
            storyTextBuilder.AppendLine(_story.Continue());
            HandleTags();
        }
            
        _storyText.SetText(storyTextBuilder);

        if(_story.currentChoices.Count == 0)
            Hide();
        else

            ShowChoiceButtons();
    }

    void ShowChoiceButtons()
    {
        for (int i = 0; i < _choiceButtons.Length; i++)
        {
            var button = _choiceButtons[i];
            button.gameObject.SetActive(i < _story.currentChoices.Count);
            button.onClick.RemoveAllListeners();
            if (i < _story.currentChoices.Count)
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

    void HandleTags()
    {
        foreach (var tag in _story.currentTags)
        {
            if (tag.StartsWith("E."))
            {
                string eventName = tag.Remove(0, 2);
                GameEvent.RaiseEvent(eventName);
            }
            else if (tag.StartsWith("Q."))
            {
                string questName = tag.Remove(0, 2);
                QuestManager.Instance.AddQuestByName(questName);
            }
            else if (tag.StartsWith("F."))
            {
                //F.BrokenPanelsInspected.9
                //string flagName = tag.Remove(0, 2);
                var values = tag.Split('.');
                FlagManager.Instance.Set(values[1], values[2]);
            }

        }
    }
}
