using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float speed = 2f;
    [SerializeField] private bool horizontal = true; // true = horizontal, false = vertical
    
    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 currentVelocity;
    private bool movingForward = true;
    
    void Start()
    {
        if (pointA != null && pointB != null)
        {
            startPos = pointA.position;
            endPos = pointB.position;
            transform.position = startPos;
        }
    }
    
    void Update()
    {
        if (pointA == null || pointB == null) return;
        
        Vector3 targetPos = movingForward ? endPos : startPos;
        float distanceToTarget = Vector3.Distance(transform.position, targetPos);
        
        if (distanceToTarget < 0.1f)
        {
            movingForward = !movingForward;
        }
        
        // Calculate movement direction
        Vector3 direction = (targetPos - transform.position).normalized;
        currentVelocity = direction * speed * Time.deltaTime;
        
        transform.Translate(currentVelocity, Space.World);
    }
    
    void FixedUpdate()
    {
        // Move any objects on the platform
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);
        foreach (var col in colliders)
        {
            if (col.CompareTag("Player"))
            {
                Rigidbody2D playerRb = col.GetComponent<Rigidbody2D>();
                if (playerRb != null && IsPlayerOnPlatform(col.gameObject))
                {
                    Vector2 moveVelocity = new Vector2(currentVelocity.x, currentVelocity.y);
                    playerRb.velocity += moveVelocity / Time.fixedDeltaTime;
                }
            }
        }
    }
    
    bool IsPlayerOnPlatform(GameObject player)
    {
        // Simple check: player's bottom should be near platform's top
        float playerBottom = player.transform.position.y - 0.5f;
        float platformTop = transform.position.y + 0.5f;
        return Mathf.Abs(playerBottom - platformTop) < 0.3f;
    }
    
    void OnDrawGizmosSelected()
    {
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(pointA.position, pointB.position);
            Gizmos.DrawWireSphere(pointA.position, 0.2f);
            Gizmos.DrawWireSphere(pointB.position, 0.2f);
        }
    }
}
