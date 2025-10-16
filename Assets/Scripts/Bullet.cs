using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 50f;
    public float lifeTime = 5f;
    public float damage = 25f;

    void Start()
    {
        // Auto-destroy as a safety
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Move the bullet forward manually (frame-rate independent)
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        // Debug to help you see what was hit
        Debug.Log($"Bullet trigger hit: {other.gameObject.name} (tag: {other.tag})");

        if (other.CompareTag("Zombie"))
        {
            ZombieHealth health = other.GetComponent<ZombieHealth>();
            if (health != null)
            {
                health.TakeDamage(damage);
                Debug.Log("Applied damage to zombie");
            }
        }

        // Optionally ignore collisions with player or weapon layer:
        // if (other.CompareTag("Player")) return;

        // Destroy on any contact
        Destroy(gameObject);
    }
}
