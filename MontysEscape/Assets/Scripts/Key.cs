using UnityEngine;
using UnityEngine.SceneManagement;

public class Key : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string requiredKey = "level1";
    
    private bool isCollected = false;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isCollected)
        {
            isCollected = true;
            GameManager.Instance?.CollectKey(requiredKey);
            gameObject.SetActive(false);
        }
    }
}
