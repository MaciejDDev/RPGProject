using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Game Event")]
public class GameEvent : ScriptableObject
{
    List<GameEventListener> _gameEventListeners = new List<GameEventListener>();

    public void Register(GameEventListener gameEventListener) => _gameEventListeners.Add(gameEventListener);
    public void Deregister(GameEventListener gameEventListener) => _gameEventListeners.Remove(gameEventListener);

    [ContextMenu("Invoke")]

    public void Invoke()
    {
        foreach (var gameEventListener in _gameEventListeners)
        {
            gameEventListener.RaiseEvent();
        }
    }

}
