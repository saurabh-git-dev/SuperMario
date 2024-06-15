using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public  PlayerSpriteRenderer smallRenderer;
    public  PlayerSpriteRenderer bigRenderer;
    private  PlayerSpriteRenderer activeRenderer;
    private CapsuleCollider2D capsuleCollider;
    private DeathAnimation deathAnimation;

    public bool IsBig => bigRenderer.enabled;
    public bool IsSmall => smallRenderer.enabled;
    public bool IsDead => deathAnimation.enabled;

    private void Awake()
    {
        deathAnimation = GetComponent<DeathAnimation>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    public void Hit()
    {
        if (IsBig)
        {
            Shrink();
        }
        else
        {
            Die();
        }
    }

    public void Shrink()
    {
        bigRenderer.enabled = false;
        smallRenderer.enabled = true;
        activeRenderer = smallRenderer;

        capsuleCollider.size = new Vector2(0.9f, 1f);
        capsuleCollider.offset = new Vector2(0, 0f);

        StartCoroutine(ScaleAnimation());
    }

    public void Grow()
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = true;
        activeRenderer = bigRenderer;

        capsuleCollider.size = new Vector2(0.9f, 2f);
        capsuleCollider.offset = new Vector2(0, 0.5f);
        
        StartCoroutine(ScaleAnimation());
    }

    private IEnumerator ScaleAnimation()
    {
        float elapsed = 0f;
        float duration = 1f;

        gameObject.layer = LayerMask.NameToLayer("PlayerScaling");

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if (Time.frameCount % 6 == 0)
            {
                smallRenderer.enabled = !smallRenderer.enabled;
                bigRenderer.enabled = !smallRenderer.enabled;
            }
            yield return null;
        }

        gameObject.layer = LayerMask.NameToLayer("Player");

        smallRenderer.enabled = false;
        bigRenderer.enabled = false;

        activeRenderer.enabled = true;

    }

    private void Die()
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        deathAnimation.enabled = true;

        GameManager.Instance.ResetLevel(3f);
    }
}
