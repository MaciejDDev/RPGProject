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
        Debug.Log( json );
        Debug.Log("Saving Game DATA Completed");
    }

    void LoadGameFlags()
    {
        _gameData = new GameData();
        FlagManager.Instance.Bind(_gameData._gameFlagDatas);
    }
}
