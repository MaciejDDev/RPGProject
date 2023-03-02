using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance { get; private set; }
    void Awake() => Instance = this;

    public int GetStatValue(Stat stat)
    {
        return 1;
    }

}