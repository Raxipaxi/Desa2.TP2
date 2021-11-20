using System;
using UnityEngine;


public class DAxeView : MonoBehaviour
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

    public void Subscribe(DAxeController controller)
    {
        controller.OnIdle += IdleAnimation;
    }
    void IdleAnimation()
    {
        _animator.Play("Idle");
    }

    void MoveAnimation()
    {
        //_animator.Play("");
    }

    void AttackAnimation()
    {
        //_animator.Play("");
    }

    void DieAnimation()
    {
        
    }

    void HitAnimation()
    {
        
    }
    
    
    
}
