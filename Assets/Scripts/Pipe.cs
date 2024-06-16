using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public KeyCode enterKeyCode = KeyCode.DownArrow;
    public Vector3 enterDirection = Vector3.down;
    public Vector3 exitDirection = Vector3.zero;
    public Transform Connection;

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (Connection != null && collider.CompareTag("Player"))
        {
            if (Input.GetKey(enterKeyCode))
            {
                StartCoroutine(Enter(collider.transform));
            }
        }
    }

    private IEnumerator Enter(Transform player)
    {
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.enabled = false;

        Vector3 endPosition = transform.position + enterDirection;
        Vector3 endScale = Vector3.one * 0.5f;

        yield return Move(player, endPosition, endScale);
        yield return new WaitForSeconds(1f);

        bool underground = Connection.transform.position.y < 0;
        Camera.main.GetComponent<SideScrolling>().SetUnderground(underground);
        
        if (exitDirection != Vector3.zero)
        {
            player.position = Connection.position - exitDirection;
            yield return Move(player, Connection.position + exitDirection, Vector3.one);
        } else
        {
            player.position = Connection.position;
            player.localScale = Vector3.one;
        }

        playerMovement.enabled = true;
    }

    private IEnumerator Move(Transform player, Vector3 endPosition, Vector3 endScale)
    {
        float elapsedTime = 0f;
        float duration = 1f;

        Vector3 startPosition = player.position;
        Vector3 startScale = player.localScale;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            player.position = Vector3.Lerp(startPosition, endPosition, t);
            player.localScale = Vector3.Lerp(startScale, endScale, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        player.position = endPosition;
        player.localScale = endScale;
    }
}
