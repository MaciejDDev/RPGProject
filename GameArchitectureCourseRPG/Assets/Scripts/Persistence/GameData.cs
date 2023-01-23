using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
    public List<GameFlagData> _gameFlagDatas;
    public GameData()
    {
        _gameFlagDatas = new List<GameFlagData>();
        //_gameFlagDatas.Add(new GameFlagData() { Value = "jason1", Name = "flagname" });

    }
}
