using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="Minigame/Flippy Box Settings",fileName = "FlippyBoxSettings-")]
public class FlippyBoxMinigameSettings : MinigameSettings
{
    public float MovingBlockSpeed = 5f;
    public Vector2 JumpVelocity = Vector2.up + Vector2.right;
    public float GrowTime = 20f;
    public Color PlayerColor = Color.green;
}
