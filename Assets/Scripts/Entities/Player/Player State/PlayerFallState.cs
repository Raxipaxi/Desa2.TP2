
using System;
using UnityEngine;

public class PlayerFallState<T> : State<T>
{
    private readonly T _landInput;
    private readonly iInput _playerInput;
    private Func<bool> _checkground;
    private Action _onFall; 
    private Action<Vector2> _onMove; 
    
    public PlayerFallState(T landInput,iInput playerInput,Func<bool> checkGround,Action onFall, Action<Vector2> onMove)
    {
        _landInput = landInput;
        _playerInput = playerInput;
        _checkground = checkGround;
        _onMove = onMove;
        _onFall = onFall;
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
            _fsm.Transition(_landInput);
        }
    }
}
