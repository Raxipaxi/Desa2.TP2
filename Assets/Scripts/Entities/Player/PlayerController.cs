using UnityEngine;
using System;
using System.Globalization;

public class PlayerController : MonoBehaviour
{

    private FSM<PlayerStatesEnum> _fsm;
    private PlayerModel _playerModel;
    private PlayerView _playerView;
    [SerializeField]private LifeUIManager _lifeUI;
    private PlayerLifeController _playerLifeController;
    private iInput _playerInput;
    private bool isCrouch;
    
    #region Actions
    public event Action<int> OnAttack;
    public event Action<Vector2> OnMove;
    public event Action OnJump;
    public event Action OnFall;
    public event Action OnLand;
    public event Action OnIdle;
    public event Action OnDie;

    public event Action<bool> OnCrouch;
    public event Action<int> OnHit;
    #endregion
    
    
    private void Awake()
    {
        _playerModel = GetComponent<PlayerModel>();
        _playerInput = GetComponent<iInput>();
        _playerView = GetComponent<PlayerView>();
        _playerLifeController = GetComponent<PlayerLifeController>();
        _lifeUI.Subscribe(this);
        _playerModel.SubscribeEvents(this);
        _playerView.SubscribeEvents(this);
        _playerLifeController.Subscribe(this);

        isCrouch = false;

        SubscribeEvents();
        
        FsmInit();

    }

    private void FsmInit()
    {
        //--------------- FSM Creation -------------------//                
        var idle = new PlayerIdleState<PlayerStatesEnum>(CheckGroundPlayer,CheckJumpPlayer,IdleCommand, PlayerStatesEnum.Run,PlayerStatesEnum.Fall,PlayerStatesEnum.Attack,PlayerStatesEnum.Crouch,PlayerStatesEnum.Jump,_playerInput );
        var run = new PlayerRunState<PlayerStatesEnum>(CheckGroundPlayer,CheckJumpPlayer, PlayerStatesEnum.Idle, PlayerStatesEnum.Jump,PlayerStatesEnum.Fall,PlayerStatesEnum.Attack,_playerInput,MoveCommand);
        var jump = new PlayerJumpState<PlayerStatesEnum>(PlayerStatesEnum.Fall, PlayerStatesEnum.Idle,JumpCommand,CheckJumpPlayer);
        var fall = new PlayerFallState<PlayerStatesEnum>(PlayerStatesEnum.Land,PlayerStatesEnum.Jump,_playerInput,CheckGroundPlayer,FallCommand, MoveCommand);
        var crouch = new PlayerCrouchState<PlayerStatesEnum>(CheckGroundPlayer, PlayerStatesEnum.Attack,PlayerStatesEnum.Idle, PlayerStatesEnum.Fall,_playerInput,MoveCommand ,CrouchCommand);
        var land = new PlayerLandState<PlayerStatesEnum>(PlayerStatesEnum.Idle, LandCommand);
        var hit = new PlayerHitState<PlayerStatesEnum>(PlayerStatesEnum.Idle);
        var attack = new PlayerAttackState<PlayerStatesEnum>(PlayerStatesEnum.Idle,PlayerStatesEnum.Run,AttackCommand,1,_playerInput);
        var dead = new PlayerDeadState<PlayerStatesEnum>(DieCommand);

        // Idle State
        idle.AddTransition(PlayerStatesEnum.Run, run);
        idle.AddTransition(PlayerStatesEnum.Jump,jump);
        idle.AddTransition(PlayerStatesEnum.Fall,fall);
        idle.AddTransition(PlayerStatesEnum.Attack, attack);
        idle.AddTransition(PlayerStatesEnum.Hit, hit);
        idle.AddTransition(PlayerStatesEnum.Crouch, crouch);
        idle.AddTransition(PlayerStatesEnum.Dead, dead);

        // Idle State
        crouch.AddTransition(PlayerStatesEnum.Run, run);
        crouch.AddTransition(PlayerStatesEnum.Fall,fall);
        crouch.AddTransition(PlayerStatesEnum.Attack, attack);
        crouch.AddTransition(PlayerStatesEnum.Hit, hit);
        crouch.AddTransition(PlayerStatesEnum.Idle, idle);
        crouch.AddTransition(PlayerStatesEnum.Dead, dead);
        
        // Run State
        run.AddTransition(PlayerStatesEnum.Idle, idle);
        run.AddTransition(PlayerStatesEnum.Fall,fall);
        run.AddTransition(PlayerStatesEnum.Jump,jump);
        run.AddTransition(PlayerStatesEnum.Attack,attack);
        run.AddTransition(PlayerStatesEnum.Hit, hit);
        run.AddTransition(PlayerStatesEnum.Crouch, crouch);
        run.AddTransition(PlayerStatesEnum.Dead, dead);
        
        // Jump State
       jump.AddTransition(PlayerStatesEnum.Fall,fall);
       jump.AddTransition(PlayerStatesEnum.Idle,idle);
       jump.AddTransition(PlayerStatesEnum.Hit, hit);
       jump.AddTransition(PlayerStatesEnum.Dead, dead);
        
        // Fall State
        fall.AddTransition(PlayerStatesEnum.Land,land);
        fall.AddTransition(PlayerStatesEnum.Jump,jump);
        fall.AddTransition(PlayerStatesEnum.Hit, hit);
        fall.AddTransition(PlayerStatesEnum.Dead, dead);
        
        // Land State
        land.AddTransition(PlayerStatesEnum.Idle,idle);
        land.AddTransition(PlayerStatesEnum.Hit, hit);
        land.AddTransition(PlayerStatesEnum.Dead, dead);
      
        // Attack State
        attack.AddTransition(PlayerStatesEnum.Idle,idle);
        attack.AddTransition(PlayerStatesEnum.Run,run);
        attack.AddTransition(PlayerStatesEnum.Crouch, crouch);
        attack.AddTransition(PlayerStatesEnum.Hit, hit);
        attack.AddTransition(PlayerStatesEnum.Dead, dead);
        
        // Hit State
        hit.AddTransition(PlayerStatesEnum.Idle,idle);
        hit.AddTransition(PlayerStatesEnum.Dead, dead);
        
        _fsm = new FSM<PlayerStatesEnum>();
        // Set init state
        _fsm.SetInit(idle);

    }

