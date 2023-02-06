using System;
using UnityEngine;

public class FlippyboxMinigame : MonoBehaviour
{

    [SerializeField] FlippyBoxMinigameSettings _defaultSettings;

    Action<MinigameResult> _completeInspection;

    public static FlippyboxMinigame Instance { get; private set; }
    public FlippyBoxMinigameSettings CurrentSettings { get; private set; }


    void Awake() => Instance = this;
    void Start()
    {
        gameObject.SetActive(false);
        if(transform.parent == null) 
            StartMinigame(_defaultSettings, (result) => Debug.Log(result));
    }

    public void StartMinigame(FlippyBoxMinigameSettings settings, Action<MinigameResult> completeInspection)
    {
        CurrentSettings = settings ?? _defaultSettings;
        _completeInspection = completeInspection;
       
        foreach (var restartable in GetComponentsInChildren<IRestart>())
        {
            restartable.Restart();
        }
        gameObject.SetActive(true);
    }

    public void Win()
    {
        _completeInspection?.Invoke(MinigameResult.Won);
        _completeInspection = null;
        gameObject.SetActive(false);
    }
    public void Lose()
    {
        _completeInspection?.Invoke(MinigameResult.Lost);
        _completeInspection = null;
        gameObject.SetActive(false);
    }
}
