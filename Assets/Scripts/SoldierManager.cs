using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierManager : MonoBehaviour
{
    [SerializeField] private GameObject _soldier;
    private GameObject _enemy;

    private void Start()
    {
        SoldierController_onAgentsDead();
    }
    private void OnEnable()
    {
        SoldierController.onAgentDead += SoldierController_onAgentsDead;
    }

    private void SoldierController_onAgentsDead()
    {
        if (_enemy != null)
        {
            Destroy(_enemy);
        }
        _enemy = Instantiate(_soldier, new Vector2(UnityEngine.Random.Range(-4f, 4f), UnityEngine.Random.Range(-4f, 4f)), Quaternion.identity);
    }
    private void OnDisable()
    {
        SoldierController.onAgentDead -= SoldierController_onAgentsDead;
    }
}
