using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] Quest _quest;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            QuestManager.Instance.AddQuest(_quest);
        }
    }
}
