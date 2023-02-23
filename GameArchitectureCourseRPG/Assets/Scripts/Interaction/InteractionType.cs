using UnityEngine;

[CreateAssetMenu(menuName = "Interaction Type")]
public class InteractionType : ScriptableObject
{
    public KeyCode HotKey = KeyCode.E;
    public string BeforeInteraction;
    public string DuringInteraction;
    public string CompletedInteraction;
    public string FailedInteraction;
    public bool IsDefault;
}
