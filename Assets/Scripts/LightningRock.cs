using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningRock : Rock
{
    [SerializeField] private float damage;
    [SerializeField] private float slowness;
    [SerializeField] private float slownessTime;

    public override void OnEnemyEnter(Enemy enemy, Collider2D collider)
    {
        enemy.Slowness(slownessTime, slowness);
        enemy.Damage(damage);
    }
}
