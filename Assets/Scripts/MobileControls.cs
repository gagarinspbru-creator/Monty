using UnityEngine;

public class MobileControls : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerController player;
    
    [Header("UI Buttons")]
    [SerializeField] private GameObject leftButton;
    [SerializeField] private GameObject rightButton;
    [SerializeField] private GameObject jumpButton;
    
    private float horizontalInput = 0f;
    
    void Start()
    {
        // Show mobile controls only on mobile platforms
        #if UNITY_ANDROID || UNITY_IOS
        if (leftButton != null) leftButton.SetActive(true);
        if (rightButton != null) rightButton.SetActive(true);
        if (jumpButton != null) jumpButton.SetActive(true);
        #else
        if (leftButton != null) leftButton.SetActive(false);
        if (rightButton != null) rightButton.SetActive(false);
        if (jumpButton != null) jumpButton.SetActive(false);
        #endif
    }
    
    public void OnLeftButtonDown()
    {
        horizontalInput = -1f;
        UpdatePlayerInput();
    }
    
    public void OnLeftButtonUp()
    {
        if (horizontalInput < 0f)
        {
            horizontalInput = 0f;
        }
        UpdatePlayerInput();
    }
    
    public void OnRightButtonDown()
    {
        horizontalInput = 1f;
        UpdatePlayerInput();
    }
    
    public void OnRightButtonUp()
    {
        if (horizontalInput > 0f)
        {
            horizontalInput = 0f;
        }
        UpdatePlayerInput();
    }
    
    public void OnJumpButtonDown()
    {
        if (player != null)
        {
            // Trigger jump through player controller
            // This is a simplified version - you may need to expose a method in PlayerController
        }
    }
    
    void UpdatePlayerInput()
    {
        // This is a placeholder - actual implementation depends on how you want to handle mobile input
        // You might need to modify PlayerController to accept direct input values
    }
}
