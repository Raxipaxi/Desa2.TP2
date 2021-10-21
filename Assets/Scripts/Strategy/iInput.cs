﻿
public interface iInput
{    
    float GetH { get; }
    float GetV { get; }
    bool IsRunning();
    bool IsAttacking();
    bool IsJumping();
    void UpdateInputs();
}
