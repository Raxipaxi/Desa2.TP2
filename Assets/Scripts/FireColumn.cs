using System.Threading.Tasks;
using UnityEngine;

public class FireColumn : MonoBehaviour
{

    [SerializeField] int _fireDmg;
    [SerializeField] private float fireTimer;
    private Animator _animator;
    

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    void OnTriggerEnter2D(Collider2D collision)
     {
         var player = collision.GetComponent<IDamageable>();
         player?.TakeDamage(_fireDmg);

     }
    
}
