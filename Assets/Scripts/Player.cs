using UnityEngine;

public class Player : MonoBehaviour
{
    public  PlayerSpriteRenderer smallRenderer;
    public  PlayerSpriteRenderer bigRenderer;
    private DeathAnimation deathAnimation;

    public bool IsBig => bigRenderer.enabled;
    public bool IsSmall => smallRenderer.enabled;
    public bool IsDead => deathAnimation.enabled;

    private void Awake()
    {
        deathAnimation = GetComponent<DeathAnimation>();
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

    private void Shrink()
    {
        bigRenderer.enabled = false;
        smallRenderer.enabled = true;
    }

    private void Die()
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        deathAnimation.enabled = true;

        GameManager.Instance.ResetLevel(3f);
    }
}
