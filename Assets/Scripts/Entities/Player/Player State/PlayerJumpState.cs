using System;



public class PlayerJumpState<T> : State<T>
{
    private T _fallInput;
    private T _idleInput;
    private Func<bool> _isJumping;
    private Action _onJump;
    private bool _doubleJump;
    
    public PlayerJumpState(T fallInmput,T idleInput,Action onJump, Func<bool> isJumping)
    {
        _fallInput = fallInmput;
        _idleInput = idleInput;
        _isJumping = isJumping;
        _onJump = onJump;

    }

    public override void Execute()
    {
        // if (_isJumping()) 
        // { 
        //     
           _onJump?.Invoke();
       
           _fsm.Transition(_fallInput);
       
        // }
        // else
        // {
        //     _fsm.Transition(_idleInput);
        // }


    }
}
