using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Pacman : MonoBehaviour
{
    public Movement Movement { get; private set; }

    private void Awake()
    {
        this.Movement = GetComponent<Movement>();
    }

    // TODO: Death animations.

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            this.Movement.SetDirection(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.Movement.SetDirection(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            this.Movement.SetDirection(Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.Movement.SetDirection(Vector2.right);
        }

        // Rotate Pac-Man according to its direction.
        var angle = Mathf.Atan2(this.Movement.Direction.y, this.Movement.Direction.x);
        this.transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    public void ResetState()
    {
        this.gameObject.SetActive(true);
        this.Movement.ResetState();
    }
}
