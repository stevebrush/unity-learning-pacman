using UnityEngine;

public class GhostScared : GhostBehavior
{
    public SpriteRenderer spriteBody;
    public SpriteRenderer spriteEyes;
    public SpriteRenderer spriteScared;
    public SpriteRenderer spriteResolved;

    public bool IsEaten { get; private set; } = false;

    private void OnEnable()
    {
        this.Ghost.Movement.speedMultiplier = 0.5f;
        this.IsEaten = false;
    }

    private void OnDisable()
    {
        this.Ghost.Movement.speedMultiplier = 1.0f;
        this.IsEaten = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.enabled && collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            Eaten();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!this.enabled)
        {
            return;
        }

        if (other.gameObject.TryGetComponent<Node>(out var node))
        {
            var maxDistance = float.MinValue;
            var preferredDirection = Vector2.zero;

            foreach (var availableDirection in node.AvailableDirections)
            {
                var newPosition =
                    this.transform.position
                    + new Vector3(availableDirection.x, availableDirection.y, 0.0f);

                var distance = (this.Ghost.target.position - newPosition).sqrMagnitude;

                if (distance > maxDistance)
                {
                    maxDistance = distance;
                    preferredDirection = availableDirection;
                }
            }

            this.Ghost.Movement.SetDirection(preferredDirection);
        }
    }

    public override void EnableForDuration(float duration)
    {
        base.EnableForDuration(duration);

        this.spriteBody.enabled = false;
        this.spriteEyes.enabled = false;
        this.spriteResolved.enabled = false;
        this.spriteScared.enabled = true;

        Invoke(nameof(Flash), this.duration / 2.0f);
    }

    public override void Disable()
    {
        base.Disable();

        this.spriteBody.enabled = true;
        this.spriteEyes.enabled = true;
        this.spriteResolved.enabled = false;
        this.spriteScared.enabled = false;
    }

    private void Flash()
    {
        if (!this.IsEaten)
        {
            this.spriteResolved.enabled = true;
            this.spriteScared.enabled = false;
            this.spriteResolved.GetComponent<AnimatedSprite>().Restart();
        }
    }

    private void Eaten()
    {
        this.IsEaten = true;
        this.spriteBody.enabled = false;
        this.spriteEyes.enabled = true;
        this.spriteResolved.enabled = false;
        this.spriteScared.enabled = false;

        // TODO: Make the ghost traverse the maze to the home position, instead.
        var homePosition = this.Ghost.BehaviorHome.inside.position;
        homePosition.z = this.Ghost.transform.position.z;
        this.Ghost.transform.position = homePosition;
        this.Ghost.BehaviorHome.EnableForDuration(this.duration);
    }
}
