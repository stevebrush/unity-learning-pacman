using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Vector2> AvailableDirections { get; private set; }
    public LayerMask obstacleLayer;

    private void Start()
    {
        this.AvailableDirections = new List<Vector2>();

        CheckDirection(Vector2.up);
        CheckDirection(Vector2.right);
        CheckDirection(Vector2.down);
        CheckDirection(Vector2.left);
    }

    private void CheckDirection(Vector2 direction)
    {
        var hit = Physics2D.BoxCast(
            this.transform.position,
            Vector2.one * 0.5f,
            0.0f,
            direction,
            1.0f,
            this.obstacleLayer
        );

        if (hit.collider == null)
        {
            this.AvailableDirections.Add(direction);
        }
    }
}
