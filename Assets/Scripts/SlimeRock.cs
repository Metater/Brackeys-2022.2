using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeRock : Rock
{
    [SerializeField] private float damage;
    [SerializeField] private float scare;
    [SerializeField] private float scareTime;
    [SerializeField] private float slowness;
    [SerializeField] private float slownessTime;

    public override void OnEnemyEnter(Enemy enemy, Collider2D collider)
    {
        enemy.Scare(scareTime, scare);
        enemy.Slowness(slownessTime, slowness);
        enemy.Damage(this, damage);
    }
}
