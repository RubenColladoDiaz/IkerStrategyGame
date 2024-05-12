using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class NPC_IAMovement : MonoBehaviour
{

    public Transform target;
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        agent.SetDestination(target.position);
    }
}