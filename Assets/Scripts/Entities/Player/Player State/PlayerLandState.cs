using UnityEngine;

public class PlayerLandState<T> : State<T>
{
    private T _inputIdle;
    private Player _player;

    public PlayerLandState(T inputIdle, Player player)
    {
        _inputIdle = inputIdle;
        _player = player;
    }

    public override void Execute()
    {
        _player.Land();
        _fsm.Transition(_inputIdle);
    }
}
