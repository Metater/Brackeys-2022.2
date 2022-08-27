using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceRock : Rock
{
    [SerializeField] private float damage;
    [SerializeField] private float slowness;
    [SerializeField] private float slownessTime;
    [SerializeField] private float ignite;
    [SerializeField] private float igniteTime;

    public override void OnEnemyEnter(Enemy enemy, Collider2D collider)
    {
        enemy.Slowness(slownessTime, slowness);
        enemy.Ignite(igniteTime, ignite);
        enemy.Damage(damage);
    }
}
