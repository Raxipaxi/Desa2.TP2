﻿using System;
using UnityEngine;

public class Player : Actor
{
    #region Position/Physics

    private Transform _transform;
    private Rigidbody2D _rb;
    private float VelY => _rb.velocity.y;
    [SerializeField] private float _speed;
    [SerializeField] private float _speedFallPen;
    [SerializeField] private float _jump;
    [SerializeField] private float groundDistance = 0.2f;
    [SerializeField] private LayerMask groundDetectionList;
    private PlayerAnimator _animator;
    
    private bool isFacingRight;//Checks where is facing
    private bool isJumping;//Checks if it already jumping
    
    #endregion

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<PlayerAnimator>();
        isFacingRight = true;
        isJumping = false;
    }

    public override void Idle()
    {
        _rb.velocity=new Vector2(0f,VelY);
        isJumping = false;
        _animator.IdleAnimation();
    }
    public override void Move(Vector2 dir,float speed)
    {
        var currDir = new Vector2(dir.x * speed,VelY);
        _rb.velocity = currDir;
        
        if (isFacingRight && dir.x<0)
        {
            Flip();
        }
        else if (!isFacingRight && dir.x>0)
        {
            Flip();
        }

        if (!IsJumping()&&CheckIfGrounded())
        {
           _animator.RunAnimation(currDir.normalized.x);
        }
    }

    public float GetSpeed()
    {
        return _speed;
    }    
    public float GetFallSpeed()
    {
        return _speed-_speedFallPen;
    }
    
    private void Flip() 
    {
        _transform.Rotate(0f, 180f, 0f);
        isFacingRight = !isFacingRight;
    }

    #region JumpFall
    public override void Jump()
    {
        isJumping = true;
        if (CheckIfGrounded())
        {
            _rb.velocity = new Vector2(_rb.velocity.x, 0f);
            var jumpForce = _jump * transform.up;
            _rb.AddForce(jumpForce, ForceMode2D.Impulse);
        }
        _animator.JumpAnimation();
    }
    public void Fall()
    {
        _animator.FallAnimation();
    }
    public void Land()
    {
        _animator.LandAnimation();
    }
    public bool IsJumping()
    {
        return isJumping;
    }
    public bool CheckIfGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundDistance, groundDetectionList);
        Debug.DrawRay(_transform.position, Vector2.down*groundDistance, Color.cyan);
        if(hit.collider != null)
            return true;
        return false;
    }
    #endregion
}