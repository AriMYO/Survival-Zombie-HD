using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    private float speed = 10f;
    private Animator animator;
    private NavMeshAgent agent;
    public Transform player;
    private SphereCollider sphereCollider;
    private BoxCollider boxCollider;
    public GameManager gameManager;
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        sphereCollider = GetComponent<SphereCollider>();
        boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {

        if (sphereCollider.bounds.Contains(player.position))
        {
            agent.isStopped = true;
            animator.SetBool("ViewUser", true);

            if (boxCollider.bounds.Contains(player.position) && !IsAttackActive())
            {
                agent.isStopped = true;
                animator.Play("Zombie Attack");
                gameManager.TakeDamage();
                return;
            }


            if (!IsScreamActive() && !IsWalkActive() && !IsAttackActive())
            {
                animator.Play("Zombie Scream");
            }

            if (IsWalkActive())
            {
                agent.isStopped = false;
                agent.destination = player.position;
            }

        }
        else
        {
            animator.Play("Zombie Idle");
            animator.SetBool("ViewUser", false);
            agent.isStopped = true;
        }

    }


    private bool IsWalkActive()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName("Zombie Walk");
    }

    private bool IsScreamActive()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName("Zombie Scream");
    }

    private bool IsAttackActive()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName("Zombie Attack");
    }

}
