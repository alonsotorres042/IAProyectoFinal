using UnityEngine;

public class Idle : StateBase
{
    public override void Enter()
    {
        Debug.Log("Idle Enter: Auto detenido.");
        base.Enter();

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

        // El auto no hace nada en este estado
        Debug.Log("Idle Execute: Esperando acción.");
    }

    public override void Exit()
    {
        Debug.Log("Idle Exit: Saliendo del estado Idle.");
        base.Exit();
    }
}
