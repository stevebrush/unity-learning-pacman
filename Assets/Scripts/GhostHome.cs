using System.Collections;
using UnityEngine;

public class GhostHome : GhostBehavior
{
    public Transform inside;
    public Transform outside;

    private void OnDisable()
    {
        if (this.gameObject.activeSelf)
        {
            StartCoroutine(ExitTransition());
        }
    }

    private void OnEnable()
    {
        StopAllCoroutines();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!this.enabled)
        {
            return;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            this.Ghost.Movement.SetDirection(this.Ghost.Movement.Direction * -1);
        }
    }

    private IEnumerator ExitTransition()
    {
        this.Ghost.Movement.SetDirection(Vector2.up, true);
        this.Ghost.Movement.Rigidbody.bodyType = RigidbodyType2D.Kinematic;
        this.Ghost.Movement.enabled = false;

        var currentPosition = this.transform.position;
        var duration = 0.5f;
        var elapsed = 0.0f;

        while (elapsed < duration)
        {
            var newPosition = Vector3.Lerp(
                currentPosition,
                this.inside.position,
                elapsed / duration
            );

            newPosition.z = currentPosition.z;

            this.Ghost.transform.position = newPosition;

            elapsed += Time.deltaTime;

            yield return null;
        }

        elapsed = 0.0f;

        while (elapsed < duration)
        {
            var newPosition = Vector3.Lerp(
                this.inside.position,
                this.outside.position,
                elapsed / duration
            );

            newPosition.z = currentPosition.z;

            this.Ghost.transform.position = newPosition;

            elapsed += Time.deltaTime;

            yield return null;
        }

        this.Ghost.Movement.SetDirection(
            new Vector2(Random.value < 0.5f ? -1.0f : 1.0f, 0.0f),
            true
        );
        this.Ghost.Movement.Rigidbody.bodyType = RigidbodyType2D.Dynamic;
        this.Ghost.Movement.enabled = true;
    }
}
