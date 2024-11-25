using UnityEngine;

public class Waiting : StateBase
{
    private AutoSensor autoSensor; // Referencia al sensor de detección
    private float waitTime = 2f; // Tiempo que el auto espera antes de reanudar
    private float elapsedTime; // Tiempo acumulado desde que comenzó a esperar

    void Awake()
    {
        this.LoadComponent();
    }

    public override void LoadComponent()
    {
        stateType = StateType.Waiting;
        base.LoadComponent();
    }

    public override void Enter()
    {
        base.Enter();
        elapsedTime = 0f;

        // Obtiene el sensor de detección
        autoSensor = GetComponent<AutoSensor>();
        if (autoSensor == null)
        {
            Debug.LogError("Waiting Enter: No se encontró el AutoSensor en el auto.");
        }

        // Detiene el auto
        SteeringBehavior steering = GetComponent<SteeringBehavior>();
        if (steering != null)
        {
            steering.Stop();
        }

        Debug.Log("Waiting Enter: El auto está esperando.");
    }

    public override void Execute()
    {
        base.Execute();

        if (autoSensor != null && autoSensor.IsObstacleDetected)
        {
            // Resetea el temporizador si el obstáculo persiste
            elapsedTime = 0f;
        }
        else
        {
            // Incrementa el tiempo acumulado
            elapsedTime += Time.deltaTime;

            // Si el tiempo de espera ha pasado, cambia al estado Driving
            if (elapsedTime >= waitTime)
            {
                _MachineState.ActiveState(StateType.Driving);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Waiting Exit: Salió del estado Waiting.");
    }
}
