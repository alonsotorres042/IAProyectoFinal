using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
[TaskCategory("MyAI/View")]
public class ActionNodeNotViewItem : ActionNodeView
{
     

    public override void OnAwake()
    {
        base.OnAwake();
    }
    public override TaskStatus OnUpdate()
    {
        if (_IACharacterVehiculo.AIEye is IAEyeCivil)
        {
            if (((IAEyeCivil)_IACharacterVehiculo.AIEye).ViewItems == null)
                return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }


}