using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalReached : MonoBehaviour
{
    public event Action OnWin;

    // Start is called before the first frame update

    private void OnTriggerStay2D(Collider2D other)
    {
        var coll = other.GetComponent<PlayerModel>();
        Debug.Log("Gane");
        if (coll!=null) OnWin?.Invoke();
        
    }
}
