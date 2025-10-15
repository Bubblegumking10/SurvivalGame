using UnityEngine;
using UnityEngine.AI;  // For pathfinding

public class ZombieAI : MonoBehaviour
{
    public float detectionRange = 25f;
    public float attackRange = 2f;
    public float wanderRadius = 15f;
    public float wanderTimer = 5f;
    public float damage = 10f;

    private Transform player;
    private NavMeshAgent agent;
    private Animator animator;

    private float timer;
    private bool isAttacking = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        timer = wanderTimer;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            AttackPlayer();
        }
        else if (distance <= detectionRange && CanSeePlayer())
        {
            ChasePlayer();
        }
        else
        {
            Wander();
        }
    }

    void Wander()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;

            // 🟡 ANIMATION PLACEHOLDER: Set to walking
            // animator.Play("Walk");
        }
    }

    void ChasePlayer()
    {
        agent.SetDestination(player.position);
        // 🟡 ANIMATION PLACEHOLDER: Set to running
        // animator.Play("Run");
    }

    void AttackPlayer()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            agent.ResetPath();

            // 🟡 ANIMATION PLACEHOLDER: Set to attacking
            // animator.Play("Attack");

            // Damage delay to match animation timing
            Invoke(nameof(DealDamage), 0.5f);
            Invoke(nameof(ResetAttack), 1.5f);  // cooldown before next attack
        }
    }

    void DealDamage()
    {
        // TODO: Call PlayerHealth.TakeDamage(damage);
        Debug.Log("Zombie dealt damage to player!");
    }

    void ResetAttack()
    {
        isAttacking = false;
    }

    bool CanSeePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, direction);

        if (angle < 60f)  // field of view
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up, direction, out hit, detectionRange))
            {
                if (hit.collider.CompareTag("Player"))
                    return true;
            }
        }
        return false;
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }
}
