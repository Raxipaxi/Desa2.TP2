using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private Animator _playerAnimator;
    private bool _isCrouched;
    

    void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
        _isCrouched = false;
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
    public void CrouchAnimation()
    {
        _isCrouched = !_isCrouched;
        _playerAnimator.SetBool(PlayerAnimParameters.IsCrouched,_isCrouched);
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
        if (!_isCrouched)
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

}
