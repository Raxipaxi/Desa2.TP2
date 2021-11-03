using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DAxeModel : MonoBehaviour, IEnemy,IDamageable
{
    public int CurrentLife { get; }
    public int MaxLife { get; }
    

    public void Patrol()
    {
        throw new System.NotImplementedException();
    }

    public void Chase()
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(int x)
    {
        throw new System.NotImplementedException();
    }

    public void Die()
    {
        throw new System.NotImplementedException();
    }
}
