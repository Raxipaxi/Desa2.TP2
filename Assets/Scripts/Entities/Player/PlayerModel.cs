using System;
using UnityEngine;

public class PlayerModel : Actor
{
    #region Position/Physics

    private Transform _transform;
    private Rigidbody2D _rb;
    private float VelY => _rb.velocity.y;
    private PlayerView _view;
    private PlayerController _controller;
    public PlayerData _playerData;
    
    
    private bool isFacingRight;//Checks where is facing
    private bool isJumping;//Checks if it already jumping
    
    #endregion
    

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>();
        _view = GetComponent<PlayerView>();
        _controller = GetComponent<PlayerController>();
        isFacingRight = true;
        isJumping = false;
        SubscribeEvents();
    }

    void SubscribeEvents()
    {
        _controller.OnAttack += Attack;
        _controller.OnMove += Move;
    }

    private void Attack()
    {
        throw new NotImplementedException();
    }

    public override void Idle()
    {
        _rb.velocity=new Vector2(0f,VelY);
        isJumping = false;
        _view.IdleAnimation();
    }

    public override void Move(Vector2 dir)
    {
        bool isOnGround = !IsJumping() && CheckIfGrounded();
        var finalSpeed = _playerData.walkSpeed;
        
        if (!isOnGround) finalSpeed -= _playerData.speedFallPenalty;
        
        
        var currDir = new Vector2(dir.x * finalSpeed,VelY);
       
        
        _rb.velocity = currDir;
        
        if (isFacingRight && dir.x<0)
        {
            Flip();
        }
        else if (!isFacingRight && dir.x>0)
        {
            Flip();
        }

        if (isOnGround)
        {
           _view.RunAnimation(currDir.normalized.x);
        }
    }

    public override void Attack(int dmgModifier)
    {
        var moving = _rb.velocity.x;
        
        _view.Attack(moving);
    }

    public float GetSpeed()
    {
        return _playerData.walkSpeed;
    }    
    public float GetFallSpeed()
    {
        return _playerData.walkSpeed-_playerData.speedFallPenalty;
    }
    
    private void Flip() 
    {
        _transform.Rotate(0f, 180f, 0f);
       // _transform.LookAt(Vector3.forward); -- Chequear 
        isFacingRight = !isFacingRight;
    }

    #region JumpFall
    public override void Jump()
    {
        isJumping = true;
        if (CheckIfGrounded())
        {
            _rb.velocity = new Vector2(_rb.velocity.x, 0f);
            var jumpForce = _playerData.jumpHeight * transform.up;
            _rb.AddForce(jumpForce, ForceMode2D.Impulse);
        }
        _view.JumpAnimation();
    }
    public void Fall()
    {
        _view.FallAnimation();
    }
    public void Land()
    {
        _view.LandAnimation();
    }
    public bool IsJumping()
    {
        return isJumping;
    }
    public bool CheckIfGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _playerData.groundDistance, _playerData.groundDetectionList);
        Debug.DrawRay(_transform.position, Vector2.down*_playerData.groundDistance, Color.cyan);
        if(hit.collider != null)
            return true;
        return false;
    }
    #endregion
}
