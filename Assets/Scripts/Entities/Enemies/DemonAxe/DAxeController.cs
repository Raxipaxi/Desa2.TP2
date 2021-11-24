using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DAxeController : MonoBehaviour
{

    private DAxeModel _dAxeModel;
    private DAxeView _dAxeView;
    private LineOfSightAI2D _lineOfSight;
    public PlayerModel _player;
    public float _minDistance;
   
    [SerializeField] private float _idleCD;
    private bool _isInIdle;
    [SerializeField]private Transform[] waypoints;
    private Transform Target => _player.transform;
    private Transform _transform;

    private FSM<DAxeStatesEnum> _fsm;
    private iNode _root;

    // Actions 
    public event Action OnIdle;
    public event Action<Vector2> OnWalk, OnRun;

    public event Action<int> OnAttack;
    public event Action OnDie;
    
    
    private void Awake()
    {
        BakeReferences();
    }

    void BakeReferences()
    {
        _dAxeModel = GetComponent<DAxeModel>();
        _dAxeView = GetComponent<DAxeView>();
        _lineOfSight = GetComponent<LineOfSightAI2D>();
    }

    void Start()
    {
        _dAxeModel.Subscribe(this);
        _dAxeView.Subscribe(this);
        _isInIdle = true;
        _transform = transform;
        InitDecisionTree();
        InitFSM();
    }

    void InitDecisionTree()
    {
        // Action Nodes
        var goToAttack = new ActionNode(() => _fsm.Transition(DAxeStatesEnum.Attack));
        var goToIdle= new ActionNode(() => _fsm.Transition(DAxeStatesEnum.Idle));
        var goToPatrol = new ActionNode(() => _fsm.Transition(DAxeStatesEnum.Patrol));
        var goToRun = new ActionNode(() => _fsm.Transition(DAxeStatesEnum.Run));
        var goToDead = new ActionNode(() => _fsm.Transition(DAxeStatesEnum.Dead));
        
        // Question Nodes

        QuestionNode isInReach = new QuestionNode(CanAttack, goToAttack, goToRun);
        QuestionNode isPlayerOnSight = new QuestionNode(CheckSeesPlayer, isInReach, goToPatrol);
        QuestionNode isIdleTime = new QuestionNode(IsIdleCD, goToIdle, isPlayerOnSight);
        
        _root = isIdleTime;
    }

    void InitFSM()
    {
        // FSM States
        var idle = new DAxeIdleState<DAxeStatesEnum>(CanSeeTheTarget,IdleCommand,_idleCD,SetIdleCDOn,_root);
        var patrol = new DAxePatrolState<DAxeStatesEnum>(CanSeeTheTarget, waypoints,_transform,WalkCommand,SetIdleCDOn, _minDistance,_root);
        var run = new DAxeRunState<DAxeStatesEnum>( RunCommand,Target,CanAttack, CanSeeTheTarget, _root);
        var attack = new DAxeAttackState<DAxeStatesEnum>();
        var hit = new DAxeHitState<DAxeStatesEnum>();
        var dead = new DAxeDeadState<DAxeStatesEnum>();

        //Idle
        idle.AddTransition(DAxeStatesEnum.Patrol, patrol);
        idle.AddTransition(DAxeStatesEnum.Attack, attack);
        idle.AddTransition(DAxeStatesEnum.Run, run);
        idle.AddTransition(DAxeStatesEnum.Hit, hit);

        // Patrol 
        patrol.AddTransition(DAxeStatesEnum.Idle, idle);
        patrol.AddTransition(DAxeStatesEnum.Run, run);
        patrol.AddTransition(DAxeStatesEnum.Attack, attack);
        patrol.AddTransition(DAxeStatesEnum.Hit, hit);

        // Attack
        attack.AddTransition(DAxeStatesEnum.Patrol, patrol);
        attack.AddTransition(DAxeStatesEnum.Idle, idle);
        attack.AddTransition(DAxeStatesEnum.Run, run);
        attack.AddTransition(DAxeStatesEnum.Hit, hit);

        // Run
        run.AddTransition(DAxeStatesEnum.Patrol, patrol);
        run.AddTransition(DAxeStatesEnum.Idle, idle);
        run.AddTransition(DAxeStatesEnum.Attack, attack);
        run.AddTransition(DAxeStatesEnum.Hit, hit);

        // Hit
        hit.AddTransition(DAxeStatesEnum.Patrol, patrol);
        hit.AddTransition(DAxeStatesEnum.Idle, idle);
        hit.AddTransition(DAxeStatesEnum.Attack, attack);
        hit.AddTransition(DAxeStatesEnum.Dead, dead);
        hit.AddTransition(DAxeStatesEnum.Run, run);

        _fsm = new FSM<DAxeStatesEnum>();
        _fsm.SetInit(idle);
    }

    #region Commands

    public void IdleCommand()
    {
        OnIdle?.Invoke();
    }

    public void WalkCommand(Vector2 dir)

    {
        OnWalk?.Invoke(dir);
    }

    public void RunCommand(Vector2 dir)

    {
        OnRun?.Invoke(dir);
    }

    public void DieCommand()
    {
        OnDie?.Invoke();
    }

    #endregion
    public bool CanSeeTheTarget()
    {
        return _lineOfSight.SingleTargetInSight(Target);
    }
    public bool CanAttack()
    {
        return _dAxeModel.data.attackDist <= Vector2.Distance(transform.position, Target.position);
    }

    public void SetIdleCDOn(bool idleState)
    {
        _isInIdle = idleState;
    }

    public bool IsIdleCD()
    {
        return _isInIdle;
    }

    public bool CheckSeesPlayer()
    {
        return _lineOfSight.SingleTargetInSight(_player.transform);
    }
    
    // Update is called once per frame
    void Update()
    {
        _fsm.OnUpdate();
    }
}
