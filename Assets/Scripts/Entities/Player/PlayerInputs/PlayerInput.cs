using System;
using Unity;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInput : MonoBehaviour, iInput
{
    private PlayerInputJoy _joyInput;
    #region Axis
    public float GetH => _xAxis;
    public float GetV => _yAxis;
    float _xAxis;
    float _yAxis;
    private float currX;
    #endregion

    private void Awake()
    {
        _joyInput = new PlayerInputJoy();
        _joyInput.GamePlay.Jump.performed += ctx => IsJumping();
        _joyInput.GamePlay.Move.started += ctx=>  currX = ctx.ReadValue<float>();
        _joyInput.GamePlay.Move.canceled += ctx => currX = 0f;
    }

    public bool IsRunning()
    {
        return (GetH != 0); 
    }

    public bool IsAttacking()
    {
        return _joyInput.GamePlay.Attack.triggered;
    }    
    
    public bool IsJumping()
    {
        return _joyInput.GamePlay.Jump.triggered;
    }

    public void UpdateInputs()
    {
        if (currX == 0f) {_xAxis = 0f;return;}

        _xAxis = currX > 0 ? 1f : -1f;
        // if (currX > 0) { _xAxis = 1; return;}
        // if (currX < 0) { _xAxis = -1;}

    }

    private void OnEnable()
    {
        _joyInput.Enable();
    }
}
