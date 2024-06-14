using System.Collections;
using UnityEngine;

public class SoftBlockHit : MonoBehaviour
{
    public int MaxHitPoints = 0;

    public Sprite emptyBlock;

    private bool animating;

    public GameObject item;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (transform.DotTest(collision.transform, Vector2.down) && !animating)
            {
                Hit();
            }
        }
    }

    private void Hit()
    {
        Player player = FindObjectOfType<Player>();

        if (MaxHitPoints != 0 )
        {
            if (item != null) {
                // Adds Coin and increments the coin count
                Instantiate(item, transform.position, Quaternion.identity);
            }
            StartCoroutine(Bounce());

            MaxHitPoints--;
        } else if (player.IsBig)
        {
            StartCoroutine(Bounce());
        }

    }

    private IEnumerator Break()
    {
        yield return null;
    }

    private IEnumerator Bounce()
    {
        animating = true;

        Vector3 currentPosition = transform.position;
        Vector3 animatingPosition = currentPosition + Vector3.up * 0.5f;

        yield return Animate(currentPosition, animatingPosition);
        yield return Animate(animatingPosition, currentPosition);

        animating = false;
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
