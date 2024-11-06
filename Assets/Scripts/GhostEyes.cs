using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GhostEyes : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer { get; private set; }

    public Movement Movement { get; private set; }

    public Sprite up;
    public Sprite right;
    public Sprite down;
    public Sprite left;

    private void Awake()
    {
        this.SpriteRenderer = GetComponent<SpriteRenderer>();
        this.Movement = GetComponentInParent<Movement>();
    }

    private void Update()
    {
        if (this.Movement.Direction == Vector2.up)
        {
            this.SpriteRenderer.sprite = this.up;
        }
        else if (this.Movement.Direction == Vector2.right)
        {
            this.SpriteRenderer.sprite = this.right;
        }
        else if (this.Movement.Direction == Vector2.down)
        {
            this.SpriteRenderer.sprite = this.down;
        }
        else if (this.Movement.Direction == Vector2.left)
        {
            this.SpriteRenderer.sprite = this.left;
        }
    }
}
