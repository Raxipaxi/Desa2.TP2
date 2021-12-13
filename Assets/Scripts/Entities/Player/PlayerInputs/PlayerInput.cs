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
    private bool _crouch;
    #endregion

    //private bool isCrouched;

    private void Awake()
    {
        _joyInput = new PlayerInputJoy();
       // _joyInput.GamePlay.Jump.performed += ctx => IsJumping();
        _joyInput.GamePlay.Move.started += ctx=>  currX = ctx.ReadValue<float>();
        _joyInput.GamePlay.Move.canceled += ctx => currX = 0f;
        _joyInput.GamePlay.Crouch.started += ctx => Crouched(true);
        _joyInput.GamePlay.Crouch.canceled += ctx => Crouched(false);
    }

    public bool IsRunning()
    {
        return (GetH != 0); 
    }

    private void Crouched(bool crouch)
    {
        Debug.Log(crouch);
        _crouch = crouch;
    }
    public bool IsCrouched()
    {
        return _crouch;
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
    }

    private void OnEnable()
    {
        _joyInput.Enable();
    }
}
