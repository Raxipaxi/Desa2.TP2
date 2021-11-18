using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evasion2D : ISteering2D
{
    private Transform _self;
    private Transform _target;
    private IVel _vel;
    private float _timePrediction;
    public Evasion2D(Transform self, Transform target, IVel vel, float timePrediction)
    {
        _self = self;
        _target = target;
        _vel = vel;
        _timePrediction = timePrediction;
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
        float directionMultiplier = (_vel.Vel * _timePrediction);
        float distance = Vector2.Distance(_target.position, _self.position);

        if (directionMultiplier >= distance)
        {
            directionMultiplier = distance / 2;
        }
        var finitPos = _target.position + _target.forward * directionMultiplier;
        Vector2 dir = (_self.position - finitPos).normalized;
        return dir;
    }
}
