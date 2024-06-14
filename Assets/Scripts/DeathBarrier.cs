using UnityEngine;

public class DeathBarrier : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            collider.gameObject.SetActive(false);
            GameManager.Instance.ResetLevel(3f);
        } else {
            Destroy(collider.gameObject);
        }
    }
}
