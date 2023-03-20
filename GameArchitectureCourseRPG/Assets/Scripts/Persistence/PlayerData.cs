using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    public string PlayerName;
    public List<StatData> StatDatas;

    public PlayerData()
    {
        StatDatas = new List<StatData>();
    }
}