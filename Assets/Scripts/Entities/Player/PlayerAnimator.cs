using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public static Animator _playerAnimator;
    

    void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
    }

    public void RunAnimation(float dir)
    {
        _playerAnimator.SetFloat(PlayerAnimParameters.RunDir,dir);
        _playerAnimator.SetBool(PlayerAnimParameters.IsRunning,true);
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
    

}
