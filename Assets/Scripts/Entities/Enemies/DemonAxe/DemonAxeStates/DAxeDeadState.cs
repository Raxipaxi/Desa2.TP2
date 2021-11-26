using System;
using UnityEngine;


public class DAxeDeadState<T> : State<T>
{
    private event Action _onDieBrain;

    public DAxeDeadState(Action onDieBrain)
    {
        _onDieBrain = onDieBrain;

    }

    public override void Execute()
    {
        // Debug.Log("MORIIII");

       _onDieBrain?.Invoke();
    }
}
