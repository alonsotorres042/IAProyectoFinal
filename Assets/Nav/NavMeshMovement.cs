using UnityEngine;
using UnityEngine.AI;

public class NavMeshMovement : MonoBehaviour
{
    public Transform target;  // El objetivo al que se moverá el objeto
    private NavMeshAgent agent;

    void Start()
    {
        // Obtener el componente NavMeshAgent del GameObject
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Si se ha asignado un objetivo, el agente lo seguirá
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }
}
