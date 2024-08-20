using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcFollow : MonoBehaviour
{
    public Transform player;
    public float followDistance = 2.0f;

    private NavMeshAgent navMeshAgent;
    public bool isFollowing = false; // Set default to false
    private Animator animator;
    private ScoreManager scoreManager;
    public AudioButton audioButton;

    private string revive = "Revive";

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        scoreManager = FindObjectOfType<ScoreManager>();
        audioButton = FindObjectOfType<AudioButton>();

        animator.SetTrigger(revive);
    }

    void Update()
    {
        if (isFollowing)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance > followDistance)
            {
                navMeshAgent.SetDestination(player.position);
                animator.SetBool("IsMoving", true);
            }
            else
            {
                navMeshAgent.SetDestination(transform.position);
                animator.SetBool("IsMoving", false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SavePoint"))
        {
            Debug.Log("Savepoint reached");
            if (scoreManager != null)
            {
                scoreManager.IncreaseScore();
            }
            if (audioButton != null)
            {
                audioButton.PlaySavePointSound();
            }
            Destroy(gameObject);
        }
    }
}
