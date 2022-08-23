using UnityEngine;

public class BasicRock : Rock
{
    [SerializeField] private float damagePerSecond;

    public override void OnEnemyStay(Enemy enemy)
    {
        enemy.Damage(Time.deltaTime * damagePerSecond);
    }
}