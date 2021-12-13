
using System;
using UnityEngine;

public class PlayerFallState<T> : State<T>
{
    private readonly T _landInput;
    private readonly T _jumpInput;
    private readonly iInput _playerInput;
    private Func<bool> _checkground;
    private Action _onFall; 
    private Action<Vector2> _onMove;
    private bool _canDoubleJump;
    
    public PlayerFallState(T landInput,T jumpInput,iInput playerInput,Func<bool> checkGround,Action onFall, Action<Vector2> onMove)
    {
        _landInput = landInput;
        _jumpInput = jumpInput;
        _playerInput = playerInput;
        _checkground = checkGround;
        _onMove = onMove;
        _onFall = onFall;
        _canDoubleJump = true;
    }

    public override void Execute()
    {
        _onFall?.Invoke();
        _playerInput.UpdateInputs();
        var h = _playerInput.GetH;
        
        Vector2 dir = new Vector2(h,0f);
        _onMove?.Invoke(dir);
        
        if (_checkground())
        {
            SetDoubleJump(true);
            _fsm.Transition(_landInput);
        }
        else if (_canDoubleJump)
        {
            if (_playerInput.IsJumping())
            {
                SetDoubleJump(false);
                _fsm.Transition(_jumpInput);    
            }
        }
    }

    private void SetDoubleJump(bool status)
    {
        _canDoubleJump = status;
    }
}
