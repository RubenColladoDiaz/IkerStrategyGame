using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class NPC_IAMovement : MonoBehaviour
{

    public Transform target;
    NavMeshAgent agent;

    private Vector3 lastPosition;

    private Animator animator;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        lastPosition = transform.position;

        animator = GetComponent<Animator>();

    }

    void Update()
    {
        agent.SetDestination(target.position);

        Vector3 direction = transform.position - lastPosition;
        lastPosition = transform.position;

        if (direction.x > 0)
        {
            transform.localScale = new Vector3(-3f, 3f, 3f);
            animator.SetBool("running", true);
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(3f, 3f, 3f);
            animator.SetBool("running", true);
        }
        else
        {
            animator.SetBool("running", false);
        }

    }
}