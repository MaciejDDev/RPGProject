using UnityEngine;

public class ToggledInteractable : Interactable
{
    [SerializeField] InteractionType _toggledinteractionType;
    bool toggleState => _data.InteractionCount % 2 == 1;

    public override InteractionType InteractionType => toggleState ? _toggledinteractionType : _interactionType;


    protected override void AfterCompleteInteraction()
    {
        _data.TimeInteracted = 0f;
    }
    protected override void OnBound()
    {
        if (toggleState)
        {
            RestoreInteractionState();
        }
    }
}
