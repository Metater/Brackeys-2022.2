using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRock : Rock
{
    [SerializeField] private float damage;
    [SerializeField] private float scare;
    [SerializeField] private float scareTime;
    [SerializeField] private float ignite;
    [SerializeField] private float igniteTime;

    public override void OnEnemyEnter(Enemy enemy, Collider2D collider)
    {
        enemy.Scare(scareTime, scare);
        enemy.Ignite(igniteTime, ignite);
        enemy.Damage(damage);
    }
}
