using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISteering2D 
{
    Vector2 GetDir();
    Transform SetTarget { set; }
    
}
