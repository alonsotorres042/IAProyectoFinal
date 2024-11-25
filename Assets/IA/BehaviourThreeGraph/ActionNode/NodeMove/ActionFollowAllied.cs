using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
[TaskCategory("MyAI/Move")]
public class ActionFollowAllied : ActionNodeVehicle
{
    public override void OnAwake()
    {
        base.OnAwake();
    }
    public override TaskStatus OnUpdate()
    {
        if(_IACharacterVehiculo.health.IsDead)
            return TaskStatus.Failure;

        SwitchUnit();

        return TaskStatus.Success;

    }
    void SwitchUnit()
    {
        switch (_UnitGame)
        {
            case UnitGame.Zombie:
                if(_IACharacterVehiculo is IACharacterVehiculoZombie)
                {
                    ((IACharacterVehiculoZombie)_IACharacterVehiculo).LookAllied();
                    ((IACharacterVehiculoZombie)_IACharacterVehiculo).MoveToAllied();
                }

                break;
            case UnitGame.Soldier:
                if (_IACharacterVehiculo is IACharacterVehiculoSoldier)
                {
                    ((IACharacterVehiculoSoldier)_IACharacterVehiculo).LookAllied();
                    ((IACharacterVehiculoSoldier)_IACharacterVehiculo).MoveToAllied();
                }
                break;
            case UnitGame.Civil:
                if (_IACharacterVehiculo is IACharacterVehiculoCivil)
                {
                    ((IACharacterVehiculoCivil)_IACharacterVehiculo).LookAllied();
                    ((IACharacterVehiculoCivil)_IACharacterVehiculo).MoveToAllied();
                }
                break;
            case UnitGame.None:
                break;
            default:
                break;
        }
    }
}