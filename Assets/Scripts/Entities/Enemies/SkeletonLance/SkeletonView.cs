using System;
using UnityEngine;


public class SkeletonView : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        BakeReferences();
    }

    void BakeReferences()
    {
        _animator = GetComponent<Animator>();
    }

    public void Subscribe(SkeletonController controller)
    {
        controller.OnIdle += IdleAnimation;
        controller.OnWalk += MoveAnimation;
        controller.OnAttack += AttackAnimation;
        controller.OnDie += DieAnimation;
        controller.OnHit += HitAnimation;
    }
    public void IdleAnimation()
    {
        _animator.Play("SkeletonIdle");
    }

    public void MoveAnimation(Vector2 dir)
    {
        _animator.Play("SkeletonWalk");
    }

    public void AttackAnimation(int blah)
    {
        _animator.Play("SkeletonAttack");
    }

    public void DieAnimation()
    {
        _animator.Play("SkeletonDead");
    }

    public void HitAnimation()
    {
        _animator.Play("SkeletonHit");
    }
    
    
    
}