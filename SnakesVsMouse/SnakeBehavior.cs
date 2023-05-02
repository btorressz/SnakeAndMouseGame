using UnityEngine;

[RequireComponent(typeof(Snake))]
public abstract class SnakeBehavior : MonoBehaviour
{
    public Snake snake { get; private set; }
    public float duration;

    private void Awake()
    {
        snake = GetComponent<Snake>();
    }

    public void Enable()
    {
        Enable(duration);
    }

    public virtual void Enable(float duration)
    {
        enabled = true;

        CancelInvoke();
        Invoke(nameof(Disable), duration);
    }

    public virtual void Disable()
    {
        enabled = false;

        CancelInvoke();
    }

}