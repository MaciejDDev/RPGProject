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



    private void Start()
    {

        for (int i = 0; i < _players.Count; i++)
        {
            _players[i].ToggleActive(i==0);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(_swapKey))
        {
            ActivateNextPlayer();
        }
    }

    void ActivateNextPlayer()
    {
        _players[_playerIndex].ToggleActive(false);
        
        
        _playerIndex++;
        if (_playerIndex >= _players.Count)
            _playerIndex = 0;

        var playerToActivate = _players[_playerIndex];
        playerToActivate.ToggleActive(true);
        _camera.Follow = playerToActivate.Shoulders;
    }
}
