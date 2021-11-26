using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DAxeController : MonoBehaviour
{

    private DAxeModel _dAxeModel;
    private DAxeView _dAxeView;
    [SerializeField]private float _sightLenght;
    [SerializeField]private float _seen;
    
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
        _dAxeModel = GetComponent<DAxeModel>();
        _dAxeView = GetComponent<DAxeView>();
    }

    void Start()
    {
        _dAxeModel.Subscribe(this);
        _dAxeView.Subscribe(this);
        Subscribe();
        _isInIdle = true;
        isDead = false;
        _transform = transform;
        InitDecisionTree();
        InitFSM();
    }

    void Subscribe()
    {
        _dAxeModel.OnHit += HitCommand;
        _dAxeModel.OnDie += DieCommand;
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
        QuestionNode isPlayerOnSight = new QuestionNode(CanSeeTheTarget, isInReach, goToPatrol);
        QuestionNode isIdleTime = new QuestionNode(IsIdleCD, goToIdle, isPlayerOnSight);
        
        _root = isIdleTime;
    }

    void InitFSM()
    {
        // FSM States
        var idle = new DAxeIdleState<DAxeStatesEnum>(CanSeeTheTarget,IdleCommand,_idleCD,SetIdleCDOn,_root);
        var patrol = new DAxePatrolState<DAxeStatesEnum>(CanSeeTheTarget, waypoints,_transform,WalkCommand,SetIdleCDOn, _minDistance,_root);
        var run = new DAxeRunState<DAxeStatesEnum>( RunCommand,Target,CanAttack, CanSeeTheTarget, _root);
        var attack = new DAxeAttackState<DAxeStatesEnum>(_dAxeModel.data.attackCooldown, AttackCommand,SetIdleCDOn, _root);
        var hit = new DAxeHitState<DAxeStatesEnum>(_root);
        var dead = new DAxeDeadState<DAxeStatesEnum>(DestroyEnemy);
    
        //Idle
        idle.AddTransition(DAxeStatesEnum.Patrol, patrol);
        idle.AddTransition(DAxeStatesEnum.Attack, attack);
        idle.AddTransition(DAxeStatesEnum.Run, run);
        idle.AddTransition(DAxeStatesEnum.Hit, hit);
        idle.AddTransition(DAxeStatesEnum.Dead, dead);

        // Patrol 
        patrol.AddTransition(DAxeStatesEnum.Idle, idle);
        patrol.AddTransition(DAxeStatesEnum.Run, run);
        patrol.AddTransition(DAxeStatesEnum.Attack, attack);
        patrol.AddTransition(DAxeStatesEnum.Hit, hit);
        patrol.AddTransition(DAxeStatesEnum.Dead, dead);

        // Attack
        attack.AddTransition(DAxeStatesEnum.Patrol, patrol);
        attack.AddTransition(DAxeStatesEnum.Idle, idle);
        attack.AddTransition(DAxeStatesEnum.Run, run);
        attack.AddTransition(DAxeStatesEnum.Hit, hit);
        attack.AddTransition(DAxeStatesEnum.Dead, dead);

        // Run
        run.AddTransition(DAxeStatesEnum.Patrol, patrol);
        run.AddTransition(DAxeStatesEnum.Idle, idle);
        run.AddTransition(DAxeStatesEnum.Attack, attack);
        run.AddTransition(DAxeStatesEnum.Hit, hit);
        run.AddTransition(DAxeStatesEnum.Dead, dead);

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

    public void AttackCommand(int dmg)
    {
        OnAttack?.Invoke(_dAxeModel.data.damage);
    }

    public void DieCommand()
    {
        OnDie?.Invoke();
        Debug.Log("morimo no?");
        _fsm.Transition(DAxeStatesEnum.Dead);

    }

    public  void DestroyEnemy()
    { 
      
      Debug.Log("morimo?");
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
    public bool CanSeeTheTarget()
    {
        var canSee = distanceBetweenTarget - _sightLenght;
        var iCanSeeU = canSee <= _seen;

        return iCanSeeU;
    }
       
    public bool CanAttack()
    {
        var canAttack = _dAxeModel.data.attackDist >= distanceBetweenTarget;
        return canAttack ;
    }
    public void SetIdleCDOn(bool idleState)
    {
        _isInIdle = idleState;
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
