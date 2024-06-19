using UnityEngine;

public class PlayerSpriteRenderer : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    private PlayerMovement playerMovement;

    public Sprite idle;
    public Sprite jump;
    public Sprite slide;
    public Sprite down;
    public AnimatedSprite run;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }

    private void OnDisable() 
    {
        spriteRenderer.enabled = false;
        run.enabled = false;
    }

    private void LateUpdate()
    {   
        run.enabled = playerMovement.IsRunning;

        if (playerMovement.IsJumping) {
            spriteRenderer.sprite = jump;
        } else if (playerMovement.IsSliding) {
            spriteRenderer.sprite = slide;
        } else if (playerMovement.IsDown) {
            spriteRenderer.sprite = down;
        } else if (!playerMovement.IsRunning) {
            spriteRenderer.sprite = idle;
        }
    }
}
