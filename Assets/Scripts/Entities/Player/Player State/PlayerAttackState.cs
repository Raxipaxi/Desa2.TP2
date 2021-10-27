using UnityEngine;


public class PlayerAttackState<T> : State<T>
{
    private Player _player;
    private T _inputIdle;
    private T _inputMove;
    private iInput _playerInput;

    public PlayerAttackState(T inputIdle, T inputMove, Player player, iInput playerInput)
    {
        _player = player;
        _inputIdle = inputIdle;
        _inputMove = inputMove;
        _playerInput = playerInput;
    }

    public override void Execute()
    {
        _player.Attack(5);
        _playerInput.UpdateInputs();
        if (_playerInput.IsRunning())
        {
            _fsm.Transition(_inputMove);
        }
        else
        {
            _fsm.Transition(_inputIdle);
        }
        
        
    }
}
