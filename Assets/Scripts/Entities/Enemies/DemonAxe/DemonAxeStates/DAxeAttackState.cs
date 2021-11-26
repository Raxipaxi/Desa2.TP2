using System;
using UnityEngine;

public class DAxeAttackState<T> : State<T>
{
    private float _attackCd;
    private float _counter;
    private Action<int> _onAttack;
    private int _dmg;
    private iNode _root;
    private Action<bool> _idleCD;

    public DAxeAttackState(float attackCd, Action<int> onAttack,Action<bool> idleCD, iNode root)
    {
        _attackCd = attackCd;
        _onAttack = onAttack;
        _idleCD = idleCD;
        _root = root;
        ResetCd();
    }

    public override void Execute()
    {
        if  (Time.time > _counter )
        {
            // Debug.Log("Counter : " + _counter);
            // Debug.Log("Time : " + Time.time);
            ResetCd();
            _onAttack?.Invoke(_dmg);
            _idleCD?.Invoke(true);
        }
        _root.Execute();
    }

    private void ResetCd()
    {
        _counter = Time.time + _attackCd;
    }
}
