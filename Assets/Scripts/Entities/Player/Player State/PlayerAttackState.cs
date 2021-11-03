using UnityEngine;


public class PlayerAttackState<T> : State<T>
{
    private PlayerModel _playerModel;
    private T _inputIdle;
    private T _inputMove;
    private iInput _playerInput;

    public PlayerAttackState(T inputIdle, T inputMove, PlayerModel playerModel, iInput playerInput)
    {
        _playerModel = playerModel;
        _inputIdle = inputIdle;
        _inputMove = inputMove;
        _playerInput = playerInput;
    }

    public override void Execute()
    {
        _playerModel.Attack(5);
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