    #region Model Questions

    private bool CheckGroundPlayer()
    {
        return _playerModel.CheckIfGrounded();
    }

    private bool CheckJumpPlayer()
    {
        return _playerModel.IsJumping();
    }
    

    #endregion
    
    
    #region Commands
    public void AttackCommand(int dmg)
    {
        OnAttack?.Invoke(dmg);
    }
    public void MoveCommand(Vector2 dir)
    {
        OnMove?.Invoke(dir);
    }
    public void JumpCommand()
    {
        OnJump?.Invoke();
    }
    public void LandCommand()
    {
        OnLand?.Invoke();
    }
    public void IdleCommand()
    {
        OnIdle?.Invoke();
    }
    public void FallCommand()
    {
        OnFall?.Invoke();
    }

    public void HitCommand(int damage)
    {
        _fsm.Transition(PlayerStatesEnum.Hit);
        OnHit?.Invoke(damage);   
    }

    public void CrouchCommand(bool crouch)
    {
        OnCrouch?.Invoke(crouch);
    }

    
    public void DieCommand()
    {
        OnDie?.Invoke();
        
        _fsm.Transition(PlayerStatesEnum.Dead);
    }
    public void DeadBrain(){
        _fsm.Transition(PlayerStatesEnum.Dead);
    }
    #endregion
    private void Update()
    {
        if (_playerModel != null) 
        {
            _fsm.OnUpdate();
            
        }
    }

    void SubscribeEvents()
    {
        _playerModel.OnHit += HitCommand;
        _playerLifeController.OnDie += DieCommand;
    }
    
}
