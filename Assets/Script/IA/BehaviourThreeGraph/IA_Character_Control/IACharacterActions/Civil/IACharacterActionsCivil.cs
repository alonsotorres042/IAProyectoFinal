using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IACharacterActionsCivil : IACharacterActions
{
    public LayerMask maskItem;

    private void Awake()
    {
        this.LoadComponent();
    }
    public override void LoadComponent()
    {
        base.LoadComponent();

    }
    private void OnTriggerEnter(Collider other)
    {
        if ((maskItem.value & (1 << other.gameObject.layer)) != 0)
        {

            this.health.health += other.gameObject.GetComponent<HealthItem>().health;
            other.gameObject.GetComponent<HealthItem>().health = 0;
            Destroy(other.gameObject);

            //
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.speed += 20f;
                Debug.Log($"Nueva velocidad del agente: {agent.speed}");
            }
        }
    }
}
