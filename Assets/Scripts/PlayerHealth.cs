using UnityEngine;
using UnityEngine.SceneManagement; // for restart on death later

public class PlayerHealth : MonoBehaviour
{
    [Header("Player Health Settings")]
    public float maxHealth = 100f;
    private float currentHealth;
    private bool isDead = false;

    [Header("Damage Feedback")]
    public AudioClip damageSound;
    private AudioSource audioSource;

    void Start()
    {
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
    }

    // Called by zombies when they hit the player
    public void TakeDamage(float damageAmount)
    {
        if (isDead) return;

        currentHealth -= damageAmount;

        // Optional: play a hurt sound
        if (damageSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(damageSound);
        }

        // Clamp health so it doesn’t go negative
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        Debug.Log("Player is dead!");
        
        // You can add a death animation or sound here later
        // Disable player movement
        PlayerController controller = GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.enabled = false;
        }

        // Disable camera movement if it’s a separate script
        MouseLook cameraLook = GetComponentInChildren<MouseLook>();
        if (cameraLook != null)
        {
            cameraLook.enabled = false;
        }

        // (Temporary) Restart scene after delay
        FindFirstObjectByType<GameManager>()?.GameOver();
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Get percentage for UI health bar later
    public float GetHealthPercent()
    {
        return currentHealth / maxHealth;
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth); // cap health
        Debug.Log("Healed! Current Health: " + currentHealth);
    }

}
