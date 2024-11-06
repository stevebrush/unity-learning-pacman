using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    public float speed = 8.0f;
    public float speedMultiplier = 1.0f;
    public Vector2 initialDirection;
    public LayerMask obstacleLayer;

    public Rigidbody2D Rigidbody { get; private set; }

    public Vector2 Direction { get; private set; }
    public Vector2 NextDirection { get; private set; }
    public Vector3 StartingPosition { get; private set; }

    private void Awake()
    {
        this.Rigidbody = GetComponent<Rigidbody2D>();
        this.StartingPosition = this.transform.position;
    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        this.speedMultiplier = 1.0f;
        this.Direction = this.initialDirection;
        this.NextDirection = Vector2.zero;
        this.transform.position = this.StartingPosition;
        this.Rigidbody.bodyType = RigidbodyType2D.Dynamic;
        this.enabled = true;
    }

    private void Update()
    {
        if (this.NextDirection != Vector2.zero)
        {
            SetDirection(this.NextDirection);
        }
    }

    private void FixedUpdate()
    {
        var position = this.Rigidbody.position;
        var translation = this.speed * this.speedMultiplier * Time.fixedDeltaTime * this.Direction;

        this.Rigidbody.MovePosition(position + translation);
    }

    public void SetDirection(Vector2 direction, bool force = false)
    {
        if (force || !Occupied(direction))
        {
            this.Direction = direction;
            this.NextDirection = Vector2.zero;
        }
        else
        {
            this.NextDirection = direction;
        }
    }

    public bool Occupied(Vector2 direction)
    {
        var hit = Physics2D.BoxCast(
            this.transform.position,
            // Make the size of the box slightly smaller than the collider.
            Vector2.one * 0.75f,
            0.0f,
            direction,
            // Detection starts at the center of our object, so the range should include
            // the width of the object, plus half.
            1.5f,
            this.obstacleLayer
        );

        return hit.collider != null;
    }
}
