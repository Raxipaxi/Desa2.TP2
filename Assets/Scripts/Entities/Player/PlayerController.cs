using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private FSM<PlayerStatesEnum> _fsm;
    private PlayerModel _playerModel;
    private iInput _playerInput;
    
    private void Awake()
    {
        _playerModel = GetComponent<PlayerModel>();
        _playerInput = GetComponent<iInput>();
        
        FsmInit();
    }
    private void FsmInit()
    {
        
        //--------------- FSM Creation -------------------//                
        var idle = new PlayerIdleState<PlayerStatesEnum>(_playerModel, PlayerStatesEnum.Run,PlayerStatesEnum.Fall,PlayerStatesEnum.Attack,PlayerStatesEnum.Jump,_playerInput );
        var run = new PlayerRunState<PlayerStatesEnum>(_playerModel, PlayerStatesEnum.Idle, PlayerStatesEnum.Jump,PlayerStatesEnum.Fall,PlayerStatesEnum.Attack,_playerInput);
        var jump = new PlayerJumpState<PlayerStatesEnum>(PlayerStatesEnum.Fall, PlayerStatesEnum.Idle,_playerModel);
        var fall = new PlayerFallState<PlayerStatesEnum>(PlayerStatesEnum.Land,_playerInput, _playerModel);
        var land = new PlayerLandState<PlayerStatesEnum>(PlayerStatesEnum.Idle, _playerModel);
        var attack = new PlayerAttackState<PlayerStatesEnum>(PlayerStatesEnum.Idle,PlayerStatesEnum.Run,_playerModel,_playerInput);
        
        
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
        if (_playerModel != null)
        {
            _fsm.OnUpdate();
            
        }
    }
    
}
