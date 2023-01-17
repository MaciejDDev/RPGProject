using System.Collections.Generic;
using UnityEngine;

public class ToggleablePanel : MonoBehaviour
{
    CanvasGroup _canvasGroup;
    static HashSet<ToggleablePanel> _visiblePanels = new HashSet<ToggleablePanel>();

    public static bool IsVisible => _visiblePanels.Count > 0; 

    void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        Hide();
    }

    public void Show()
    {
        _visiblePanels.Add(item: this);
        _canvasGroup.alpha = 0.5f;
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