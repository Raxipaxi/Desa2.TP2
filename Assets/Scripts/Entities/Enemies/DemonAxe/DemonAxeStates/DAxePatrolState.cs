
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class DAxePatrolState<T> : State<T>
{
    private iNode _root;
    private Action<Vector2> _onWalk;
    private Action<bool> _idleCD;
    private Func<bool> _isSeen;
    private Transform[] _waypoints;
    private Transform _currWaypoint;
    private Transform _transform;
    
    private HashSet<Transform> _visited;
    
    private float _minDistance;

    public DAxePatrolState(Func<bool> isSeen,Transform[] waypoints, Transform transform, Action<Vector2> onWalk,Action<bool> idleCD,float minDistance, iNode root)
    {
        _root = root;
        _waypoints = waypoints;
        _onWalk = onWalk;
        _isSeen = isSeen;
        _minDistance = minDistance;
        _transform = transform;
        _idleCD = idleCD;
        _visited = new HashSet<Transform>();
    }

    public override void Awake()
    {
        if (_currWaypoint==null)
        {
            NextWaypoint();
        }
    }


    public override void Execute()
    {
        
        _onWalk?.Invoke(_currWaypoint.position);
        var canSee = _isSeen.Invoke();

        if (canSee)
        {
            _root.Execute();
            return;
        }

        var distNext = Vector2.Distance(_currWaypoint.position, _transform.position);
        
        if (distNext > _minDistance) return;
      
        NextWaypoint();
        _idleCD?.Invoke(true);
        _root.Execute();
        
    }


    private void NextWaypoint()
    {
        
        for (int i = 0; i < _waypoints.Length; i++)
        { 
           var temp = _waypoints[i];
           if (_currWaypoint!=temp&&!_visited.Contains(temp))
            {
                _currWaypoint = temp;
                break;
            }
        }
        
        _visited.Add(_currWaypoint);

        if (_visited.Count >=  _waypoints.Length)
        {
            _visited.Clear();
        }
    }
}
