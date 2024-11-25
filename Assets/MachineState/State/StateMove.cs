using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMove : StateBase
{
    protected SteeringBehavior _SteeringBehavior;
    public Transform place; // Destino del movimiento

    public override void LoadComponent()
    {
        // Obtén el componente SteeringBehavior
        //_SteeringBehavior = GetComponent<SteeringBehavior>();
        //if (_SteeringBehavior == null)
        //{
        //    Debug.LogError("StateMove: No se encontró SteeringBehavior en el objeto.");
        //}
        base.LoadComponent();
    }

    public virtual void MoveToPlace()
    {
        //// Validación: verifica que el destino esté asignado
        //if (place == null)
        //{
        //    Debug.LogWarning("StateMove: El destino (place) no está asignado.");
        //    return;
        //}

        //// Calcula la fuerza de dirección hacia el destino
        //Vector3 steeringForce = _SteeringBehavior.Arrive(place);

        //// Aplica la fuerza y ajusta la magnitud, deteniéndose si está cerca
        //_SteeringBehavior.ClampMagnitude(steeringForce, place);

        //// Actualiza la posición del objeto
        //_SteeringBehavior.UpdatePosition();

        //// Detiene el movimiento si está cerca del destino
        //if (_SteeringBehavior.IsNearTarget(place, 0.1f))
        //{
        //    _SteeringBehavior.Stop(); // Detiene la velocidad
        //}
    }
}
