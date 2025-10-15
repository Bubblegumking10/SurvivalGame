using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 50f;
    public float lifeTime = 5f;
    public float damage = 25f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * speed;

        // Destroy after lifetime expires
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // If we hit a zombie
        if (collision.gameObject.CompareTag("Zombie"))
        {
            // Deal damage (requires ZombieHealth script on zombie)
            ZombieHealth health = collision.gameObject.GetComponent<ZombieHealth>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }

        // Destroy bullet on any collision
        Destroy(gameObject);
    }
}
