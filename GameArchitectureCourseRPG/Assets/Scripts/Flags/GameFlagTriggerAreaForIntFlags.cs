using UnityEngine;

public class GameFlagTriggerAreaForIntFlags : MonoBehaviour
{
    [SerializeField] IntGameFlag _gameFlag;
    [SerializeField] int _amount;
    private void OnTriggerEnter(Collider other) => _gameFlag.Modify(_amount);
}