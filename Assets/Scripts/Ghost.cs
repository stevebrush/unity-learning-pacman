using UnityEngine;

[
    RequireComponent(typeof(GhostChase)),
    RequireComponent(typeof(GhostHome)),
    RequireComponent(typeof(GhostScared)),
    RequireComponent(typeof(GhostScatter)),
    RequireComponent(typeof(Movement)),
]
public class Ghost : MonoBehaviour
{
    public Movement Movement { get; private set; }

    public GhostChase BehaviorChase { get; private set; }
    public GhostHome BehaviorHome { get; private set; }
    public GhostScared BehaviorScared { get; private set; }
    public GhostScatter BehaviorScatter { get; private set; }
    public GhostBehavior initialBehavior;

    public Transform target;

    public int points = 200;

    private void Awake()
    {
        this.Movement = GetComponent<Movement>();
        this.BehaviorChase = GetComponent<GhostChase>();
        this.BehaviorHome = GetComponent<GhostHome>();
        this.BehaviorScared = GetComponent<GhostScared>();
        this.BehaviorScatter = GetComponent<GhostScatter>();
    }

    private void Start()
    {
        ResetState();
    }

    // TODO: Make ghost eyes move when movement changes.

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            var gameManager = FindFirstObjectByType<GameManager>();

            if (this.BehaviorScared.enabled)
            {
                gameManager.GhostEaten(this);
            }
            else
            {
                gameManager.PacmanEaten();
            }
        }
    }

    public void ResetState()
    {
        this.gameObject.SetActive(true);
        this.Movement.ResetState();

        this.BehaviorChase.Disable();
        this.BehaviorScared.Disable();
        this.BehaviorScatter.Enable();

        if (this.initialBehavior != this.BehaviorHome)
        {
            this.BehaviorHome.Disable();
        }

        if (this.initialBehavior != null)
        {
            this.initialBehavior.Enable();
        }
    }
}
