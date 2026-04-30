using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject levelSelectPanel;
    [SerializeField] private GameObject settingsPanel;
    
    [Header("Level Buttons")]
    [SerializeField] private GameObject levelButtonPrefab;
    [SerializeField] private Transform levelsContainer;
    
    void Start()
    {
        ShowMainMenu();
    }
    
    public void ShowMainMenu()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
        if (levelSelectPanel != null) levelSelectPanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(false);
        
        Time.timeScale = 1f;
    }
    
    public void ShowLevelSelect()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (levelSelectPanel != null)
        {
            levelSelectPanel.SetActive(true);
            PopulateLevelButtons();
        }
    }
    
    public void ShowSettings()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (levelSelectPanel != null) levelSelectPanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(true);
    }
    
    public void PlayGame()
    {
        // Load first level
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    
    public void LoadLevel(int levelIndex)
    {
        // Load specific level
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelIndex);
    }
    
    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
    
    void PopulateLevelButtons()
    {
        if (levelsContainer == null || levelButtonPrefab == null) return;
        
        // Clear existing buttons
        foreach (Transform child in levelsContainer)
        {
            Destroy(child.gameObject);
        }
        
        // Create buttons for each level (1-20 for World 1)
        for (int i = 1; i <= 20; i++)
        {
            GameObject button = Instantiate(levelButtonPrefab, levelsContainer);
            
            // Setup button with level number
            // You'll need to add a script to handle button clicks
            Debug.Log($"Created button for level {i}");
        }
    }
    
    public void SetMusicVolume(float volume)
    {
        // Implement music volume control
        Debug.Log($"Music volume: {volume}");
    }
    
    public void SetSFXVolume(float volume)
    {
        // Implement SFX volume control
        Debug.Log($"SFX volume: {volume}");
    }
}
