using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform[] waypoints;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f;
    public Transform player;
    public GameObject detectionZone;

    private NavMeshAgent agent;
    private Animator animator;
    private int currentWaypointIndex = 0;
    private float nextFireTime = 0f;
    private bool isPlayerDetected = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        if (detectionZone != null)
        {
            Collider detectionCollider = detectionZone.GetComponent<Collider>();
            if (detectionCollider != null)
            {
                detectionCollider.isTrigger = true;
            }
        }
        SetPatrolling();
    }

    void Update()
    {
        if (isPlayerDetected)
        {
            Shoot();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerDetected = true;
            SetShooting();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerDetected = false;
            SetPatrolling();
        }
    }

    void Shoot()
    {
        if (Time.time >= nextFireTime)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void SetPatrolling()
    {
        agent.isStopped = false;
        if (waypoints.Length > 0)
        {
            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }
        animator.SetBool("isPatrolling", true);
        animator.SetBool("isShooting", false);
        animator.SetBool("isIdle", false);
    }

    void SetShooting()
    {
        agent.isStopped = true;
        animator.SetBool("isPatrolling", false);
        animator.SetBool("isShooting", true);
        animator.SetBool("isIdle", false);
    }
}
