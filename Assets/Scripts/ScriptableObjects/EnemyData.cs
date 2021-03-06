using UnityEngine;


[CreateAssetMenu(fileName = "NewEnemy", menuName = "Entities / GroundEnemy", order = 0)]
public class EnemyData : ScriptableObject
{
    [Header ("Movement")]
    public float walkSpeed;
    public float runSpeed;
    public float groundDistance = 0.2f;
    
    [Header("Damage")] 
    public int damage;
    public float attackCooldown;
    public float attackDist;

    [Header("Life")] public int maxLife;

}
