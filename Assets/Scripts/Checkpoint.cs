using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private bool isActivated = false;
    
    [Header("Visual")]
    [SerializeField] private SpriteRenderer flagRenderer;
    [SerializeField] private Color inactiveColor = Color.gray;
    [SerializeField] private Color activeColor = Color.green;
    
    void Start()
    {
        UpdateVisual();
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isActivated)
        {
            Activate();
        }
    }
    
    void Activate()
    {
        isActivated = true;
        UpdateVisual();
        
        // Update player spawn point
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            // You might want to store this checkpoint as the new spawn point
            Debug.Log("Checkpoint activated!");
        }
    }
    
    void UpdateVisual()
    {
        if (flagRenderer != null)
        {
            flagRenderer.color = isActivated ? activeColor : inactiveColor;
        }
    }
    
    public Vector2 GetSpawnPoint()
    {
        return transform.position;
    }
}
