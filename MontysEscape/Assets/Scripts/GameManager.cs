using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [Header("Game Settings")]
    [SerializeField] private float restartDelay = 0.5f;
    
    [Header("References")]
    [SerializeField] private PlayerController player;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Door currentDoor;
    
    private HashSet<string> collectedKeys = new HashSet<string>();
    private int currentLevel = 1;
    private int totalDeaths = 0;
    private float levelStartTime;
    
    public int CurrentLevel => currentLevel;
    public int TotalDeaths => totalDeaths;
    public float CurrentTime => Time.time - levelStartTime;
    
    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        // Subscribe to player events
        if (player != null)
        {
            player.OnDeath += HandlePlayerDeath;
            player.OnLevelComplete += HandleLevelComplete;
        }
    }
    
    void Start()
    {
        ResetLevel();
    }
    
    void OnDestroy()
    {
        if (player != null)
        {
            player.OnDeath -= HandlePlayerDeath;
            player.OnLevelComplete -= HandleLevelComplete;
        }
    }
    
    public void CollectKey(string keyId)
    {
        collectedKeys.Add(keyId);
        Debug.Log($"Key collected: {keyId}");
    }
    
    public bool HasKey(string keyId)
    {
        return collectedKeys.Contains(keyId);
    }
    
    public void ClearKeys()
    {
        collectedKeys.Clear();
    }
    
    void HandlePlayerDeath()
    {
        totalDeaths++;
        Invoke(nameof(ResetLevel), restartDelay);
    }
    
    void HandleLevelComplete()
    {
        Debug.Log($"Level {currentLevel} completed!");
        currentLevel++;
        
        if (currentDoor != null)
        {
            currentDoor.LoadNextLevel();
        }
    }
    
    public void ResetLevel()
    {
        ClearKeys();
        
        if (player != null && spawnPoint != null)
        {
            player.ResetPlayer(spawnPoint.position);
        }
        
        levelStartTime = Time.time;
        Debug.Log($"Level {currentLevel} started");
    }
    
    public void RestartGame()
    {
        currentLevel = 1;
        totalDeaths = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    // For mobile touch controls
    public void MoveLeft(bool isPressed)
    {
        // Implement touch input handling
    }
    
    public void MoveRight(bool isPressed)
    {
        // Implement touch input handling
    }
    
    public void Jump()
    {
        // Implement touch jump
    }
}
