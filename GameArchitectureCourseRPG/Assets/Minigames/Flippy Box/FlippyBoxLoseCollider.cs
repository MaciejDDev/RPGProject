using UnityEngine;

public class FlippyBoxLoseCollider : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
            Debug.Log("You lose!!");
    }
}
