using System;
using UnityEngine;

public class PlayerHitState<T> : State<T>
{
    private T _idleInput;
    public PlayerHitState(T idleInput)
    {
        _idleInput = idleInput;

    }

    public override void Execute()
    {
        _fsm.Transition(_idleInput);
    }
}