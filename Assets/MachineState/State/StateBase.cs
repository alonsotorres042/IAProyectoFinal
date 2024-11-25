using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBase : MonoBehaviour
{
    protected MachineState _MachineState; // Referencia a la máquina de estados
    public StateType stateType; // Tipo del estado (Idle, Driving, Waiting, etc.)
    public StateNode stateNode; // Nodo de estado (MoveTo, Stay, etc.)

    // Carga los componentes necesarios
    public virtual void LoadComponent()
    {
        _MachineState = GetComponent<MachineState>();
        if (_MachineState == null)
        {
            Debug.LogError($"StateBase: No se encontró MachineState en el objeto {gameObject.name}.");
        }
    }

    // Método llamado al entrar en el estado
    public virtual void Enter()
    {
        //Debug.Log($"{stateType} Enter: Entrando en el estado {stateType}.");
    }

    // Método que se ejecuta en cada frame mientras el estado está activo
    public virtual void Execute()
    {
        //Debug.Log($"{stateType} Execute: Ejecutando estado {stateType}.");
    }

    // Método llamado al salir del estado
    public virtual void Exit()
    {
        //Debug.Log($"{stateType} Exit: Saliendo del estado {stateType}.");
    }

    // Obtiene un tipo de estado aleatorio que sea diferente del actual
    public StateType GetRandomStateType()
    {
        // Obtiene todos los valores del enum StateType
        StateType[] values = (StateType[])System.Enum.GetValues(typeof(StateType));

        // Filtra los valores para excluir el estado actual
        List<StateType> possibleValues = new List<StateType>();
        foreach (StateType value in values)
        {
            if (value != stateType)
            {
                possibleValues.Add(value);
            }
        }

        // Retorna un valor aleatorio de los posibles
        if (possibleValues.Count > 0)
        {
            int randomIndex = Random.Range(0, possibleValues.Count);
            return possibleValues[randomIndex];
        }

        Debug.LogWarning("StateBase: No hay otros estados posibles para seleccionar.");
        return stateType; // Devuelve el estado actual si no hay otros
    }
}
