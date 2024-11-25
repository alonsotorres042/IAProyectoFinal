using UnityEngine;

public class Driving : StateWait
{
    private Node currentNode; // Nodo actual del auto
    private AutoSensor autoSensor; // Sensor para detectar obstáculos
    private bool isPaused = false; // Indica si el auto está detenido por un obstáculo
    private float pauseDuration = 2f; // Tiempo que el auto esperará antes de reanudar
    private float pauseTimer = 0f; // Temporizador para manejar la pausa

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

        // Obtiene el AutoSensor para detección de obstáculos
        autoSensor = GetComponent<AutoSensor>();
        if (autoSensor == null)
        {
            Debug.LogError("Driving Enter: No se encontró AutoSensor en el auto.");
        }

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

        // Si hay un obstáculo detectado, detener el movimiento
        if (autoSensor != null && autoSensor.IsObstacleDetected)
        {
            if (!isPaused)
            {
                isPaused = true; // Indica que el auto está detenido
                pauseTimer = 0f; // Resetea el temporizador
                _SteeringBehavior.Stop(); // Detiene el auto
                Debug.Log("Driving: Obstáculo detectado. Pausando el auto.");
            }

            // Incrementa el temporizador mientras el obstáculo esté presente
            pauseTimer += Time.deltaTime;
            if (pauseTimer >= pauseDuration)
            {
                isPaused = false; // Reanuda el movimiento después de esperar
                Debug.Log("Driving: Tiempo de pausa completado. Reanudando movimiento.");
            }

            // Salir del método para no procesar el movimiento mientras está pausado
            return;
        }

        // Lógica normal de movimiento si no hay obstáculos o ya pasó la pausa
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
