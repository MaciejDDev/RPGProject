using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
    public List<GameFlagData> _gameFlagDatas;
    public List<InspectableData> InspectableDatas;
    public GameData()
    {
        _gameFlagDatas = new List<GameFlagData>();
        InspectableDatas= new List<InspectableData>();
        //_gameFlagDatas.Add(new GameFlagData() { Value = "jason1", Name = "flagname" });

    }
}
