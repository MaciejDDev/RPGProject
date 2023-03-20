using System;
using System.Collections.Generic;
using Unity.VisualScripting;

[Serializable]
public class GameData
{
    public List<GameFlagData> GameFlagDatas;
    public List<InteractableData> InteractableDatas;
    public List<SlotData> SharedSlotDatas;
    public List<PlaceableData> PlaceableDatas;
    public List<PlayerData> PlayerDatas;

    public GameData()
    {
        GameFlagDatas = new List<GameFlagData>();
        InteractableDatas= new List<InteractableData>();
        SharedSlotDatas = new List<SlotData>();
        PlaceableDatas = new List<PlaceableData>();
        PlayerDatas = new List<PlayerData>();
    }
}
