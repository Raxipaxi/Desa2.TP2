
using UnityEngine;

public class PlayerFallState<T> : State<T>
{
    private readonly T _landInput;
    private readonly Player _player;
    private readonly iInput _playerInput;

    public PlayerFallState(T landInput,iInput playerInput, Player player)
    {
        _landInput = landInput;
        _player = player;
        _playerInput = playerInput;
    }

    public override void Execute()
    {
        _player.Fall();
        _playerInput.UpdateInputs();
        var h = _playerInput.GetH;
        Vector2 dir = new Vector2(h,0f);
        _player.Move(dir, _player.GetFallSpeed());
        
        if (_player.CheckIfGrounded())
        {
                _fsm.Transition(_landInput);
        }
    }
}
