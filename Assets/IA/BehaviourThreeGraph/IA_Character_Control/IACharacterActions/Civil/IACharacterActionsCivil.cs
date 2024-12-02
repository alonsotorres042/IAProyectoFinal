using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            if (other.GetComponent<HealthItem>())
            {
                this.health.health += other.gameObject.GetComponent<HealthItem>().health;
                other.gameObject.GetComponent<HealthItem>().health = 0;
                Destroy(other.gameObject);
            }
            else if(other.GetComponent<FoodItem>())
            {
                Destroy(other.gameObject);
            }
        }
    }
}