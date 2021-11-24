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

    // private async void FixedUpdate()
    // {
    //     await LetItIdleFire(fireTimer);
    //     await LetItBurn(1.167f+fireTimer);
    //     
    // }
    //
    // private async Task LetItIdleFire(float secs)
    // {
    //     var loop = secs + Time.time;
    //     Debug.Log("Loop " + loop);
    //     while (Time.time>loop)
    //     {
    //          _animator.Play("IdleFire");
    //         await Task.Yield();
    //     }
    //     
    // }
    // private async Task LetItBurn(float sec)
    // {
    //     var loop = sec + Time.time;
    //     Debug.Log("Loop2 " + loop);
    //     while (Time.time>loop)
    //     {
    //         _animator.Play("Rise");
    //         await Task.Yield();
    //     }
    // }
    //
    void OnTriggerEnter2D(Collider2D collision)
     {
         var player = collision.GetComponent<IDamageable>();
         player?.TakeDamage(_fireDmg);

     }
}
