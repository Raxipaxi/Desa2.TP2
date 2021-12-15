using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{

    private SkeletonModel _skeletonModel;
    private SkeletonView _skeletonView;
    
    public PlayerModel _player;
    public float _minDistance;

    public bool isDead;
   
    [SerializeField] private float _idleCD;
    private bool _isInIdle;
    [SerializeField]private Transform[] waypoints;
    private Transform Target => _player.transform;
    private Transform _transform;
    private float distanceBetweenTarget => Vector2.Distance(_transform.position, Target.position);
    private FSM<DAxeStatesEnum> _fsm;
    private iNode _root;

    // Actions 
    public event Action OnIdle;
    public event Action<Vector2> OnWalk, OnRun;
    public event Action<int> OnAttack;
    public event Action OnDie, OnHit;
    
    private void Awake()
    {
        BakeReferences();
    }

    void BakeReferences()
    {
        _skeletonModel = GetComponent<SkeletonModel>();
        _skeletonView = GetComponent<SkeletonView>();
    }

    void Start()
    {
        _skeletonModel.Subscribe(this);
        _skeletonView.Subscribe(this);
        Subscribe();
        _isInIdle = true;
        isDead = false;
        _transform = transform;
        InitDecisionTree();
        InitFSM();
    }

    void Subscribe()
    {
        _skeletonModel.OnHit += HitCommand;
        _skeletonModel.OnDie += DieCommand;
    }

    void InitDecisionTree()
    {
        // Action Nodes
        var goToAttack = new ActionNode(() => _fsm.Transition(DAxeStatesEnum.Attack));
        var goToIdle= new ActionNode(() => _fsm.Transition(DAxeStatesEnum.Idle));
        var goToPatrol = new ActionNode(() => _fsm.Transition(DAxeStatesEnum.Patrol));
        var goToDead = new ActionNode(() => _fsm.Transition(DAxeStatesEnum.Dead));
        
        // Question Nodes

        QuestionNode isInReach = new QuestionNode(CanAttack, goToAttack, goToPatrol);
        QuestionNode isIdleTime = new QuestionNode(IsIdleCD, goToIdle, isInReach);
        QuestionNode isPlayerAlive = new QuestionNode(IsPlayerAlive, isIdleTime, goToPatrol);
        
        _root = isPlayerAlive;
    }

    void InitFSM()
    {
        // FSM States
        var idle = new SkeletonIdleState<DAxeStatesEnum>(CanAttack,IdleCommand,_idleCD,SetIdleCDOn,_root);
        var patrol = new SkeletonPatrolState<DAxeStatesEnum>(CanAttack,waypoints,_transform,WalkCommand,SetIdleCDOn, _minDistance,_root);
        var attack = new SkeletonAttackState<DAxeStatesEnum>(_skeletonModel.data.attackCooldown, AttackCommand,SetIdleCDOn, _root);
      //  var hit = new SkeletonHitState<DAxeStatesEnum>(_root);
        var dead = new SkeletonDeadState<DAxeStatesEnum>(DestroyEnemy);
    
        //Idle
        idle.AddTransition(DAxeStatesEnum.Patrol, patrol);
        idle.AddTransition(DAxeStatesEnum.Attack, attack);
        idle.AddTransition(DAxeStatesEnum.Dead, dead);

        // Patrol 
        patrol.AddTransition(DAxeStatesEnum.Idle, idle);
        patrol.AddTransition(DAxeStatesEnum.Attack, attack);
        patrol.AddTransition(DAxeStatesEnum.Dead, dead);

        // Attack
        attack.AddTransition(DAxeStatesEnum.Patrol, patrol);
        attack.AddTransition(DAxeStatesEnum.Idle, idle);
        attack.AddTransition(DAxeStatesEnum.Dead, dead);




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

    public void AttackCommand(int dmg)
    {
        OnAttack?.Invoke(_skeletonModel.data.damage);
    }

    public void DieCommand()
    {
        OnDie?.Invoke();
        _fsm.Transition(DAxeStatesEnum.Dead);

    }

    public  void DestroyEnemy()
    { 
      isDead = true;
      GetComponent<Collider2D>().enabled = false;
      GetComponent<Rigidbody2D>().isKinematic = true;

      this.enabled = false;

    }

    public void HitCommand()
    {
        OnHit?.Invoke();
    }

    #endregion
       
    public bool CanAttack()
    {
        var canAttack = _skeletonModel.data.attackDist >= distanceBetweenTarget;
        return canAttack ;
    }
    public void SetIdleCDOn(bool idleState)
    {
        _isInIdle = idleState;
    }

    public bool IsPlayerAlive()
    {
        var isalive = _player.GetIsAlive();
        return isalive;
    }
    public bool IsIdleCD()
    {
        return _isInIdle;
    }
    
    void Update()
    {
        if(!isDead) _fsm.OnUpdate();
    }
}
