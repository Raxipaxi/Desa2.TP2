using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee2D : ISteering2D
{
    private Transform _self;
    private Transform _target;

    public Flee2D(Transform self, Transform target)
    {
        _self = self;
        _target = target;
    }
    public Transform SetSelf
    {
        set
        {
            _self = value;
        }
    }

    public Transform SetTarget
    {
        set
        {
            _target = value;
        }
    }
    public Vector2 GetDir()
    {
        //A : Target
        //B: Self

        return (_self.position - _target.position).normalized;
    }

}
