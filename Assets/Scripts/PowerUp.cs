using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public AudioClip coinSound;
    public AudioClip extraLifeSound;

    private AudioSource audioSource;

    public enum Type
    {
        Coin,
        ExtraLife,
        MagicMushroom,
        StarPower
    }

    public Type type;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

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
                audioSource.clip = coinSound;
                break;
            case Type.ExtraLife:
                GameManager.Instance.AddLives(1);
                audioSource.clip = extraLifeSound;
                break;
            case Type.MagicMushroom:
                player.GetComponent<Player>().Grow();
                break;
            case Type.StarPower:
                player.GetComponent<Player>().ActivateStarPower();
                break;
        }
        
        audioSource.Play();

        Destroy(gameObject);
    }
}
