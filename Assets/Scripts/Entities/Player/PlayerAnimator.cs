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

    public void MoveAnimation(float speed)
    {
        _playerAnimator.SetFloat(PlayerAnimParameters.Vel,speed);
    }
    
    public void IdleAnimation()
    {
        _playerAnimator.SetFloat(PlayerAnimParameters.Vel,0f);
    }
    

}
