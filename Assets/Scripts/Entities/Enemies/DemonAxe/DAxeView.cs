using System;
using UnityEngine;


public class DAxeView : MonoBehaviour
{
    private Animator _animator;
    public bool isRunning;

    private void Awake()
    {
        BakeReferences();
    }

    void BakeReferences()
    {
        _animator = GetComponent<Animator>();
    }

    public void Subscribe(DAxeController controller)
    {
        controller.OnIdle += IdleAnimation;
        controller.OnWalk += MoveAnimation;
        controller.OnAttack += AttackAnimation;
        controller.OnDie += DieAnimation;
        controller.OnHit += HitAnimation;
    }

    // public void Subscribe(DAxeModel model)
    // {
    //     
    // }
    public void IdleAnimation()
    {
        _animator.Play("DemonIdle");
    }

    public void MoveAnimation(Vector2 dir)
    {
        _animator.speed = isRunning? 1f: 0.5f;
        _animator.Play("DemonMove");
    }

    public void AttackAnimation(int blah)
    {
        // Debug.Log("holis");
        _animator.Play("DemonAttack");
    }

    public void DieAnimation()
    {
        _animator.Play("DemonDead");
        //_animator.SetBool(DemonAnimationParameters.IsDead,true);
    }

    public void HitAnimation()
    {
        _animator.Play("DemonHit");
    }
    
    
    
}
