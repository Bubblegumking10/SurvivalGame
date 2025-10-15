using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    private Transform player;
    private float wanderRadius = 20f;
    private float wanderTimer = 5f;
    private float timer;

    // Distances
    public float chaseDistance = 50f;
    public float attackDistance = 1.5f;
    public float viewAngle = 80f; // front view cone

    // Attack timing
    private float attackCooldown = 2f;
    private float lastAttackTime = 0f;

    // States
    private enum State { Idle, Wander, Chase, Attack }
    private State currentState = State.Idle;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        // Check if placed on NavMesh
        if (!agent.isOnNavMesh)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(transform.position, out hit, 5f, NavMesh.AllAreas))
            {
                transform.position = hit.position;
            }
            else
            {
                Debug.LogWarning("Zombie not on NavMesh — destroying.");
                Destroy(gameObject);
                return;
            }
        }

        timer = wanderTimer;
        currentState = State.Wander;
    }

    void Update()
    {
        if (player == null || agent == null || !agent.isOnNavMesh)
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if player is in front view
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        bool playerInFront = angle < viewAngle * 0.5f;

        // State logic
        if (distanceToPlayer <= attackDistance && playerInFront)
        {
            currentState = State.Attack;
        }
        else if (distanceToPlayer <= chaseDistance && playerInFront)
        {
            currentState = State.Chase;
        }
        else
        {
            currentState = State.Wander;
        }

        switch (currentState)
        {
            case State.Wander:
                Wander();
                break;

            case State.Chase:
                ChasePlayer();
                break;

            case State.Attack:
                AttackPlayer();
                break;
            default:
                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", false);
                break;
        }
    }

    void Wander()
    {
        timer += Time.deltaTime;
        agent.speed = 1.5f;

        // ✅ WALK ANIMATION
        animator.SetBool("isWalking", true);
        animator.SetBool("isRunning", false);

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }
    }

    void ChasePlayer()
    {
        if (player == null) return;

        agent.SetDestination(player.position);
        agent.speed = 3.5f;

        // ✅ RUN ANIMATION
        animator.SetBool("isRunning", true);
        animator.SetBool("isWalking", false);
}

    void AttackPlayer()
    {
        agent.ResetPath();

        // ✅ ATTACK ANIMATION
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", false);
        animator.SetTrigger("Attack");

        if (Time.time > lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            Debug.Log("Zombie attacks player!");
            // Optional: deal damage here
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;
        randomDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);
        return navHit.position;
    }
}
