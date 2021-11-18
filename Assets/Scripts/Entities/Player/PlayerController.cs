using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{

    private FSM<PlayerStatesEnum> _fsm;
    private PlayerModel _playerModel;
    private iInput _playerInput;

    #region Actions
    public event Action<int> OnAttack;
    public event Action<Vector2> OnMove;
    public event Action OnJump;
    public event Action OnFall;
    public event Action OnLand;
    public event Action OnIdle;
    #endregion
    
    
    private void Awake()
    {
        _playerModel = GetComponent<PlayerModel>();
        _playerModel.SubscribeEvents(this);
        _playerInput = GetComponent<iInput>();
        
        
        FsmInit();
    }
    
    
    private void FsmInit()
    {
        //--------------- FSM Creation -------------------//                
        var idle = new PlayerIdleState<PlayerStatesEnum>(CheckGroundPlayer,CheckJumpPlayer,IdleCommand, PlayerStatesEnum.Run,PlayerStatesEnum.Fall,PlayerStatesEnum.Attack,PlayerStatesEnum.Jump,_playerInput );
        var run = new PlayerRunState<PlayerStatesEnum>(CheckGroundPlayer,CheckJumpPlayer, PlayerStatesEnum.Idle, PlayerStatesEnum.Jump,PlayerStatesEnum.Fall,PlayerStatesEnum.Attack,_playerInput,MoveCommand);
        var jump = new PlayerJumpState<PlayerStatesEnum>(PlayerStatesEnum.Fall, PlayerStatesEnum.Idle,JumpCommand,CheckJumpPlayer);
        var fall = new PlayerFallState<PlayerStatesEnum>(PlayerStatesEnum.Land,_playerInput,CheckGroundPlayer,FallCommand, MoveCommand);
        var land = new PlayerLandState<PlayerStatesEnum>(PlayerStatesEnum.Idle, LandCommand);
        var attack = new PlayerAttackState<PlayerStatesEnum>(PlayerStatesEnum.Idle,PlayerStatesEnum.Run,AttackCommand,_playerModel.data.damage,_playerInput);

        // Idle State
        idle.AddTransition(PlayerStatesEnum.Run, run);
        idle.AddTransition(PlayerStatesEnum.Jump,jump);
        idle.AddTransition(PlayerStatesEnum.Fall,fall);
        idle.AddTransition(PlayerStatesEnum.Attack, attack);
        
        // Run State
        run.AddTransition(PlayerStatesEnum.Idle, idle);
        run.AddTransition(PlayerStatesEnum.Fall,fall);
        run.AddTransition(PlayerStatesEnum.Jump,jump);
        run.AddTransition(PlayerStatesEnum.Attack,attack);
        
        // Jump State
       jump.AddTransition(PlayerStatesEnum.Fall,fall);
       jump.AddTransition(PlayerStatesEnum.Idle,idle);
        
        // Fall State
        fall.AddTransition(PlayerStatesEnum.Land,land);
        
        // Land State
        land.AddTransition(PlayerStatesEnum.Idle,idle);
      
        // Attack State
        attack.AddTransition(PlayerStatesEnum.Idle,idle);
        attack.AddTransition(PlayerStatesEnum.Run,run);
        
        
        

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
    #endregion
    private void Update()
    {
        if (_playerModel != null)
        {
            _fsm.OnUpdate();
            
        }
    }
    
}
