using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int value = 1;
    [SerializeField] private float rotationSpeed = 100f;
    
    private bool isCollected = false;
    
    void Update()
    {
        // Rotate coin for visual effect
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isCollected)
        {
            Collect();
        }
    }
    
    void Collect()
    {
        isCollected = true;
        
        // Notify level manager
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        if (levelManager != null)
        {
            levelManager.AddCoin();
        }
        
        // Visual/audio feedback can be added here
        
        gameObject.SetActive(false);
    }
}
