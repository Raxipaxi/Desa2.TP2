using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DAxeController : MonoBehaviour
{

    private DAxeModel _dAxeModel;

    private FSM<EnemyStatesEnum> _fsm;
    
    // Start is called before the first frame update
    private void Awake()
    {
        BakeReferences();
    }

    void BakeReferences()
    {
        _dAxeModel = GetComponent<DAxeModel>();
        
    }

    void Start()
    {
        InitDecisionTree();
        InitFSM();
    }

    void InitDecisionTree()
    {
        
        
    }
    void InitFSM()
    {
        // FSM States
        var idle = new DAxeIdleState<DAxeStatesEnum>();
        var patrol = new DAxeIdleState<DAxeStatesEnum>();
        var run = new DAxeIdleState<DAxeStatesEnum>();
        var attack = new DAxeIdleState<DAxeStatesEnum>();
        var hit = new DAxeHitState<DAxeStatesEnum>();
        var dead = new DAxeIdleState<DAxeStatesEnum>();
        
        //Idle
        idle.AddTransition(DAxeStatesEnum.Patrol,patrol);
        idle.AddTransition(DAxeStatesEnum.Attack,attack);
        idle.AddTransition(DAxeStatesEnum.Run,run);
        idle.AddTransition(DAxeStatesEnum.Dead,dead);
        idle.AddTransition(DAxeStatesEnum.Hit,hit);
        
        // Patrol 
        patrol.AddTransition(DAxeStatesEnum.Idle,idle);
        patrol.AddTransition(DAxeStatesEnum.Run,run);
        patrol.AddTransition(DAxeStatesEnum.Dead,dead);
        patrol.AddTransition(DAxeStatesEnum.Attack,attack);
        patrol.AddTransition(DAxeStatesEnum.Hit,hit);
        
        // Attack
        attack.AddTransition(DAxeStatesEnum.Patrol,patrol);
        attack.AddTransition(DAxeStatesEnum.Idle,idle);
        attack.AddTransition(DAxeStatesEnum.Run,run);
        attack.AddTransition(DAxeStatesEnum.Dead,dead);
        attack.AddTransition(DAxeStatesEnum.Hit,hit);
        
        // Run
        run.AddTransition(DAxeStatesEnum.Patrol,patrol);
        run.AddTransition(DAxeStatesEnum.Idle,idle);
        run.AddTransition(DAxeStatesEnum.Attack,attack);
        run.AddTransition(DAxeStatesEnum.Dead,dead);
        run.AddTransition(DAxeStatesEnum.Hit,hit);
        
        //
        
        
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
