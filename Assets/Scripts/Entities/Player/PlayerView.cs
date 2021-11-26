using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private Animator _playerAnimator;
    

    void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
    }

    public void SubscribeEvents(PlayerController controller)
    {
        controller.OnHit += HitAnimation;
    }

    public void RunAnimation(float dir)
    {
        _playerAnimator.SetFloat(PlayerAnimParameters.RunDir,dir);
        _playerAnimator.SetBool(PlayerAnimParameters.IsRunning,true);
    }

    public void HitAnimation(int damage)
    {
        _playerAnimator.Play("Player_Hit");
    }
    
    public void IdleAnimation()
    {
        _playerAnimator.SetBool(PlayerAnimParameters.IsRunning,false);
    }

    public void JumpAnimation()
    {
        _playerAnimator.SetBool(PlayerAnimParameters.OnAir,true);
    }

    public void FallAnimation()
    {
        _playerAnimator.SetBool(PlayerAnimParameters.IsTouchingGround,false);
        _playerAnimator.SetBool(PlayerAnimParameters.OnAir,true);
    }

    public void LandAnimation()
    {
        _playerAnimator.SetBool(PlayerAnimParameters.OnAir,false);
        _playerAnimator.SetBool(PlayerAnimParameters.IsTouchingGround,true);
    }

    public void DeadAnimation()
    {
        _playerAnimator.Play("Player_Dead");
    }

    public void Attack(float vel)
    {
        if (vel>0)
        {
            _playerAnimator.Play("AttackIdle");
        }
        else
        {
            _playerAnimator.Play("AttackMoving");
        }
        
    }

}
