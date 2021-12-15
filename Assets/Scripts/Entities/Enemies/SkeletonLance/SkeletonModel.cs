using System;
using UnityEngine;
public class SkeletonModel : Actor
{
    #region Properties

    [SerializeField] public EnemyData data;
    [SerializeField] private ParticleSystem bloodSplash;
    [SerializeField] private LayerMask playerMask;
    
    private Rigidbody2D _rb;
    private SkeletonView _skeletonView;
    private int _currLife;
    private Transform _transform;
    public event Action OnDie;
    public event Action OnHit;

    private float currSpeed;

    private DAxeController _controller;
    private DAxeView _view;
    private bool isFacingRight;//Checks where is facing

    #endregion

    private void Awake()
    {
        BakeReferences();
        isFacingRight = true;
        _currLife = data.maxLife;
        isFacingRight = true;
        _transform = transform;
    }

    public void BakeReferences()
    {
        _rb = GetComponent<Rigidbody2D>();
        _skeletonView = GetComponent<SkeletonView>();
    }

    public void Subscribe(SkeletonController controller)
    {
        controller.OnAttack += Attack;
        controller.OnIdle += Idle;
        controller.OnWalk += Walk;
        
    }
    public override void TakeDamage(int damage)
    {
        _currLife -= damage;
        // Debug.LogWarning("Me hicieron nana " + damage);
        OnHit?.Invoke();
        bloodSplash.Play();
        if (_currLife<=0)
        {
            Die();
        }
    }

    public override void Idle()
    {
        _rb.velocity = Vector2.zero;
    }

    #region Movement
    void Walk(Vector2 dir)
    {
        currSpeed = data.walkSpeed;
        Move(dir);
    }

    public override void Move(Vector2 dir)
    {
        dir.y = _transform.position.y;
        
        var Flipthing = _transform.position.x  - dir.x < 0 ? currSpeed * -1f : currSpeed;
        
        if (isFacingRight && Flipthing>0)
        {
            Flip();
        }
        else if (!isFacingRight && Flipthing<0)
        {
            Flip();
        }
       
        var direccion = dir - (Vector2)_transform.position;

        _rb.velocity = direccion.normalized *  currSpeed;

    }

    #endregion

    #region Attack Methods
    public override void Attack(int dmg)
    {
        
        Idle();
    }
    private void PlayerHitCheck()
    {
        //    LO VA A TENER LA LANZA
        
        //
        // if (hit!=null)
        // {
        //     var player =  hit.GetComponent<IDamageable>();
        //
        //     player?.TakeDamage(data.damage);
        // }

    }
    private void OnDrawGizmo()
    {
        Gizmos.color= Color.red;
   
    }

    #endregion

    public override void Die()
    {
        _rb.velocity = Vector2.zero;

        OnDie?.Invoke();
    }


    private void Flip()
    {
        var y = isFacingRight ? 180f : 0f;
        transform.localRotation=Quaternion.Euler(0f, y, 0f);
      
        isFacingRight = !isFacingRight;
    }
    


}