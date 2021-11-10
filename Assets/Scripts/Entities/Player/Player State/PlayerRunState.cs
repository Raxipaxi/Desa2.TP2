using  UnityEngine;

public class PlayerRunState<T> : State<T>
{
    private T _inputIdle;
    private T _inputJump;
    private T _inputFall;
    private T _inputAttack;
    private iInput _playerInput;
    private PlayerModel _playerModel;


    public PlayerRunState(PlayerModel playerModel, T inputIdle, T inputJump,T inputFall ,T inputAttack,iInput playerInput)
    {
        _playerModel = playerModel;
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
            if (_playerModel.CheckIfGrounded())
            {
                Vector2 dir = new Vector2(h,0);
                _playerModel.Move(dir);
            }
           else 
           {
               _fsm.Transition(_inputFall);
           }
        }

        if (_playerInput.IsJumping()&&!_playerModel.IsJumping())
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
