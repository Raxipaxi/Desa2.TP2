using System;
using UnityEngine;


public class SkeletonDeadState<T> : State<T>
{
    private event Action _onDieBrain;

    public SkeletonDeadState(Action onDieBrain)
    {
        _onDieBrain = onDieBrain;

    }

    public override void Execute()
    {
        _onDieBrain?.Invoke();
    }
}