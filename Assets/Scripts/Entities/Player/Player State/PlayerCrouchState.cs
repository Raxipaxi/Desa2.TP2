using UnityEngine;
using System;

public class PlayerCrouchState <T> : State<T>
{
    private  Action<Vector2> _onMove;
    private  Action _onCrouch;
    private T _runInput;
    private T _attackInput;
    private T _fallInput;
    private iInput _playerInput;
    
    public PlayerCrouchState(T runInput, T attackInput, T fallInput,iInput playerInput, Action<Vector2> onMove, Action onCrouch)
    {
        _onMove = onMove;
        _onCrouch = onCrouch;
        _runInput = runInput;
        _attackInput = attackInput;
        _fallInput = fallInput;
        _playerInput = playerInput;
    }

    public override void Awake()
    {
        
    }

    public override void Execute()
    {
        base.Execute();
    }

    public override void Sleep()
    {
        base.Sleep();
    }
}
