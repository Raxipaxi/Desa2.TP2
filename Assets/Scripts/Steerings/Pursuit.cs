using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursuit : ISteering
{
    Transform _self;
    private float SelfX => _self.position.x;
    private float SelfY => _self.position.y;
    private Vector2 _self2D;
    Transform _target;
    IVel _vel;
    float _timePrediction;
    float _offset;
    public Pursuit(Transform self, Transform target, IVel vel, float timePrediction, float offset = 2)
    {
        _self = self;
        _target = target;
        _vel = vel;
        _timePrediction = timePrediction;
        _offset = offset;
    }
    public Vector2 GetDir()
    {
        float multiplierDirection = (_vel.Vel * _timePrediction);
        float distance = Vector2.Distance(_target.position, _self.position);

        if (multiplierDirection >= distance)
        {
            multiplierDirection = distance / _offset;
        }
        Vector2 finitPos = _target.position + _target.forward * multiplierDirection;
        //B-A
        _self2D = new Vector2(SelfX, SelfY);
        Vector2 dir = finitPos - _self2D;
        dir = dir.normalized;
        return dir;
    }
}
