using System.Collections;
using UnityEngine;

public class BlockCoin : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.AddCoin();

        StartCoroutine(Bounce());
    }

    private IEnumerator Bounce()
    {
        Vector3 currentPosition = transform.position;
        Vector3 animatingPosition = currentPosition + Vector3.up * 2f;

        yield return Animate(currentPosition, animatingPosition);
        yield return Animate(animatingPosition, currentPosition);

        Destroy(gameObject);
    }

    private IEnumerator Animate(Vector3 from, Vector3 to)
    {
        float duration = 0.25f;
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
