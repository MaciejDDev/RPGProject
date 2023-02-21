using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class ToggleablePanel : MonoBehaviour
{
    [SerializeField] KeyCode HotKey;

    CanvasGroup _canvasGroup;
    static HashSet<ToggleablePanel> _visiblePanels = new HashSet<ToggleablePanel>();

    public static bool AnyVisible => _visiblePanels.Count > 0;

    public bool IsVisible => _canvasGroup.alpha > 0f;

    void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        Hide();
    }
    private void Update()
    {
        if (Input.GetKeyDown(HotKey))
            ToggleState();
        else if (Input.GetKeyDown(KeyCode.Escape))
            Hide();
    }

    private void ToggleState()
    {
        if (IsVisible)
            Hide();
        else
            Show();   
    }

    public void Show()
    {
        _visiblePanels.Add(item: this);
        _canvasGroup.alpha = 1f;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
    }
    public void Hide()
    {
        _visiblePanels.Remove(item: this);
        _canvasGroup.alpha = 0.0f;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }
}