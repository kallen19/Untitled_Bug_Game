using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerState
{
    InControl,
    Knockback
}

public class move : MonoBehaviour
{
    private InputAction moveAction;

    private Vector2 moveVector = Vector2.zero;

    public Rigidbody2D rb;

    public PlayerState state;
    
    public float speedUnitsPerSec = 0;

    public Vector3 knockbackDirection;
    public float knockbackMaxTime = 1f;
    private float knockbackTimeLeft;
    public float knockbackMaxSpeed = 50f;
    private float knockbackSpeed;
    public float knockbackFallOffRatio = 0.95f;

    [SerializeField] private SpriteRenderer sr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (state)
        {
            case PlayerState.InControl:
                moveVector = moveAction.ReadValue<Vector2>();
                rb.linearVelocity = speedUnitsPerSec * Time.deltaTime * moveVector;
                break;
            case PlayerState.Knockback:
                transform.Translate(knockbackSpeed * Time.deltaTime * knockbackDirection);

                // faux friction

                knockbackSpeed *= knockbackFallOffRatio;

                knockbackTimeLeft -= Time.fixedDeltaTime;
                if (knockbackTimeLeft <= 0)
                {
                    state = PlayerState.InControl;
                }
                break;
        }
    }

    public void Knockback(Transform awayFrom)
    {
        knockbackDirection = transform.position - awayFrom.position;
        state = PlayerState.Knockback;
        knockbackTimeLeft = knockbackMaxTime;
        rb.linearVelocity = knockbackDirection;
        knockbackSpeed = knockbackMaxSpeed;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Knockback(other.transform);
        }
    }
}
