using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DAxeModel : Actor
{
    [SerializeField] private EnemyData data;
    private Rigidbody _rb;
    private DAxeView _dAxeView;
    private int _currLife;
    private int _currDmg;
    private int CurrLife => _currLife;
    
    

    private DAxeController _controller;
    private DAxeView _view;
    private bool isFacingRight;//Checks where is facing

    private void Awake()
    {
        BakeReferences();
        isFacingRight = true;
        _currLife = data.maxLife;
        _currLife = data.maxLife;
        _currDmg = data.damage;
        isFacingRight = true;
    }

    public void BakeReferences()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public override void TakeDamage(int damage)
    {
        _currLife -= damage;
        
    }

    public override void Die()
    {
        base.Die();
    }

    public override void Move(Vector2 dir)
    {
        base.Move(dir);
    }
}
