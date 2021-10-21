
using UnityEngine;

public class PlayerFallState<T> : State<T>
{
    private T _landInput;
    private Player _player;

    public PlayerFallState(T landInput, Player player)
    {
        _landInput = landInput;
        _player = player;
    }

    public override void Execute()
    {
        _player.Fall();
        if (_player.CheckIfGrounded())
        {
                _fsm.Transition(_landInput);
        }
    }
}
