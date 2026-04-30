using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float coyoteTime = 0.15f;
    [SerializeField] private float jumpBufferTime = 0.1f;
    
    [Header("Ground Check")]
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private Vector2 groundCheckSize = new Vector2(0.8f, 0.1f);
    [SerializeField] private LayerMask groundLayer;
    
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private float horizontalInput;
    private bool jumpPressed;
    private bool jumpHeld;
    
    // Coyote time & jump buffer
    private float coyoteTimeCounter;
    private float jumpBufferCounter;
    
    // State
    private bool isGrounded;
    private bool isDead;
    
    // Events
    public System.Action<float> OnMove;
    public System.Action OnJump;
    public System.Action OnDeath;
    public System.Action OnLevelComplete;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        
        // Freeze rotation
        rb.freezeRotation = true;
    }
    
    void Update()
    {
        if (isDead) return;
        
        // Input handling
        HandleInput();
        
        // Update timers
        UpdateTimers();
        
        // Ground check
        CheckGround();
        
        // Jump
        if (jumpPressed && coyoteTimeCounter > 0f)
        {
            PerformJump();
            jumpPressed = false;
        }
    }
    
    void FixedUpdate()
    {
        if (isDead) return;
        
        // Movement
        Move();
    }
    
    void HandleInput()
    {
        // Horizontal movement
        horizontalInput = Input.GetAxisRaw("Horizontal");
        OnMove?.Invoke(horizontalInput);
        
        // Jump input with buffer
        if (Input.GetButtonDown("Jump"))
        {
            jumpPressed = true;
            jumpBufferCounter = jumpBufferTime;
            OnJump?.Invoke();
        }
        else
        {
            jumpPressed = false;
            jumpBufferCounter = 0f;
        }
        
        // Variable jump height (release jump to fall faster)
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }
    
    void UpdateTimers()
    {
        // Coyote time countdown
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        
        // Jump buffer countdown
        if (jumpBufferCounter > 0f)
        {
            jumpBufferCounter -= Time.deltaTime;
        }
    }
    
    void CheckGround()
    {
        isGrounded = Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0f, groundLayer);
    }
    
    void Move()
    {
        Vector2 targetVelocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        rb.velocity = targetVelocity;
        
        // Flip sprite based on direction
        if (horizontalInput > 0f)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (horizontalInput < 0f)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
    
    void PerformJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        coyoteTimeCounter = 0f;
    }
    
    public void Die()
    {
        if (isDead) return;
        
        isDead = true;
        rb.velocity = Vector2.zero;
        rb.simulated = false;
        boxCollider.enabled = false;
        
        OnDeath?.Invoke();
    }
    
    public void ResetPlayer(Vector2 spawnPosition)
    {
        isDead = false;
        transform.position = spawnPosition;
        rb.velocity = Vector2.zero;
        rb.simulated = true;
        boxCollider.enabled = true;
    }
    
    public void CompleteLevel()
    {
        OnLevelComplete?.Invoke();
    }
    
    // Debug visualization
    void OnDrawGizmosSelected()
    {
        if (groundCheckPoint != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(groundCheckPoint.position, groundCheckSize);
        }
    }
}
