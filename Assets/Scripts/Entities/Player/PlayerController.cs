using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private FSM<PlayerStatesEnum> _fsm;
    private Player _player;
    private iInput _playerInput;
    
    private void Awake()
    {
        _player = GetComponent<Player>();
        _playerInput = GetComponent<iInput>();
        
        FsmInit();
    }
    private void FsmInit()
    {
        
        //--------------- FSM Creation -------------------//                
        var idle = new PlayerIdleState<PlayerStatesEnum>(_player, PlayerStatesEnum.Run,PlayerStatesEnum.Fall,PlayerStatesEnum.Attack,PlayerStatesEnum.Jump,_playerInput );
        var run = new PlayerRunState<PlayerStatesEnum>(_player, PlayerStatesEnum.Idle, PlayerStatesEnum.Jump,PlayerStatesEnum.Fall,PlayerStatesEnum.Attack,_playerInput);
        var jump = new PlayerJumpState<PlayerStatesEnum>(PlayerStatesEnum.Fall, PlayerStatesEnum.Idle,_player);
        var fall = new PlayerFallState<PlayerStatesEnum>(PlayerStatesEnum.Land,_playerInput, _player);
        var land = new PlayerLandState<PlayerStatesEnum>(PlayerStatesEnum.Idle, _player);
        var attack = new PlayerAttackState<PlayerStatesEnum>(PlayerStatesEnum.Idle,PlayerStatesEnum.Run,_player,_playerInput);
        
        
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
    private void Update()
    {
        if (_player != null)
        {
            _fsm.OnUpdate();
            
        }
    }
    
}
