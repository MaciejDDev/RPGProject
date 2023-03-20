using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    public string PlayerName;
    public List<StatData> StatDatas;
    internal List<SlotData> SlotDatas;

    public PlayerData()
    {
        StatDatas = new List<StatData>();
        SlotDatas = new List<SlotData>();
    }
}