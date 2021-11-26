using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalReached : MonoBehaviour
{
    public event Action OnWin;

    [SerializeField] private LayerMask _plaLayerMask;
    // Start is called before the first frame update

    private void OnTriggerStay2D(Collider2D other)
    {
        var coll = other.GetComponent<PlayerModel>();

        if (coll!=null) OnWin?.Invoke();
        
    }
}
