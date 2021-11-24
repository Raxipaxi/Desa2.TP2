

using System;
using UnityEditor.AssetImporters;
using UnityEngine;

public class DAxeAttackState<T> : State<T>
{
    private float _attackCd;
    private float _counter;
    private Action<int> _onAttack;
    private int _dmg;
    private iNode _root;

    public DAxeAttackState(float attackCd, Action<int> onAttack, iNode root)
    {
        _attackCd = attackCd;
        _onAttack = onAttack;
        _root = root;
        _counter = 0f;
    }

    public override void Execute()
    {
        if  (Time.time > _counter )
        {
            _onAttack?.Invoke(_dmg);
            _counter = Time.time + _attackCd;
        }
        _root.Execute();
    }
    
}
