using System;
using UnityEngine;

public class WinLoseMinigamePanel : MonoBehaviour
{
    private Action _completeInspection;

    public static WinLoseMinigamePanel Instance { get; private set; }


    private void Awake() => Instance = this;
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void StartMinigame(Action completeInspection)
    {
        _completeInspection = completeInspection;
        gameObject.SetActive(true);
    }

    public void Win()
    {
        _completeInspection?.Invoke();
        _completeInspection= null;
        gameObject.SetActive(false);
    }
    public void Lose()
    {
        _completeInspection = null;
        gameObject.SetActive(false);
    }
}