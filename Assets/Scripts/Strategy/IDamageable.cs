using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    int CurrentLife { get; }
    int MaxLife { get; }
    void TakeDamage(int x);
    void Die();
}
