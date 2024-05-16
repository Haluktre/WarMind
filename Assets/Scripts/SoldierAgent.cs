using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class SoldierAgent : Agent
{
    SoldierController soldierController;

    [SerializeField] Transform _soldier;
    private Vector2 _randomPos;
    public static Action endEpisode;
    private bool _kill = false;
    private Vector2 oldPos;
    private void Start()
    {
        oldPos = transform.position;
        soldierController = GetComponent<SoldierController>();
        SoldierController.onAgentDead += SoldierController_onAgentDead;
    }

    private void SoldierController_onAgentDead()
    {
        _kill = true;
        AddReward(1);
    }

#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
    private void OnDisable()
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
    {
        SoldierController.onAgentDead -= SoldierController_onAgentDead;
    }
    public override void OnEpisodeBegin()
    {

        endEpisode?.Invoke();
        _randomPos = new Vector2(UnityEngine.Random.Range(-4, 4), UnityEngine.Random.Range(-4, 4));
        transform.position = _randomPos;

    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position.x);
        sensor.AddObservation(transform.position.y);
        sensor.AddObservation(_soldier.rotation.z);
        sensor.AddObservation(_kill);

        _kill = false;
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        soldierController.rotateDirect = actions.DiscreteActions[0];
        soldierController.isFire = actions.DiscreteActions[1];
        soldierController.horizontalInput = actions.DiscreteActions[2];
        soldierController.verticalInput = actions.DiscreteActions[3];

        soldierController.SoldierMovement();

        AddReward(-1 / MaxStep);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActions = actionsOut.DiscreteActions;

        switch (Mathf.RoundToInt(Input.GetAxisRaw("Horizontal")))
        {
            case -1: discreteActions[0] = 0; break;
            case 0: discreteActions[0] = 1; break;
            case 1: discreteActions[0] = 2; break;
        }
        switch (Mathf.RoundToInt(Input.GetAxisRaw("Vertical")))
        {
            case -1: discreteActions[1] = 0; break;
            case 0: discreteActions[1] = 1; break;
            case 1: discreteActions[1] = 2; break;
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
