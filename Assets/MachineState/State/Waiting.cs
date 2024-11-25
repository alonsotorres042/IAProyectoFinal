using UnityEngine;

public class Waiting : StateBase
{
    public float waitTime = 3f; // Tiempo en segundos que el auto esperará
    private float timer;

    public override void Enter()
    {
        Debug.Log("Waiting Enter: Auto esperando.");
        base.Enter();

        // Resetea el temporizador
        timer = waitTime;

        // Detener el auto
        SteeringBehavior steering = GetComponent<SteeringBehavior>();
        if (steering != null)
        {
            steering.Stop();
        }
    }

    public override void Execute()
    {
        base.Execute();

        // Cuenta hacia atrás
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            Debug.Log("Waiting Execute: Tiempo de espera terminado, cambiando a Driving.");
            _MachineState.ActiveState(StateType.Driving); // Cambia al estado Driving
        }
    }

    public override void Exit()
    {
        Debug.Log("Waiting Exit: Saliendo del estado Waiting.");
        base.Exit();
    }
}
