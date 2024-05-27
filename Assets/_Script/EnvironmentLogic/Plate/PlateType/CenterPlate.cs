using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterPlate : BasePlate
{
    private bool isExecuting;

    [SerializeField]private float maxEnergy;
    private float currentEnergy;
    [SerializeField] private float consumeSpeed;

    private void Start()
    {
        isExecuting = false;
        currentEnergy = 0f;
    }

    private void Update()
    {
        energyConsuming();
    }

    public override void Interact()
    {
        base.Interact();

        //TODO:Add Energys
    }

    public override void InteractAlt()
    {
        base.InteractAlt();

        //execute toggle logic
        //turn on/off your center
        isExecuting = !isExecuting;
    }
    private void energyConsuming()
    {
        if (isExecuting)
        {
            currentEnergy -= Time.deltaTime * consumeSpeed;

            if (currentEnergy < 0f)
            {
                isExecuting = false;
                currentEnergy = 0f;
            }
        }
    }

}
