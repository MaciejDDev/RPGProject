using System.Collections;
using System.Linq;
using UnityEngine;

public class GamePersistence : MonoBehaviour
{
    public GameData _gameData;
   
    
    void Start() => LoadGame();
    void OnDisable() => SaveGame();

    void SaveGame()
    {
        Debug.Log("Saving Game DATA");
        var json = JsonUtility.ToJson(_gameData);
        PlayerPrefs.SetString("GameData", json);
        Debug.Log( json );
        Debug.Log("Saving Game DATA Completed");
    }

    void LoadGame()
    {
        var json = PlayerPrefs.GetString("GameData");
        _gameData = JsonUtility.FromJson<GameData>(json); //new GameData();
        if(_gameData ==null)
            _gameData = new GameData();


        var players = FindObjectsOfType<Player>();
        foreach( Player player in players )
        {
            var playerData = _gameData.PlayerDatas.FirstOrDefault(t => t.PlayerName == player.name);
            if (playerData == null)
            {
                playerData = new PlayerData() { PlayerName = player.name };
                _gameData.PlayerDatas.Add(playerData);
            }
            player.Bind(playerData);
        }

        //StatsManager.Instance.Bind(_gameData.StatDatas);
        Inventory.Instance.Bind(_gameData.SlotDatas);
        FlagManager.Instance.Bind(_gameData.GameFlagDatas);
        InteractionManager.Bind(_gameData.InteractableDatas);
        PlacementManager.Instance.Bind(_gameData.PlaceableDatas);
        FindObjectOfType<PlayerPicker>().Initialize();
    }
}
