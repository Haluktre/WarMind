using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class SoldierAgent : Agent
{
    SoldierController soldierController;

    private void Start()
    {
        soldierController = GetComponent<SoldierController>();
    }
    public override void CollectObservations(VectorSensor sensor)
    {

    }

    public override void OnEpisodeBegin()
    {
        base.OnEpisodeBegin();
    }

    public override void OnActionReceived(ActionBuffers actions)
    {

        soldierController.horizontalInput = actions.DiscreteActions[0];
        soldierController.verticalInput = actions.DiscreteActions[1];
        soldierController.rotateDirect = actions.DiscreteActions[2];
        soldierController.isFire = actions.DiscreteActions[3];

        soldierController.SoldierMovement();
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActions = actionsOut.DiscreteActions;

        switch (Mathf.RoundToInt(Input.GetAxisRaw("Horizontal")))
        {
            case -1: discreteActions[0] = -1; break;
            case 0: discreteActions[0] = 0; break;
            case 1: discreteActions[0] = 1; break;
        }
        switch (Mathf.RoundToInt(Input.GetAxisRaw("Vertical")))
        {
            case -1: discreteActions[1] = -1; break;
            case 0: discreteActions[1] = 0; break;
            case 1: discreteActions[1] = 1; break;
        }

        if (Input.GetKey(KeyCode.E))
        {
            discreteActions[2] = -1;
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            discreteActions[2] = 1;
        }
        else
        {
            discreteActions[2] = 0;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            discreteActions[3] = 1;
        }
        else
        {
            discreteActions[3] = 0;
        }
    }



}
