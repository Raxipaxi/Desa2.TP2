using System;
using UnityEngine;
using Unity;

public class LineOfSight : MonoBehaviour
{
    // Line of Sight Parameters
    public float range = 10;
    public float angle = 90;

    public bool IsInSight(Transform target, Transform origin, LayerMask maskObs) // ver de pasarlo a una clase
    {
        Vector2 diff = target.position - origin.position;
        float distance = diff.magnitude;
        if (distance > range) return false;

        Vector2 front = origin.forward;

        if (!InAngle(diff, front)) return false;

        if (!IsInView(origin.position, diff.normalized, distance, maskObs)) return false;

        return true;
    }

    bool InAngle(Vector2 from, Vector2 to)
    {
        float angleToTarget = Vector2.Angle(from, to);
        return angleToTarget < angle / 2;
    }

    bool IsInView(Vector2 originPos, Vector2 dirToTarget, float distance, LayerMask maskObstacle)
    {
        return !Physics.Raycast(originPos, dirToTarget, distance, maskObstacle);
    }
}

