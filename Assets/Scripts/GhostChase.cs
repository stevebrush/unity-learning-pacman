using UnityEngine;

public class GhostChase : GhostBehavior
{
    private void OnDisable()
    {
        this.Ghost.BehaviorScatter.Enable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!this.enabled && this.Ghost.BehaviorScared.enabled)
        {
            return;
        }

        if (other.gameObject.TryGetComponent<Node>(out var node))
        {
            var minDistance = float.MaxValue;
            var preferredDirection = Vector2.zero;

            foreach (var availableDirection in node.AvailableDirections)
            {
                var newPosition =
                    this.transform.position
                    + new Vector3(availableDirection.x, availableDirection.y, 0.0f);

                var distance = (this.Ghost.target.position - newPosition).sqrMagnitude;

                if (distance < minDistance)
                {
                    minDistance = distance;
                    preferredDirection = availableDirection;
                }
            }

            this.Ghost.Movement.SetDirection(preferredDirection);
        }
    }
}
