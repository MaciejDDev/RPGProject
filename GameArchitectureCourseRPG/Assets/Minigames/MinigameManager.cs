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
    public void StartMinigame(FlippyBoxMinigameSettings settings, Action<MinigameResult> completeInspection)
    {
        FlippyboxMinigame.Instance.StartMinigame(settings, completeInspection);
   
    }

    internal void StartMinigame(object minigameSettings, Action<MinigameResult> handleMinigameCompleted)
    {
        throw new NotImplementedException();
    }
}
