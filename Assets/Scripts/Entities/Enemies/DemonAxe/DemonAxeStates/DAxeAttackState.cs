

using System;
using UnityEditor.AssetImporters;
using UnityEngine;

public class DAxeAttackState<T> : State<T>
{
    private float _attackCd;
    private float _counter;
    private Action<int> _onAttack;
    private int _dmg;
    private Action _onChase;

    public DAxeAttackState(float attackCd, Action<int> onAttack, Action onChase)
    {
        _attackCd = attackCd;
        _onAttack = onAttack;
        onChase = onChase;
        _counter = 0f;
    }

    public override void Execute()
    {
        if  (Time.time > _counter )
        {
            _onAttack?.Invoke(_dmg);
            _counter = Time.time + _attackCd;
        }
        else
        {
            _onChase?.Invoke();
        }
      
    }
    
}
