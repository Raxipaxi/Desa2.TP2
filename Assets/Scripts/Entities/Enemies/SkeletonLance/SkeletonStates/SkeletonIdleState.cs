

using System;
using UnityEngine;

public class SkeletonIdleState<T> : State<T>
{
    private Action _onIdle;
    private iNode _root;
    private float _counter;
    private float _currCounter;
    private Action<bool> _idleCD;
    private Func<bool> _canAttack;

    public SkeletonIdleState(Func<bool> canAttack, Action onIdle, float counter, Action<bool> idleCd, iNode root)
    {
        _root = root;
        _onIdle = onIdle;
        _counter = counter;
        _idleCD = idleCd;
        _canAttack = canAttack;
  
        ResetCount();
    }

    public override void Execute()
    {
        _onIdle?.Invoke();

        _currCounter -= Time.deltaTime;
        
        if (!(_currCounter<=0)&&!_canAttack()) return;
        ResetCount();
        _idleCD?.Invoke(false);
        _root.Execute();
    }
    
    void ResetCount()
    {
        _currCounter = _counter;
    }
    
}
