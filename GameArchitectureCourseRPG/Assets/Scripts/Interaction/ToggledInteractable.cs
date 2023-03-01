using UnityEngine;

public class ToggledInteractable : Interactable
{
    [SerializeField] InteractionType _toggledinteractionType;
    bool _toggleState;

    public override InteractionType InteractionType => _toggleState ? _toggledinteractionType : _interactionType;


    protected override void CompleteInteraction()
    {
        SendInteractionComplete();
        _toggleState = !_toggleState;
        _data.TimeInteracted = 0f;
    }
}
