using System;
using UnityEngine;

public class LifeController 
{

    private float currentLife;
    public float MaxLife { get; private set; }

    public event Action OnDead;
    public event Action<float, float> OnGetDamage; //int 0 = currentLife, int 1 = damage
    public event Action<float, float> OnGetHeal; //int 0 = currentLife, int 1 = healing
    public event Action <float> onRevive; //Life percentage
    private GameObject _owner;
    public bool IsAlive => currentLife > 0;
    public bool isFullHealth => currentLife == MaxLife;

    public float LifePercentage => currentLife / MaxLife;


    public float CurrentLife
    {
        get => currentLife;

        set
        {
            currentLife = value;

            if (!IsAlive)
            {
                OnDead?.Invoke();
            }

            if (currentLife > MaxLife)
            {
                currentLife = MaxLife;
            }
            if (currentLife < 0)
            {
                currentLife = 0;
            }
        }

    }

    public void Revive()
    {
        currentLife = MaxLife;
        onRevive?.Invoke(LifePercentage);
    }
    public LifeController(float maxlife,GameObject owner)
    {
        MaxLife = maxlife;
        CurrentLife = maxlife;
        _owner = owner;
    }

    public void GetDamage(float damage)
    {
        if (!IsAlive) return;
          CurrentLife -= damage;
          OnGetDamage?.Invoke(CurrentLife, damage);
    }


    public void GetHeal(float heal)
    {
        CurrentLife += heal;
        OnGetHeal?.Invoke(CurrentLife, heal);
    }
}
