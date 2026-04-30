using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Patrol Settings")]
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float speed = 2f;
    [SerializeField] private bool patrolEnabled = true;
    
    [Header("Ground Check")]
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private Vector2 groundCheckSize = new Vector2(0.8f, 0.1f);
    [SerializeField] private LayerMask groundLayer;
    
    private Transform currentTarget;
    private Rigidbody2D rb;
    private bool isGrounded;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.freezeRotation = true;
        }
        
        // Set initial target
        if (pointB != null)
        {
            currentTarget = pointB;
        }
    }
    
    void Update()
    {
        if (!patrolEnabled) return;
        
        CheckGround();
        
        // Move towards target
        if (currentTarget != null)
        {
            Vector2 direction = (currentTarget.position - transform.position).normalized;
            
            // Flip sprite based on direction
            if (direction.x > 0f)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (direction.x < 0f)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            
            // Check if reached target
            float distanceToTarget = Vector2.Distance(transform.position, currentTarget.position);
            if (distanceToTarget < 0.1f)
            {
                // Switch target
                if (currentTarget == pointB && pointA != null)
                {
                    currentTarget = pointA;
                }
                else if (currentTarget == pointA && pointB != null)
                {
                    currentTarget = pointB;
                }
            }
        }
    }
    
    void FixedUpdate()
    {
        if (!patrolEnabled || currentTarget == null) return;
        
        Vector2 direction = (currentTarget.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
    }
    
    void CheckGround()
    {
        if (groundCheckPoint != null)
        {
            isGrounded = Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0f, groundLayer);
            
            // Reverse direction if no ground ahead
            if (!isGrounded && currentTarget != null)
            {
                Vector2 direction = (currentTarget.position - transform.position).normalized;
                
                // If moving in positive x but no ground, switch to other point
                if (direction.x > 0f && pointA != null)
                {
                    currentTarget = pointA;
                }
                // If moving in negative x but no ground, switch to other point
                else if (direction.x < 0f && pointB != null)
                {
                    currentTarget = pointB;
                }
            }
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.Die();
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.Die();
            }
        }
    }
    
    void OnDrawGizmosSelected()
    {
        // Draw patrol points
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(pointA.position, pointB.position);
            Gizmos.DrawWireSphere(pointA.position, 0.2f);
            Gizmos.DrawWireSphere(pointB.position, 0.2f);
        }
        
        // Draw ground check
        if (groundCheckPoint != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(groundCheckPoint.position, groundCheckSize);
        }
    }
}
