using System.Collections;
using UnityEngine;

public class GamePersistence : MonoBehaviour
{
    GameData _gameData;
   
    
    void Start() => LoadGameFlags();
    void OnDisable() => SaveGameFlags();

    void SaveGameFlags()
    {
        Debug.Log("Saving Game DATA");
        var json = JsonUtility.ToJson(_gameData);
        PlayerPrefs.SetString("GameData", json);
        Debug.Log( json );
        Debug.Log("Saving Game DATA Completed");
    }

    void LoadGameFlags()
    {
        var json = PlayerPrefs.GetString("GameData");
        _gameData = JsonUtility.FromJson<GameData>(json); //new GameData();
        if(_gameData ==null)
            _gameData = new GameData();

        FlagManager.Instance.Bind(_gameData._gameFlagDatas);
    }
}
