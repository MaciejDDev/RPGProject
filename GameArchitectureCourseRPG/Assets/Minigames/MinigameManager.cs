using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    Action _completeInspection;

    public static MinigameManager Instance { get; private set; }


    private void Awake()
    {
        Instance= this;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            _completeInspection?.Invoke();
            _completeInspection= null;
        }
    }
    public void StartMinigame(MinigameSettings settings, Action<MinigameResult> completeInspection)
    {
        if (settings is FlippyBoxMinigameSettings flippyBoxMinigameSettings)    
            FlippyboxMinigame.Instance.StartMinigame(flippyBoxMinigameSettings, completeInspection);
        else if (settings is WinLoseMinigameSettings winLoseMinigameSettings)
            WinLoseMinigamePanel.Instance.StartMinigame(completeInspection);
    }

    
}
