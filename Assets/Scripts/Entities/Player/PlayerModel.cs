using System;
using UnityEngine;

public class PlayerModel : Actor
{
    #region Position/Physics
    
    private Transform _transform;
    private Rigidbody2D _rb;
    private float VelY => _rb.velocity.y;
    private PlayerView _view;

    [SerializeField] private ParticleSystem jumpDust;
    
    [SerializeField]public PlayerData data;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask enemyMask;

    public event Action OnDead;
    public event Action<int> OnHit;

    private bool isAlive;

    private bool isCrouched;
    private bool isGrounded;

    private bool isFacingRight;//Checks where is facing
    private bool isJumping;//Checks if it already jumping
    
    #endregion

    private void Awake()
    {
        BakeReferences();

        isAlive = true;
        isFacingRight = true;
        isJumping = false;
        isCrouched = false;
        isGrounded = true;
    }

    void BakeReferences()
    {
        _transform = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>();
        _view = GetComponent<PlayerView>();
    }

    public void SubscribeEvents(PlayerController controller)
    {
        controller.OnAttack += Attack;
        controller.OnMove   += Move;
        controller.OnJump   += Jump;
        controller.OnLand   += Land;
        controller.OnFall   += Fall;
        controller.OnIdle   += Idle;
        controller.OnDie   += Die;
        controller.OnCrouch += Crouch;
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

        var finalSpeed = data.walkSpeed;
        
        if (!isOnGround) finalSpeed -= data.speedFallPenalty;
        
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

    public void Crouch(bool iscrouch)
    {
        isCrouched = iscrouch;
        if (isCrouched)
        {
            _rb.velocity = Vector2.zero;
            _view.CrouchAnimation();
        }
        
    }

    public override void Attack(int dmgModifier)
    {
        var moving = _rb.velocity.x;
        
        _view.Attack(moving);
        AudioManager.instance.PlayPlayerSound(PlayerSoundClips.Attack);
        EnemyHitCheck()?.TakeDamage(data.damage*dmgModifier);
    }
   
    private void Flip()
    {

        var y = isFacingRight ? 180f : 0f;
        _transform.localRotation=Quaternion.Euler(0f, y, 0f);
      
        isFacingRight = !isFacingRight;
    }

    #region JumpFall
    public override void Jump()
    {
        float jumpMult = 1f;
        isJumping = true;
        if (!CheckIfGrounded())
        {
            jumpMult = 0.75f;
        }

        _rb.velocity = new Vector2(_rb.velocity.x, 0f);
        var jumpForce = data.jumpHeight * transform.up* jumpMult;
        _rb.AddForce(jumpForce, ForceMode2D.Impulse);
        CreateDust();

        _view.JumpAnimation();
    }

    public override void Die()
    {
        AudioManager.instance.PlayPlayerSound(PlayerSoundClips.Grunt);
        isAlive = false;
        _view.DeadAnimation();
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
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, data.groundDistance, data.groundDetectionList);
        Debug.DrawRay(_transform.position, Vector2.down*data.groundDistance, Color.cyan);
        if(hit.collider != null)
            return true;

        return false;
        //return isGrounded;
    }
    #endregion

    #region Attack

    private IDamageable EnemyHitCheck()
    {
        var hit = Physics2D.OverlapCircle(attackPoint.position, attackRadius,enemyMask); //  nonallocate masmejor
        
        if(hit==null) return null;
        return hit.GetComponent<IDamageable>();
    }

    public override void TakeDamage(int damage)
    {
        OnHit?.Invoke(damage);
    }

    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.color= Color.red;
        Gizmos.DrawWireSphere(attackPoint.position,attackRadius);
    }

    public void RealDead()
    {
        OnDead?.Invoke();
    }

    public bool GetIsAlive()
    {
        return isAlive;
    }

    void CreateDust()
    {
        jumpDust.Play();
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     Debug.Log("Pum");
    //     var floor = other.GetComponent<GameObject>().layer;
    //     if (data.groundDetectionList == floor)
    //     {
    //         isGrounded = true;
    //     }
    // }
    //
    // private void OnTriggerStay2D(Collider2D other)
    // {
    //     Debug.Log("Kachau");
    //     var floor = other.GetComponent<LayerMask>().value;
    //     if (data.groundDetectionList == floor)
    //     {
    //         isGrounded = true;
    //     }
    // }
    //
    // private void OnCollisionExit2D(Collision2D other)
    // {
    //     Debug.Log("out");
    //         isGrounded = false;
    // }
}
//TODO arreglar que el hit tambien lo informe el controller