using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI deathCounterText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject deathPanel;
    
    [Header("Keys UI")]
    [SerializeField] private Transform keysContainer;
    [SerializeField] private GameObject keyIconPrefab;
    
    private bool isPaused = false;
    
    void Start()
    {
        UpdateUI();
        
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
        
        if (deathPanel != null)
        {
            deathPanel.SetActive(false);
        }
    }
    
    void Update()
    {
        UpdateUI();
        
        // Pause input
        if (Input.GetButtonDown("Pause"))
        {
            TogglePause();
        }
    }
    
    void UpdateUI()
    {
        if (GameManager.Instance != null)
        {
            // Update death counter
            if (deathCounterText != null)
            {
                deathCounterText.text = $"Deaths: {GameManager.Instance.TotalDeaths}";
            }
            
            // Update timer
            if (timerText != null)
            {
                float time = GameManager.Instance.CurrentTime;
                int minutes = Mathf.FloorToInt(time / 60);
                int seconds = Mathf.FloorToInt(time % 60);
                int milliseconds = Mathf.FloorToInt((time * 100) % 100);
                timerText.text = $"{minutes:00}:{seconds:00}:{milliseconds:00}";
            }
            
            // Update level text
            if (levelText != null)
            {
                levelText.text = $"Level {GameManager.Instance.CurrentLevel}";
            }
        }
    }
    
    public void TogglePause()
    {
        isPaused = !isPaused;
        
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(isPaused);
        }
        
        Time.timeScale = isPaused ? 0f : 1f;
    }
    
    public void ResumeGame()
    {
        isPaused = false;
        
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
        
        Time.timeScale = 1f;
    }
    
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        GameManager.Instance?.ResetLevel();
        
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
        
        isPaused = false;
    }
    
    public void ShowDeathPanel()
    {
        if (deathPanel != null)
        {
            deathPanel.SetActive(true);
        }
    }
    
    public void HideDeathPanel()
    {
        if (deathPanel != null)
        {
            deathPanel.SetActive(false);
        }
    }
    
    public void AddKeyIcon()
    {
        if (keysContainer != null && keyIconPrefab != null)
        {
            GameObject keyIcon = Instantiate(keyIconPrefab, keysContainer);
            keyIcon.SetActive(true);
        }
    }
    
    public void ClearKeyIcons()
    {
        if (keysContainer != null)
        {
            foreach (Transform child in keysContainer)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
