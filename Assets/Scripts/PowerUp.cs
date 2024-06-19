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
        audioSource.volume = 0.75f;
        switch (type)
        {
            case Type.Coin:
                audioSource.clip = coinSound;
                GameManager.Instance.AddCoin();
                break;
            case Type.ExtraLife:
                audioSource.clip = extraLifeSound;
                GameManager.Instance.AddLives(1);
                break;
            case Type.MagicMushroom:
                player.GetComponent<Player>().Grow();
                break;
            case Type.StarPower:
                player.GetComponent<Player>().ActivateStarPower();
                break;
        }

        audioSource.Play();

        // hide the power up
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        Destroy(gameObject, 2f);
    }
}
