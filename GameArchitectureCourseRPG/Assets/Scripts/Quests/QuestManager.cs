using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] QuestPanel _questPanel;
    [SerializeField] List<Quest> _allQuests;

    private List<Quest> _activeQuests = new List<Quest>();

    public static QuestManager Instance { get; private set; }

    void Awake() => Instance = this;
    public void AddQuest(Quest quest)
    {
        _activeQuests.Add(quest);
        _questPanel.SelectQuest(quest);
    }

    public void AddQuestByName(string questName)     
    {
        var quest = _allQuests.FirstOrDefault(tag => tag.name == questName);
        if (quest != null)
            AddQuest(quest);
        else
            Debug.LogError(message:$"Missing quest {questName} attemped to add from dialog");
    }

    [ContextMenu("Progress Quests")]
    public void ProgressQuests()
    {
        foreach (var quest in _activeQuests)
        {
            quest.TryProgress();
        }
    }
}
