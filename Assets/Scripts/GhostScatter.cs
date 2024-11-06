using System.Collections.Generic;
using UnityEngine;

public class GhostScatter : GhostBehavior
{
    private void OnDisable()
    {
        this.Ghost.BehaviorChase.Enable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!this.enabled || this.Ghost.BehaviorScared.enabled)
        {
            return;
        }

        if (other.gameObject.TryGetComponent<Node>(out var node))
        {
            var index = Random.Range(0, node.AvailableDirections.Count);
            var directions = new List<Vector2>(node.AvailableDirections);

            if (directions[index] == (this.Ghost.Movement.Direction * -1) && directions.Count > 1)
            {
                directions.RemoveAt(index);
                index = Random.Range(0, directions.Count);
            }

            this.Ghost.Movement.SetDirection(directions[index]);
        }
    }
}
