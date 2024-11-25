using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
[TaskCategory("MyAI/BaseClass")]
public class ActionNode : Action
{

    protected IACharacterVehiculo _IACharacterVehiculo;
    protected IACharacterActions _IACharacterActions;
    protected UnitGame _UnitGame;
    public override void OnAwake()
    {
        base.OnAwake();
        _IACharacterVehiculo = GetComponent<IACharacterVehiculo>();
        _IACharacterActions = GetComponent<IACharacterActions>();
        if (_IACharacterVehiculo.health != null)
            _UnitGame = _IACharacterVehiculo.health._UnitGame;
    }
    //public override void OnStart()
    //{
        
    //    {

    //    }
    //   _UnitGame = _IACharacterVehiculo.health._UnitGame;
    //}

}