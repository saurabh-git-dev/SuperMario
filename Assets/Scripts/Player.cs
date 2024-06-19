using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public  PlayerSpriteRenderer smallRenderer;
    public  PlayerSpriteRenderer bigRenderer;
    private  PlayerSpriteRenderer activeRenderer;
    private CapsuleCollider2D capsuleCollider;
    private DeathAnimation deathAnimation;
    private AudioSource audioSource;

    public bool IsBig => bigRenderer.enabled;
    public bool IsSmall => smallRenderer.enabled;
    public bool IsDead => deathAnimation.enabled;
    public bool IsStartPower { get; private set; }

    public AudioClip growClip;
    public AudioClip shrinkClip;
    public AudioClip dieClip;
    public AudioClip starPowerClip;


    private void Awake()
    {
        deathAnimation = GetComponent<DeathAnimation>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        audioSource = GetComponent<AudioSource>();
        activeRenderer = smallRenderer;
    }

    public void Hit()
    {
        if (!IsStartPower && !IsDead)
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
    }

    public void Shrink()
    {
        bigRenderer.enabled = false;
        smallRenderer.enabled = true;
        activeRenderer = smallRenderer;

        UpdateCollider();

        StartCoroutine(ScaleAnimation());

        audioSource.clip = shrinkClip;
        audioSource.volume = 1f;
        audioSource.Play();
    }

    public void Grow()
    {
        if (IsBig) {
            return;
        };

        smallRenderer.enabled = false;
        bigRenderer.enabled = true;
        activeRenderer = bigRenderer;

        UpdateCollider();
        
        StartCoroutine(ScaleAnimation());

        audioSource.clip = growClip;
        audioSource.volume = 1f;
        audioSource.Play();
    }

    public void UpdateCollider()
    {
        PlayerMovement playerMovement = gameObject.GetComponent<PlayerMovement>();

        if (playerMovement.IsDown)
        {   
            capsuleCollider.size = new Vector2(0.9f, 1f);
            capsuleCollider.offset = new Vector2(0, -0.1f);
        } else if (IsSmall)
        {
            capsuleCollider.size = new Vector2(0.9f, 1f);
            capsuleCollider.offset = new Vector2(0, 0f);
        }
        else
        {
            capsuleCollider.size = new Vector2(0.9f, 2f);
            capsuleCollider.offset = new Vector2(0, 0.5f);
        }
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

    public void ActivateStarPower()
    {
        StartCoroutine(StarPowerAnimation());
    }

    public IEnumerator StarPowerAnimation(float duration = 10f)
    {
        IsStartPower = true;

        audioSource.clip = AudioManager.Instance.DieClip;
        audioSource.Play();
        
        PlayerMovement playerMovement = gameObject.GetComponent<PlayerMovement>();
        playerMovement.moveSpeed += 4;

        float elapsed = 0f;

        while (elapsed < duration)
        {   
            elapsed += Time.deltaTime;

            if (Time.frameCount % 6 == 0)
            {
                activeRenderer.spriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            }

            yield return null;
        }

        playerMovement.moveSpeed -= 4;
        activeRenderer.spriteRenderer.color = Color.white;
        IsStartPower = false;
        audioSource.Stop();
    }

    private void Die()
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        deathAnimation.enabled = true;

        GameManager.Instance.ResetLevel(3f);

        audioSource.clip = dieClip;
        audioSource.volume = 1f;
        audioSource.Play();
    }
}
