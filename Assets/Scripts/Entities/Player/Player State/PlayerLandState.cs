using System;

public class PlayerLandState<T> : State<T>
{
    private T _inputIdle;
    private Action _onLand;

    public PlayerLandState(T inputIdle, Action onLand)
    {
        _inputIdle = inputIdle;
        _onLand = onLand;
    }

    public override void Execute()
    {
        _onLand?.Invoke();
        _fsm.Transition(_inputIdle);
    }
}
