using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float fallDelay = 0.3f;
    [SerializeField] private float respawnTime = 2f;
    
    private bool isFalling = false;
    private bool hasRespawned = true;
    private Rigidbody2D rb;
    private Vector3 startPosition;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Static;
        }
        startPosition = transform.position;
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isFalling && hasRespawned)
        {
            Invoke(nameof(StartFall), fallDelay);
        }
    }
    
    void StartFall()
    {
        isFalling = true;
        hasRespawned = false;
        
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
        
        Invoke(nameof(RespawnPlatform), respawnTime);
    }
    
    void RespawnPlatform()
    {
        transform.position = startPosition;
        
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
        }
        
        isFalling = false;
        hasRespawned = true;
    }
}
