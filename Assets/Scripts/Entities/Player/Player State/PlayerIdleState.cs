using System;
using UnityEngine;

public class PlayerIdleState<T> : State<T>
{
    private T _runInput;
    private T _attackInput;
    private T _jumpInput;
    private T _fallInput;
    private T _crouchInput;
    private iInput _playerInput;
    private Func<bool> _checkGround;
    private Func<bool> _isJumping;
    private Action _onIdle;
    

    public PlayerIdleState(Func<bool> checkground,Func<bool> isJumping,Action onIdle, T runInput,T fallInput,T attackInput,T crouchInput, T jumpInput,iInput playerInput)
    {
        _checkGround = checkground;
        _isJumping = isJumping;
        _runInput = runInput;
        _attackInput = attackInput;
        _fallInput = fallInput;
        _crouchInput = crouchInput;
        _jumpInput = jumpInput; 
        _playerInput = playerInput;
        _onIdle = onIdle;

    }

    public override void Execute()
    {
        if (_playerInput.IsCrouched())
        {
            _fsm.Transition(_crouchInput);
            return;
        }
        
        if (_checkGround())
        {
            _onIdle?.Invoke();
        }
        
        _playerInput.UpdateInputs(); 

        if (!_isJumping()&&_checkGround())
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
