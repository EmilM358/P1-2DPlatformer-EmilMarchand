using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // I chose to go the Rigidbody2D with velocity manipulation route because it is the one I'm most familiar with.
    // Since this is for an assignment, I want to do well, so it is better not to experiment too much, in my opinion. 

    // --------- Base Movement ---------
    [SerializeField] private SpriteRenderer spriteRenderer;
    public ScriptingBinding PlayerInput;
    private InputAction moveAction;
    private Vector2 moveInput;
    private Vector2 facingDirection = Vector2.right;

    // --------- Acceleration ---------
    [SerializeField] private float acceleration = 20f;
    [SerializeField] private float maxSpeed = 5f;
    private Rigidbody2D rb;


    private void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        PlayerInput = new ScriptingBinding();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        moveAction = PlayerInput.Player.Move;
        moveAction.Enable();
    }

    void OnDisable()
    {
        moveAction.Disable();
    }

    private void Update()
    {
  
        moveInput = moveAction.ReadValue<Vector2>();

        // --------- Flip ---------
        if (moveInput.x != 0)
        {
            spriteRenderer.flipX = moveInput.x < 0;
            facingDirection = new Vector2(moveInput.x, 0).normalized;
        }

    }
    private void FixedUpdate()
    {
        float accelFactor = acceleration * Time.fixedDeltaTime;
        float targetSpeed = moveInput.x * maxSpeed;
        float newX = Mathf.Lerp(rb.linearVelocity.x, targetSpeed, accelFactor);
        rb.linearVelocity = new Vector2(newX, rb.linearVelocity.y);
    }
}