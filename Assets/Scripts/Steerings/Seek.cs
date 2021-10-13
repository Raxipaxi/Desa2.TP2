using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : ISteering
{
    Transform _self;
    Transform _target;
    public Seek(Transform self, Transform target)
    {
        _self = self;
        _target = target;
    }
    public Vector2 GetDir()
    {
        //A: Self
        //B: Target
        //B-A: Target - Self

        Vector2 dir = _target.position - _self.position;
        dir = dir.normalized;
        return dir;
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
