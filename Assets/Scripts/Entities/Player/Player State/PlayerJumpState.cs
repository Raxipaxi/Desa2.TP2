using UnityEngine;


public class PlayerJumpState<T> : State<T>
{
    private T _fallInput;
    private T _idleInput;
    private Player _player;

    public PlayerJumpState(T fallInmput,T idleInput, Player player)
    {
        _fallInput = fallInmput;
        _idleInput = idleInput;
        _player = player;
    }

    public override void Execute()
    {
        if (!_player.IsJumping())
        {
            _player.Jump();
    
                _fsm.Transition(_fallInput);
       
        }
        else
        {
            _fsm.Transition(_idleInput);
        }
            
        
        
    }
}
