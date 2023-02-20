using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
    public List<GameFlagData> GameFlagDatas;
    public List<InspectableData> InspectableDatas;
    public List<SlotData> SlotDatas;
    public List<PlaceableData> PlaceableDatas;

    public GameData()
    {
        GameFlagDatas = new List<GameFlagData>();
        InspectableDatas= new List<InspectableData>();
        SlotDatas = new List<SlotData>();
        PlaceableDatas = new List<PlaceableData>();
    }
}

