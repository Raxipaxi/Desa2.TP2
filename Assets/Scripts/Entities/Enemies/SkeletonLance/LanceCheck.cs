using System;
using UnityEngine;

public class LanceCheck : MonoBehaviour
{
    [SerializeField] public EnemyData data;
    private void OnTriggerEnter2D(Collider2D other)
    {
        var player =  other.GetComponent<IDamageable>();
        player?.TakeDamage(data.damage);
    }
}
