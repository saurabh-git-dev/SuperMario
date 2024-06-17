using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBreak : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("Break");
        StartCoroutine(Break());
    }

    private IEnumerator Break()
    {
        // Get all the children of the block
        Transform[] children = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i);
        }

        // Animate the block breaking
        float duration = 0.3f;
        float elapsedTime = 0f;

        Vector3[] startPositions = new Vector3[children.Length];
        Vector3[] endPosition = new Vector3[children.Length];
        Vector3[] endScale = new Vector3[children.Length];

        for (int i = 0; i < children.Length; i++)
        {
            bool boxInRightHalf = children[i].position.x > transform.position.x;
            startPositions[i] = children[i].localPosition;
            float randomMultiplier = Random.Range(1f, 3f);
            endPosition[i] = new Vector3(children[i].localPosition.x + Vector3.one.x * randomMultiplier * (boxInRightHalf ? 1 : -1), randomMultiplier, 0);
            endScale[i] = Vector3.one * 0.5f;
        }

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            for (int i = 0; i < children.Length; i++)
            {
                children[i].localPosition = Vector3.Lerp(startPositions[i], endPosition[i], t);
                children[i].localScale = Vector3.Lerp(Vector3.one, endScale[i], t);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    
        Destroy(gameObject);
    }
}
