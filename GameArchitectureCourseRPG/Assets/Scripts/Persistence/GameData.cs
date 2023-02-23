using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
    public List<GameFlagData> GameFlagDatas;
    public List<InteractableData> InteractableDatas;
    public List<SlotData> SlotDatas;
    public List<PlaceableData> PlaceableDatas;

    public GameData()
    {
        GameFlagDatas = new List<GameFlagData>();
        InteractableDatas= new List<InteractableData>();
        SlotDatas = new List<SlotData>();
        PlaceableDatas = new List<PlaceableData>();
    }
}

