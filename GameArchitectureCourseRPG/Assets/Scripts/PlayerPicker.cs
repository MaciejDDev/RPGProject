using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPicker : MonoBehaviour
{

    [SerializeField] List<Player> _players;
    [SerializeField] KeyCode _swapKey = KeyCode.Tab;
    int _playerIndex;
    [SerializeField] CinemachineVirtualCamera _camera;

    void Update()
    {
        if (Input.GetKeyDown(_swapKey))
        {
            ActivateNextPlayer();
        }
    }

    void ActivateNextPlayer()
    {
        _players[_playerIndex].GetComponent<ThirdPersonMover>().enabled = false;
        
        _playerIndex++;
        if (_playerIndex >= _players.Count)
            _playerIndex = 0;

        var playerToActivate = _players[_playerIndex];
        playerToActivate.GetComponent<ThirdPersonMover>().enabled = true;
        _camera.Follow = playerToActivate.Shoulders;
    }
}
