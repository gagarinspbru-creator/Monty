using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string requiredKey = "level1";
    [SerializeField] private string nextSceneName;
    
    private bool isOpen = false;
    
    void Update()
    {
        if (!isOpen && GameManager.Instance != null)
        {
            if (GameManager.Instance.HasKey(requiredKey))
            {
                OpenDoor();
            }
        }
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isOpen)
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.CompleteLevel();
            }
        }
    }
    
    void OpenDoor()
    {
        isOpen = true;
        // Visual feedback - you can add animation/sound here
        Debug.Log("Door opened!");
    }
    
    public void LoadNextLevel()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            // Load next scene in build order
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            if (currentSceneIndex < SceneManager.sceneCountInBuildSettings - 1)
            {
                SceneManager.LoadScene(currentSceneIndex + 1);
            }
        }
    }
}
