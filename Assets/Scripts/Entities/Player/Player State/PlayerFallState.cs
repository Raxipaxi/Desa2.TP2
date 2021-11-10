
using UnityEngine;

public class PlayerFallState<T> : State<T>
{
    private readonly T _landInput;
    private readonly PlayerModel _playerModel;
    private readonly iInput _playerInput;

    public PlayerFallState(T landInput,iInput playerInput, PlayerModel playerModel)
    {
        _landInput = landInput;
        _playerModel = playerModel;
        _playerInput = playerInput;
    }

    public override void Execute()
    {
        _playerModel.Fall();
        _playerInput.UpdateInputs();
        var h = _playerInput.GetH;
        Vector2 dir = new Vector2(h,0f);
        _playerModel.Move(dir);
        
        if (_playerModel.CheckIfGrounded())
        {
                _fsm.Transition(_landInput);
        }
    }
}
