using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DAxeModel : Actor
{
    [SerializeField] public EnemyData data;
    private Rigidbody _rb;
    private DAxeView _dAxeView;
    private int _currLife;
    private int _currDmg;
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
    }

    public void BakeReferences()
    {
        _rb = GetComponent<Rigidbody>();
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

    void Run(Vector2 dir)
    {
        currSpeed = data.runSpeed;
        Move(dir);
    }    
    void Walk(Vector2 dir)
    {
        currSpeed = data.walkSpeed;
        Move(dir);
    }

    public override void Die()
    {
        OnDie?.Invoke();
    }

    public override void Move(Vector2 dir)
    {
        var currDir = dir.x * currSpeed;
        var finalVel = new Vector3(currDir, dir.y,0f);
        _rb.velocity = finalVel;
        
        if (isFacingRight && dir.x<0)
        {
            Flip();
        }
        else if (!isFacingRight && dir.x>0)
        {
            Flip();
        }
    }
    
    private void Flip()
    {
        var y = isFacingRight ? 180f : 0f;
        transform.localRotation=Quaternion.Euler(0f, y, 0f);
      
        isFacingRight = !isFacingRight;
    }

}
