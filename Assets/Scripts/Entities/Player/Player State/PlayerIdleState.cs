using UnityEngine;

public class PlayerIdleState<T> : State<T>
{
    private T _runInput;
    private T _attackInput;
    private T _jumpInput;
    private T _fallInput;
    private iInput _playerInput;
    private Player _player;

    public PlayerIdleState(Player player, T runInput,T fallInput,T attackInput,T jumpInput,iInput playerInput)
    {
        _player = player;
        _runInput = runInput;
        _attackInput = attackInput;
        _fallInput = fallInput;
        _jumpInput = jumpInput; 
        _playerInput = playerInput;

    }

    public override void Execute()
    {
        if (_player.CheckIfGrounded())
        {
            _player.Idle();
        }
    
        _playerInput.UpdateInputs(); 

        if (!_player.IsJumping()&&_player.CheckIfGrounded())
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
