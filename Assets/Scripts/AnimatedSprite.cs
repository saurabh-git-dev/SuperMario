using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{
    public Sprite[] items;
    public float framerate = 1f / 6f;

    private SpriteRenderer spriteRenderer;

    private int index;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable() 
    {
        InvokeRepeating(nameof(Animate), framerate, framerate);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Animate));
    }

    private void Animate()
    {
        if (index >= items.Length) {
            index = 0;
        }

        spriteRenderer.sprite = items[index];
        index++;
    }
}
