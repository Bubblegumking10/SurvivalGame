using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [Header("UI References")]
    public Image healthBarFill;
    public TextMeshProUGUI scoreText;

    private PlayerHealth playerHealth;
    private int score = 0;

    void Start()
    {
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        UpdateScore(0); // Initialize text
    }

    void Update()
    {
        if (playerHealth != null && healthBarFill != null)
        {
            healthBarFill.fillAmount = playerHealth.GetHealthPercent();
        }
    }

    public void UpdateScore(int points)
    {
        score += points;
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}
