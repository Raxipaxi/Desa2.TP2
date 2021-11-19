using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek2D : ISteering2D
{
    private Transform _self;
    private Transform _target;

    public Seek2D(Transform self, Transform target)
    {
        _self = self;
        _target = target;
    }
    public Vector2 GetDir()
    {
        //A : Self
        //B: Target

        return (_target.position - _self.position).normalized;
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

}
