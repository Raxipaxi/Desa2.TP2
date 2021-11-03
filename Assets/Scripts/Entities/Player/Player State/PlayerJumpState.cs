using UnityEngine;


public class PlayerJumpState<T> : State<T>
{
    private T _fallInput;
    private T _idleInput;
    private PlayerModel _playerModel;

    public PlayerJumpState(T fallInmput,T idleInput, PlayerModel playerModel)
    {
        _fallInput = fallInmput;
        _idleInput = idleInput;
        _playerModel = playerModel;
    }

    public override void Execute()
    {
        if (!_playerModel.IsJumping())
        {
            _playerModel.Jump();
    
                _fsm.Transition(_fallInput);
       
        }
        else
        {
            _fsm.Transition(_idleInput);
        }
            
        
        
    }
}
