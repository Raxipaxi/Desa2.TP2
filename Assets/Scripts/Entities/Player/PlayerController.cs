using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private FSM<PlayerStates> _fsm;
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
        var idle = new PlayerIdleState<PlayerStates>(_player, PlayerStates.Run,PlayerStates.Fall,PlayerStates.Attack,PlayerStates.Jump,_playerInput );
        var run = new PlayerRunState<PlayerStates>(_player, PlayerStates.Idle, PlayerStates.Jump,PlayerStates.Fall,_playerInput);
        var jump = new PlayerJumpState<PlayerStates>(PlayerStates.Fall, PlayerStates.Idle,_player);
        var fall = new PlayerFallState<PlayerStates>(PlayerStates.Land, _player);
        var land = new PlayerLandState<PlayerStates>(PlayerStates.Idle, _player);
        
        
        // Idle State
        idle.AddTransition(PlayerStates.Run, run);
        idle.AddTransition(PlayerStates.Jump,jump);
        idle.AddTransition(PlayerStates.Fall,fall);
        
        // Run State
        run.AddTransition(PlayerStates.Idle, idle);
        run.AddTransition(PlayerStates.Fall,fall);
        run.AddTransition(PlayerStates.Jump,jump);
        
        // Jump State
       jump.AddTransition(PlayerStates.Fall,fall);
       jump.AddTransition(PlayerStates.Idle,idle);
        
        // Fall State
        fall.AddTransition(PlayerStates.Land,land);
        
        // Land State
        land.AddTransition(PlayerStates.Idle,idle);
        
        
        

        _fsm = new FSM<PlayerStates>();
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
