using UnityEngine;

public class Driving : StateWait
{
    private Node currentNode; // Nodo actual del auto

    void Awake()
    {
        this.LoadComponent();
    }

    public override void LoadComponent()
    {
        stateType = StateType.Driving;
        base.LoadComponent();
    }

    public override void Enter()
    {
        base.Enter();

        NodeManager manager = FindObjectOfType<NodeManager>();
        if (manager == null)
        {
            Debug.LogError("Driving Enter: No se encontró NodeManager en la escena.");
            _MachineState.ActiveState(StateType.Idle);
            return;
        }

        if (currentNode == null)
        {
            currentNode = manager.GetClosestNode(transform.position);
        }

        if (currentNode == null)
        {
            Debug.LogError("Driving Enter: No se encontró un nodo inicial cercano.");
            _MachineState.ActiveState(StateType.Idle);
            return;
        }

        Node nextNode = currentNode.GetRandomConnectedNode();

        if (nextNode != null)
        {
            place = nextNode.transform; // Asigna el siguiente nodo como destino
            currentNode = nextNode; // Actualiza el nodo actual

            // **Conectar con AutoController**
            AutoController autoController = GetComponent<AutoController>();
            if (autoController != null)
            {
                autoController.SetTargetNode(nextNode.transform); // Actualiza el nodo en AutoController
            }

            stateNode = StateNode.MoveTo; // Cambia al estado MoveTo
            Debug.Log($"Driving Enter: Nodo asignado como destino: {nextNode.name}");
        }
        else
        {
            Debug.LogWarning("Driving Enter: No se encontró un nodo conectado.");
            _MachineState.ActiveState(StateType.Idle);
        }
    }


    public override void Execute()
    {
        base.Execute();

        switch (stateNode)
        {
            case StateNode.MoveTo:
                // Calcula la fuerza de dirección hacia el nodo de destino
                Vector3 steeringForce = _SteeringBehavior.Arrive(place);

                // Aplica la fuerza, deteniendo el auto si está cerca del nodo
                _SteeringBehavior.ClampMagnitude(steeringForce, place);

                // Actualiza la posición del auto
                _SteeringBehavior.UpdatePosition();

                // Cambia al siguiente estado si el auto llegó al nodo
                if (_SteeringBehavior.IsNearTarget(place, 0.1f))
                {
                    _SteeringBehavior.Stop();
                    stateNode = StateNode.StartStay;
                }
                break;

            case StateNode.StartStay:
                StartCoroutineWait(); // Inicia el temporizador para esperar
                stateNode = StateNode.Stay; // Cambia al estado Stay
                break;

            case StateNode.Stay:
                if (!WaitTime)
                {
                    _MachineState.ActiveState(StateType.Driving); // Cambia a Driving nuevamente
                }
                break;

            case StateNode.Finish:
                Debug.Log("Driving Finish: Finalizó el estado.");
                break;

            default:
                Debug.LogError($"Driving Execute: Nodo de estado desconocido: {stateNode}");
                break;
        }
    }

    public override void Exit()
    {
        base.Exit();
        stateNode = StateNode.MoveTo;
        Debug.Log("Driving Exit: Salió del estado Driving.");
    }
}
