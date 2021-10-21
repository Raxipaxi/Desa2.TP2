using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour, IDamageable, iMobile
{
    public int CurrentLife => _life;
    
    [SerializeField]public int _life = 100;

    public int MaxLife => _maxLife;
    [SerializeField]public int _maxLife = 100;

    public virtual void TakeDamage(int damage)
    {
        _life -= damage;
        
        if (_life <=0)
        {
            Die();
        }
    }

    public virtual void RecoverLife(int _heal)
    {
        _life += _heal;
    }
    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public virtual void Idle()
    {
        throw new NotImplementedException();
    }

    public virtual void Attack(int dmg)
    {
    }

    public virtual void Jump()
    {
        
    }

    public virtual bool Patrol()
    {
        throw new NotImplementedException();
    }

    public virtual void Chase()
    {
        throw new NotImplementedException();
    }

    public virtual void Move(Vector2 dir, float speed)
    {
        throw new NotImplementedException();
    }
    
    
}