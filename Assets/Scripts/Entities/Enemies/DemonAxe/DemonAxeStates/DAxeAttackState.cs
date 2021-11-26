

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
        _onChase = onChase;
        ResetCD();
    }

    public override void Execute()
    {
        if  (Time.time > _counter )
        {
            _onAttack?.Invoke(_dmg);
            ResetCD();
        }
        else
        {
            _onChase?.Invoke();
        }
      
    }

    private void ResetCD()
    {
        _counter = Time.time + _attackCd;
    }
}
