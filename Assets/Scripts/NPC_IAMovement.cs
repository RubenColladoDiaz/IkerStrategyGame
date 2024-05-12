using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class NPC_IAMovement : MonoBehaviour
{

    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.autoBraking = false;

        GotoNextPoint();
    }


    void GotoNextPoint()
    {
        if (points.Length == 0)
            return;

        agent.destination = points[destPoint].position;

        destPoint = (destPoint + 1) % points.Length;
    }


    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();
    }
}


/*
   Los puntos de patrullaje son proporcionados al script utilizando un arreglo p�blico de Transforms. Este arreglo puede 
   ser asignado desde el inspector utilizando GameObjects para marcar la posici�n de los puntos. La funci�n GotoNextPoint 
   figura el punto de destino para el agente (que tambi�n lo empieza a mover) y luego selecciona el nuevo destino que 
   ser� utilizado en el siguiente llamado. Como se mantiene, el c�digo hace un ciclo entre los puntos en la secuencia que 
   estos ocurren en el arreglo pero usted puede f�cilmente modificar esto, digamos al utilizar Random.Range para escoger 
   un indice del arreglo en aleatorio.
 */