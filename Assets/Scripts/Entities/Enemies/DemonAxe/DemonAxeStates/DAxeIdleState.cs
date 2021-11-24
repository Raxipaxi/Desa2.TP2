

using System;
using UnityEngine;

public class DAxeIdleState<T> : State<T>
{
    private Func<bool> _isSeen;
    private Action _onIdle;
    private iNode _root;
    private float _counter;
    private float _currCounter;
    private Action<bool> _idleCD;

    public DAxeIdleState(Func<bool> isSeen, Action onIdle, float counter, Action<bool> idleCd, iNode root)
    {
        _root = root;
        _isSeen = isSeen;
        _onIdle = onIdle;
        _counter = counter;
        _idleCD = idleCd;
  
        ResetCount();
    }

    public override void Execute()
    {
        _onIdle?.Invoke();

        _currCounter -= Time.deltaTime;
        
        if (!(_currCounter<=0)&&!_isSeen()) return;
        ResetCount();
        _idleCD?.Invoke(false);
        _root.Execute();
    }
    
    void ResetCount()
    {
        _currCounter = _counter;
    }
    
}
