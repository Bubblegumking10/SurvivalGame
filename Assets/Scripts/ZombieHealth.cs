using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    private Animator animator;
    private UnityEngine.AI.NavMeshAgent agent;
    private Collider col;

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

        // Disable movement
        if (agent) agent.enabled = false;
        if (col) col.enabled = false;

        // Play death animation
        animator.CrossFade("Z_FallingForward", 0.1f);
        // or animator.Play("Z_FallingBack");

        Destroy(gameObject, 4f);
    }
}
