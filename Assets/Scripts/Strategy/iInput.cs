
public interface iInput
{    
    float GetH { get; }
    float GetV { get; }
    bool IsMoving();
    bool IsAttacking();
    void UpdateInputs();
}
