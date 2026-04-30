using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Level Settings")]
    [SerializeField] private int levelNumber = 1;
    [SerializeField] private string levelName = "Level 1";
    
    [Header("References")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Door exitDoor;
    
    [Header("Collectibles")]
    [SerializeField] private int totalCoins = 0;
    private int collectedCoins = 0;
    
    public int LevelNumber => levelNumber;
    public string LevelName => levelName;
    public Transform SpawnPoint => spawnPoint;
    
    void Start()
    {
        Debug.Log($"Starting level: {levelName}");
    }
    
    public void AddCoin()
    {
        collectedCoins++;
        if (collectedCoins >= totalCoins)
        {
            OnAllCoinsCollected();
        }
    }
    
    void OnAllCoinsCollected()
    {
        Debug.Log("All coins collected!");
        // Bonus reward logic here
    }
    
    public void CompleteLevel()
    {
        Debug.Log($"Level {levelNumber} completed!");
        // Save progress, show results, etc.
    }
}
