using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [Header("Health Settings")]
    public float healAmount = 25f;  // how much health it restores

    [Header("Effects")]
    public ParticleSystem pickupEffect;  // optional for a little effect
    public AudioClip pickupSound;        // optional sound effect

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.Heal(healAmount);

                if (pickupEffect != null)
                    Instantiate(pickupEffect, transform.position, Quaternion.identity);

                if (pickupSound != null)
                    AudioSource.PlayClipAtPoint(pickupSound, transform.position);

                Destroy(gameObject);
            }
        }
    }
}
