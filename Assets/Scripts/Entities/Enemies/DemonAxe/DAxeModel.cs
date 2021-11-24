using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DAxeModel : Actor
{
    [SerializeField] public EnemyData data;
    private Rigidbody2D _rb;
    private DAxeView _dAxeView;
    private int _currLife;
    private int _currDmg;
    private Transform _transform;
    private int CurrLife => _currLife;
    public event Action OnDie;

    private float currSpeed;
    

    private DAxeController _controller;
    private DAxeView _view;
    private bool isFacingRight;//Checks where is facing

    private void Awake()
    {
        BakeReferences();
        isFacingRight = true;
        _currLife = data.maxLife;
        _currDmg = data.damage;
        isFacingRight = true;
        _transform = transform;
    }

    public void BakeReferences()
    {
        _rb = GetComponent<Rigidbody2D>();
        _dAxeView = GetComponent<DAxeView>();
    }

    public void Subscribe(DAxeController controller)
    {
        controller.OnAttack += Attack;
        controller.OnIdle += Idle;
        controller.OnRun += Run;
        controller.OnWalk += Walk;
        
    }
    public override void TakeDamage(int damage)
    {
        _currLife -= damage;
        if (_currLife<=0)
        {
            Die();
        }
    }

    public override void Idle()
    {
        _rb.velocity = Vector2.zero;
    }

    void Run(Vector2 dir)
    {
        _dAxeView.isRunning = true;
        currSpeed = data.runSpeed;
        Move(dir);
    }    
    void Walk(Vector2 dir)
    {
        _dAxeView.isRunning = false;
        currSpeed = data.walkSpeed;
        Move(dir);
    }

    public override void Die()
    {
        OnDie?.Invoke();
    }

    public override void Move(Vector2 dir)
    {
        dir.y = transform.position.y;
        var currDir = dir.normalized;

        var finalSpeed = _transform.position.x - dir.x   < 0 ? currSpeed * -1f : currSpeed;
        
        if (isFacingRight && finalSpeed>0)
        {
            Flip();
        }
        else if (!isFacingRight && finalSpeed<0)
        {
            Flip();
        }
        _rb.velocity = currDir * finalSpeed;

    }
    
    private void Flip()
    {
        var y = isFacingRight ? 180f : 0f;
        transform.localRotation=Quaternion.Euler(0f, y, 0f);
      
        isFacingRight = !isFacingRight;
    }

}
