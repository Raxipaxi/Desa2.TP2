using System;
using  UnityEngine;

public class PlayerRunState<T> : State<T>
{
    private T _inputIdle;
    private T _inputJump;
    private T _inputFall;
    private T _inputAttack;
    private iInput _playerInput;
    private Func<bool> _checkGround;
    private Func<bool> _checkJump;
    private Action<Vector2> _onMove;


    public PlayerRunState(Func<bool> checkGround,Func<bool> checkJump, T inputIdle, T inputJump,T inputFall ,T inputAttack,iInput playerInput, Action<Vector2> onMove)
    {
        _inputIdle = inputIdle;
        _inputJump = inputJump;
        _inputFall = inputFall;
        _playerInput = playerInput;
        _inputAttack = inputAttack;
        _checkGround = checkGround;
        _checkJump = checkJump;
        _onMove = onMove;
    }
    public override void Execute()
    {
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
               _fsm.Transition(_inputFall);
               return;
           }
        }

        if (_playerInput.IsJumping()&&!_checkJump())
        {
            _fsm.Transition(_inputJump);
        }

        if (_playerInput.IsAttacking())
        {
            _fsm.Transition(_inputAttack);
        }
        
        if(!_playerInput.IsRunning())
        {
            _fsm.Transition(_inputIdle); 
        }
        
    }

}
