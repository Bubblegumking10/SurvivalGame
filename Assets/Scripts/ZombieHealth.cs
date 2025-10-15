using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    private Animator animator;
    private UnityEngine.AI.NavMeshAgent agent;
    private Collider col;

    [Header("Death Effects")]
    public GameObject deathEffect; // assign the particle prefab here

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        col = GetComponent<Collider>();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Zombie dead!");

        // Stop moving and colliding
        if (agent) agent.enabled = false;
        if (col) col.enabled = false;

        // Play death animation (optional)
        if (animator != null)
        {
            animator.CrossFade("Z_FallingForward", 0.1f);
        }

        // Spawn particle explosion
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position + Vector3.up * 1f, Quaternion.identity);
        }

        // Destroy zombie after a delay
        Destroy(gameObject, 2f);
    }
}
