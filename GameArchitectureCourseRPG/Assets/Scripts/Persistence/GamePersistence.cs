using System.Collections;
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

        Inventory.Instance.Bind(_gameData.SlotDatas);
        FlagManager.Instance.Bind(_gameData.GameFlagDatas);
        InteractionManager.Bind(_gameData.InteractableDatas);
        PlacementManager.Instance.Bind(_gameData.PlaceableDatas);
    }
}
