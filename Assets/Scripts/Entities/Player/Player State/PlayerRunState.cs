using  UnityEngine;

public class PlayerRunState<T> : State<T>
{
    private T _inputIdle;
    private T _inputJump;
    private T _inputFall;
    private T _inputAttack;
    private iInput _playerInput;
    private Player _player;


    public PlayerRunState(Player player, T inputIdle, T inputJump,T inputFall ,T inputAttack,iInput playerInput)
    {
        _player = player;
        _inputIdle = inputIdle;
        _inputJump = inputJump;
        _inputFall = inputFall;
        _playerInput = playerInput;
        _inputAttack = inputAttack;
    }
    public override void Execute()
    {
        _playerInput.UpdateInputs();
        var h = _playerInput.GetH;
        
        if (_playerInput.IsRunning())
        {
            if (_player.CheckIfGrounded())
            {
                Vector2 dir = new Vector2(h,0);
                _player.Move(dir, _player.GetSpeed());
            }
           else 
           {
               _fsm.Transition(_inputFall);
           }
        }

        if (_playerInput.IsJumping()&&!_player.IsJumping())
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
