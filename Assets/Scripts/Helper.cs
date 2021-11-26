public enum PlayerStatesEnum 
{
    Idle,
    Run,
    Attack,
    Jump,
    Fall,
    Land,
    Dead,
    Hit,
    Heal,
    Damage
}

public static class PlayerAnimParameters
{
    public const string OnAir = "OnAir";
    public const string RunDir = "RunDir";
    public const string IsRunning = "IsRunning";
    public const string IsTouchingGround = "IsTouchingGround";
    public const string ChangeDir = "ChangeDir";
    public const string IsAttacking = "IsAttacking";

}