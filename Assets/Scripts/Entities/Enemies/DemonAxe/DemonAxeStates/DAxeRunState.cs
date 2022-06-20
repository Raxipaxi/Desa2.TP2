using System;
using UnityEditorInternal;
using UnityEngine;


public class DAxeRunState<T> : State<T>
{
    private Transform _target;
    private Transform _ourPos;
    private float _attackDist;
    private Func<bool> _isSeen;
    private Func<bool> _canAttack;
    private Action<Vector2> _onRun;
    private iNode _root;

    public DAxeRunState(Action<Vector2> onRun,Transform target, Func<bool> canAttack,Func<bool> isSeen,iNode root) 
    {
        _onRun = onRun;
        _target = target;
        _canAttack = canAttack;
        _isSeen = isSeen;
        _root = root;
    }

    public override void Awake()
    {
        
    }

    public override void Execute()
    {
        var onSight = _isSeen();
        
        _onRun?.Invoke(_target.position);

        if (!onSight) { _root.Execute(); return; }
        
        if (_canAttack()) _root.Execute();
    }

    public override void Sleep()
    {
        base.Sleep();
    }
}
