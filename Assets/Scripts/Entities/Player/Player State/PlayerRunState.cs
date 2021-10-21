using  UnityEngine;

public class PlayerRunState<T> : State<T>
{
    private T _inputIdle;
    private T _inputJump;
    private T _inputFall;
    private iInput _playerInput;
    private Player _player;


    public PlayerRunState(Player player, T inputIdle, T inpuptJump,T inputFall ,iInput playerInput)
    {
        _player = player;
        _inputIdle = inputIdle;
        _inputJump = inpuptJump;
        _inputFall = inputFall;
        _playerInput = playerInput; 
    }
    public override void Execute()
    {
        _playerInput.UpdateInputs();
        var h = _playerInput.GetH;
        
        if (_playerInput.IsRunning())
        {
            if (!_player.IsFalling())
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
        
        if(!_playerInput.IsRunning())
        {
            _fsm.Transition(_inputIdle); 
        }
        
    }

}
