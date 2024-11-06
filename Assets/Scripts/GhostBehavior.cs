using UnityEngine;

[RequireComponent(typeof(Ghost))]
public abstract class GhostBehavior : MonoBehaviour
{
    public Ghost Ghost { get; private set; }
    public float duration;

    private void Awake()
    {
        this.Ghost = GetComponent<Ghost>();
    }

    public void Enable()
    {
        EnableForDuration(this.duration);
    }

    public virtual void EnableForDuration(float duration)
    {
        this.enabled = true;
        CancelInvoke();
        Invoke(nameof(Disable), duration);
    }

    public virtual void Disable()
    {
        this.enabled = false;
        CancelInvoke();
    }
}
