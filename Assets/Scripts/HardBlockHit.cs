using System.Collections;
using UnityEngine;

public class HardBlockHit : MonoBehaviour
{
    public int MaxHits = 1;
    public Sprite emptyBlock;

    private SpriteRenderer spriteRenderer;

    public GameObject item;
    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (transform.DotTest(collision.transform, Vector2.down))
            {
                Hit();
            }
        }
    }

    private void Hit()
    {
        if (MaxHits > 0)
        {
            StartCoroutine(Bounce());

            if (item != null)
            {
                // Adds Coin and increments the coin count
                Instantiate(item, transform.position, Quaternion.identity);
            }

            MaxHits--;
        }
    }

    private IEnumerator Bounce()
    {
        spriteRenderer.enabled = true;
        Vector3 currentPosition = transform.position;
        Vector3 animatingPosition = currentPosition + Vector3.up * 0.5f;

        yield return Animate(currentPosition, animatingPosition);
        yield return Animate(animatingPosition, currentPosition);
        spriteRenderer.sprite = emptyBlock;
    }

    private IEnumerator Animate(Vector3 from, Vector3 to)
    {
        float duration = 0.1f;
        float startTime = Time.time;
        float endTime = startTime + duration;

        while (Time.time < endTime)
        {
            float t = (Time.time - startTime) / duration;
            transform.position = Vector3.Lerp(from, to, t);
            yield return null;
        }
        
        transform.position = to;
    }
}
