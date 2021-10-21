using Unity;
using UnityEngine;

public class PlayerInput : MonoBehaviour, iInput
{

    #region Axis
    public float GetH => _xAxis;
    public float GetV => _yAxis;
    float _xAxis;
    float _yAxis;
    #endregion
    
    public bool IsRunning()
    {
        return (GetH != 0); 
    }

    public bool IsAttacking()
    {
        return Input.GetKeyDown(KeyCode.Z);
    }    
    
    public bool IsJumping()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    public void UpdateInputs()
    {
        _xAxis = Input.GetAxisRaw("Horizontal");
    }
}
