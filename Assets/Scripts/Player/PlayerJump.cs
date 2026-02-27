using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    ScriptingBinding inputActions;
    Rigidbody2D rb;

    public float jumpForce = 5f; 
    [SerializeField] private float jumpCutMultiplier = 0.5f; 
    [SerializeField] private float fallMultiplier = 2.5f; 
    [SerializeField] private float lowJumpMultiplier = 2f; 
    [SerializeField] private float maxFallSpeed = -10f;
    private Collider2D coll;

    bool isJumpPressed = false;
    bool isJumpReleased = false;
    bool isJumpHeld = false;

    // I tried both the OverlapCircle and Raycast but had trouble with edge detection.
    // My jump felt weak when I was too close to the edge of a platform.
    // I used a Boxcast for the game I made for Salim's class but that wasn't one of the options so I opted not to use it.
    // I then looked into the OnCollisionEnter2D/OnCollisionStay2D with layer filtering method and while looking at the Unity API,
    // I found IsTouchingLayers. I tried it and it did exactly what I wanted. I know it ends up not being one of the methods listed, but it does have layer filtering... :D ?


    [SerializeField] private LayerMask groundLayer; 

    // I chose Coyote Time because I asked myself which one I'd rather have if given the choice
    // between it and jump buffering. I thought it would make for a better experience.

    [SerializeField] private float coyoteTime = 0.2f; 
    [SerializeField] private float coyoteTimeCounter;
    bool isJumping = false;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        inputActions = new ScriptingBinding();
    }

    void OnEnable()
    {
        inputActions.Player.Jump.Enable();
        inputActions.Player.Jump.performed += OnJumpPerformed;
        inputActions.Player.Jump.canceled += OnJumpCanceled;
    }
    void OnDisable()
    {
        inputActions.Player.Jump.performed -= OnJumpPerformed;
        inputActions.Player.Jump.canceled -= OnJumpCanceled;
        inputActions.Player.Jump.Disable();
    }

    private void Update()
    {
        bool isGrounded = IsGrounded();

        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
            isJumping = false;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (isJumpPressed && coyoteTimeCounter > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isJumpPressed = false;
            isJumping = true;
            coyoteTimeCounter = 0f;
        }

        if (isJumpReleased)
        {
            if (rb.linearVelocity.y > 0f)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * jumpCutMultiplier);
            }
            isJumpReleased = false;
        }

        // --------- Vizualize Ground Detection ---------
        // green = grounded, red = not grounded
        Color rayColor = IsGrounded() ? Color.green : Color.red;
        Debug.DrawRay(transform.position, Vector3.down * 0.5f, rayColor);
    
}

    private void FixedUpdate()
    {
        if (rb.linearVelocity.y < 0f)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1f) * Time.fixedDeltaTime;
        }
        else if (rb.linearVelocity.y > 0f && isJumping && !isJumpHeld)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1f) * Time.fixedDeltaTime;
        }

        if (rb.linearVelocity.y < maxFallSpeed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, maxFallSpeed);
        }
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isJumpPressed = true;
            isJumpHeld = true;
        }
    }

    private void OnJumpCanceled(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            isJumpReleased = true;
            isJumpHeld = false;
        }
    }

    private bool IsGrounded()
    {
        return coll.IsTouchingLayers(groundLayer);

    }
}
