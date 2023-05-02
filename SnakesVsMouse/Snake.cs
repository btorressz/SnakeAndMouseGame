using UnityEngine;

[DefaultExecutionOrder(-10)]
[RequireComponent(typeof(Movement))]
public class Snake : MonoBehaviour
{
    public Movement movement { get; private set; }
    public SnakesHome home { get; private set; }
    public SnakeScatter scatter { get; private set; }
    public SnakeChase chase { get; private set; }
    public SnakeFrightened frightened { get; private set; }
    public SnakeBehavior initialBehavior;
    public Transform target;
    public int points = 200;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        home = GetComponent<SnakesHome>();
        scatter = GetComponent<SnakeScatter>();
        chase = GetComponent<SnakeChase>();
        frightened = GetComponent<SnakeBehavior>();
    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        gameObject.SetActive(true);
        movement.ResetState();

        frightened.Disable();
        chase.Disable();
        scatter.Enable();

        if (home != initialBehavior) {
            home.Disable();
        }

        if (initialBehavior != null) {
            initialBehavior.Enable();
        }
    }

    public void SetPosition(Vector3 position)
    {
        // Keep the z-position the same since it determines draw depth
        position.z = transform.position.z;
        transform.position = position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Mouse"))
        {
            if (frightened.enabled) {
                FindObjectOfType<GameManager>().SnakesEaten(this);
            } else {
                FindObjectOfType<GameManager>().MouseEaten();
            }
        }
    }

}