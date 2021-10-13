// All actors that have movement use this interface

using UnityEngine;

public interface iMobile // Can move and attack
{
        void Idle();
        void Walk(Vector2 dir);
    
        void Attack(int dmg);

        bool Patrol();

        void Chase();

        void Move(Vector2 dir, float speed);
        
        void Run(Vector2 dir);
}
