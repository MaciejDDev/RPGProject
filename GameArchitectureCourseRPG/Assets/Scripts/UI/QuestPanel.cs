using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestPanel : ToggleablePanel
{
    [SerializeField] Quest _selectedQuest;
    [SerializeField] Step _selectedStep;
    [SerializeField] TMP_Text _nameText;
    [SerializeField] TMP_Text _descriptionText;
    [SerializeField] TMP_Text _currentObjectivesText;

    [SerializeField] Image _iconImage;
    [ContextMenu(itemName:"Bind")]
    public void Bind()
    {
        _iconImage.sprite = _selectedQuest.Sprite;
        _nameText.SetText(_selectedQuest.DisplayName);
        _descriptionText.SetText(_selectedQuest.Description);

        _selectedStep = _selectedQuest.Steps.FirstOrDefault();
        DisplayStepInstructionsAndObjectives();
        Show();
    }

    public void SelectQuest(Quest quest)
    {
        _selectedQuest = quest;
        Bind();
    }

    private void DisplayStepInstructionsAndObjectives()
    {
        StringBuilder builder = new StringBuilder();
        if (_selectedStep != null)
        {
            builder.AppendLine(_selectedStep.Instructions);
            foreach (var objective in _selectedStep.Objectives)
            {
                builder.AppendLine(objective.ToString());

            }
        }
        _currentObjectivesText.SetText(builder.ToString());
    }
}
