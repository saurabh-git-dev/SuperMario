using UnityEngine;

public class Koopa : MonoBehaviour
{
    public Sprite shellSprite;

    public float ShellSpeed = 12f;
    public bool IsShelled { get; private set; }
    public bool IsPushed { get; private set; }

    private void OnCollisionEnter2D (Collision2D collision)
    {
        if (!IsShelled && collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();

            if (collision.transform.DotTest(transform, Vector2.down)) {
                EnterShell();
            } else {
                player.Hit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (IsShelled && collider.CompareTag("Player"))
        {
            if (!IsPushed) 
            {
                Vector2 direction = new Vector2(transform.position.x - collider.transform.position.x, 0f).normalized;
                PushShell(direction);
            } else
            {
                Player player = collider.gameObject.GetComponent<Player>();
                player.Hit();
            }
        } 
        else if (!IsShelled && collider.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {
            Hit();
        }
    }

    private void EnterShell() 
    {
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = shellSprite;
        IsShelled = true;
    }

    private void PushShell(Vector2 direction) 
    {
        GetComponent<Rigidbody2D>().isKinematic = false;
        EntityMovement entityMovement = GetComponent<EntityMovement>();
        entityMovement.enabled = true;
        entityMovement.direction = direction;
        entityMovement.speed = ShellSpeed;
        IsPushed = true;

        gameObject.layer = LayerMask.NameToLayer("Shell");
    }

    private void Hit()
    {
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 3f);
    }

    private void OnBecameInvisible()
    {
        if (IsPushed) {
            Destroy(gameObject);
        }
    }
}

