public enum PlayerStates 
{
    Idle,
    Move,
    Shoot,
    Dead,
    Heal,
    Damage
}

public static class PlayerAnimParameters
{
    public const string Vel   = "Vel";
    public const string OnAir = "OnAir";
}