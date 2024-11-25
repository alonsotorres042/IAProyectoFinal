using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drive : StateMove
{
    public override void LoadComponent()
    {
        // Define el tipo de estado como Driving
        stateType = StateType.Driving;
        base.LoadComponent();
    }

    public override void Enter()
    {
        Debug.Log("Drive Enter: Auto comenzó a moverse.");
        base.Enter();
    }

    public override void Execute()
    {
        if (place == null)
        {
            Debug.LogWarning("Drive Execute: No se ha definido un destino (place).");
            return;
        }

        // Calcula la fuerza de dirección hacia el destino
        Vector3 steeringForce = _SteeringBehavior.Arrive(place);

        // Aplica la fuerza y ajusta la magnitud
        _SteeringBehavior.ClampMagnitude(steeringForce, place);

        // Actualiza la posición del objeto
        _SteeringBehavior.UpdatePosition();

        // Verifica si el auto ha llegado al destino
        if (_SteeringBehavior.IsNearTarget(place, 0.1f))
        {
            Debug.Log("Drive Execute: Llegó al destino. Cambiando de estado...");
            _SteeringBehavior.Stop(); // Detiene completamente el auto
            _MachineState.ActiveState(StateType.Waiting); // Cambia al estado Waiting
        }
    }

    public override void Exit()
    {
        Debug.Log("Drive Exit: Auto detuvo su movimiento.");
        base.Exit();
    }
}
