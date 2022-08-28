using UnityEngine;

public class BasicRock : Rock
{
    [SerializeField] private float damage;

    public override void OnEnemyEnter(Enemy enemy, Collider2D collider)
    {
        enemy.Damage(this, damage);
    }
}