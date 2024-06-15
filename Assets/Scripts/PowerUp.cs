using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum Type
    {
        Coin,
        ExtraLife,
        MagicMushroom,
        StartPower
    }

    public Type type;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Collect(collider.gameObject);
        }
    }

    private void Collect(GameObject player)
    {
        switch (type)
        {
            case Type.Coin:
                GameManager.Instance.AddCoin();
                break;
            case Type.ExtraLife:
                GameManager.Instance.AddLives(1);
                break;
            case Type.MagicMushroom:
                player.GetComponent<Player>().Grow();
                break;
            case Type.StartPower:
                break;
        }

        Destroy(gameObject);
    }
}
