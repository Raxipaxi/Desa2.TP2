

using System;

public class DAxeIdleState<T> : State<T>
{
    private Func<bool> _isSeen;
    private Action _onIdle;
    private iNode _root;

    public DAxeIdleState(Func<bool> isSeen,Action onIdle ,iNode root)
    {
        _root = root;
        _isSeen = isSeen;
        _onIdle = onIdle;
    }

    public override void Execute()
    {
        _onIdle?.Invoke();
        if(!_isSeen()) return;
        
        _root.Execute();
        
    }

   
    
}
