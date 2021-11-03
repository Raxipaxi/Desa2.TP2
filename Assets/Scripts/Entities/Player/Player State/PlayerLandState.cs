using UnityEngine;

public class PlayerLandState<T> : State<T>
{
    private T _inputIdle;
    private PlayerModel _playerModel;

    public PlayerLandState(T inputIdle, PlayerModel playerModel)
    {
        _inputIdle = inputIdle;
        _playerModel = playerModel;
    }

    public override void Execute()
    {
        _playerModel.Land();
        _fsm.Transition(_inputIdle);
    }
}
