using UnityEngine;

public class PlayerIdleState<T> : State<T>
{
    private T _runInput;
    private T _attackInput;
    private T _jumpInput;
    private T _fallInput;
    private iInput _playerInput;
    private PlayerModel _playerModel;

    public PlayerIdleState(PlayerModel playerModel, T runInput,T fallInput,T attackInput,T jumpInput,iInput playerInput)
    {
        _playerModel = playerModel;
        _runInput = runInput;
        _attackInput = attackInput;
        _fallInput = fallInput;
        _jumpInput = jumpInput; 
        _playerInput = playerInput;

    }

    public override void Execute()
    {
        if (_playerModel.CheckIfGrounded())
        {
            _playerModel.Idle();
        }
    
        _playerInput.UpdateInputs(); 

        if (!_playerModel.IsJumping()&&_playerModel.CheckIfGrounded())
        {
            
            if (_playerInput.IsRunning())
            {
                _fsm.Transition(_runInput);
                
            }else if (_playerInput.IsAttacking())
            {
                _fsm.Transition(_attackInput);
                
            }else if (_playerInput.IsJumping())
            {
                _fsm.Transition(_jumpInput);
            }
        }
        else
        {
            _fsm.Transition(_fallInput);
        }
    }
}
