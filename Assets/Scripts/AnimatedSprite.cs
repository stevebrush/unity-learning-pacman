using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour
{
    public SpriteRenderer Renderer { get; private set; }
    public Sprite[] sprites;
    public float animationDelay = 0.25f;
    public int AnimationFrame { get; private set; }
    public bool shouldLoop = true;

    private void Awake()
    {
        this.Renderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(AdvanceFrame), this.animationDelay, this.animationDelay);
    }

    private void AdvanceFrame()
    {
        if (!this.shouldLoop || !this.Renderer.enabled)
        {
            return;
        }

        this.AnimationFrame++;

        if (this.AnimationFrame >= this.sprites.Length)
        {
            this.AnimationFrame = 0;
        }

        this.Renderer.sprite = this.sprites[this.AnimationFrame];
    }

    public void Restart()
    {
        this.AnimationFrame = -1;
        AdvanceFrame();
    }
}
