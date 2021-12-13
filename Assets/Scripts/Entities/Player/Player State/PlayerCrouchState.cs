using UnityEngine;
using System;

public class PlayerCrouchState <T> : State<T>
{
    private  Action<Vector2> _onMove;
    private  Action<bool> _onCrouch;
    private T _attackInput;
    private T _fallInput;
    private iInput _playerInput;
    private T _idleInput;
    private Func<bool> _checkGround;

    public PlayerCrouchState(Func<bool> checkGround, T attackInput,T idleInput, T fallInput,iInput playerInput, Action<Vector2> onMove, Action<bool> onCrouch)
    {
        _onMove = onMove;
        _onCrouch = onCrouch;
        _attackInput = attackInput;
        _fallInput = fallInput;
        _idleInput = idleInput;
        _playerInput = playerInput;
        _checkGround = checkGround;
    }

    public override void Awake()
    {
        _onCrouch.Invoke(true);
    }

    public override void Execute()
    {
        if (!_playerInput.IsCrouched())
        {
            _fsm.Transition(_idleInput);
            return;
        }
        _playerInput.UpdateInputs();
        var h = _playerInput.GetH;
        if (_playerInput.IsRunning())
        {
            if (_checkGround())
            {
                Vector2 dir = new Vector2(h,0);
                _onMove?.Invoke(dir);
            }
            else 
            {
                _fsm.Transition(_fallInput);
                return;
            }
        }
        else
        {
            _onCrouch.Invoke(true);
        }
        if (_playerInput.IsAttacking())
        {
            _fsm.Transition(_attackInput);
        }
        
    }

    public override void Sleep()
    {
        _onCrouch.Invoke(false);
    }
}
