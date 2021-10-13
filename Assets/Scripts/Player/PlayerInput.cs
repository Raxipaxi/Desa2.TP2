using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour, iInput
{

    #region Axis
    public float GetH => _xAxis;
    public float GetV => _yAxis;
    float _xAxis;
    float _yAxis;
    #endregion
    
    #region Properties

    private Rigidbody2D _rb;
    
    #endregion
    
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public bool IsMoving()
    {
        return (GetH != 0 || GetV != 0);
    }

    public bool IsAttacking()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateInputs()
    {
        _xAxis = Input.GetAxis("Horizontal");
        _yAxis = Input.GetAxis("Vertical");
    }
}
