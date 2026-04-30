using UnityEngine;

public class DisappearingBlock : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float disappearDelay = 0.5f;
    [SerializeField] private float reappearTime = 2f;
    [SerializeField] private bool startVisible = true;
    
    [Header("Visual")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color visibleColor = Color.white;
    [SerializeField] private Color invisibleColor = new Color(1f, 1f, 1f, 0.3f);
    
    private bool isVisible;
    private bool isDisappearing = false;
    private Collider2D blockCollider;
    
    void Awake()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        blockCollider = GetComponent<Collider2D>();
        
        isVisible = startVisible;
        UpdateVisual();
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isVisible && !isDisappearing)
        {
            StartDisappear();
        }
    }
    
    void StartDisappear()
    {
        isDisappearing = true;
        Invoke(nameof(HideBlock), disappearDelay);
    }
    
    void HideBlock()
    {
        isVisible = false;
        isDisappearing = false;
        
        if (blockCollider != null)
        {
            blockCollider.enabled = false;
        }
        
        UpdateVisual();
        
        Invoke(nameof(ShowBlock), reappearTime);
    }
    
    void ShowBlock()
    {
        isVisible = true;
        
        if (blockCollider != null)
        {
            blockCollider.enabled = true;
        }
        
        UpdateVisual();
    }
    
    void UpdateVisual()
    {
        if (spriteRenderer != null)
        {
            Color targetColor = isVisible ? visibleColor : invisibleColor;
            spriteRenderer.color = targetColor;
        }
    }
}
