using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireRock : Rock
{
    [SerializeField] private float damage;
    [SerializeField] private float scare;
    [SerializeField] private float scareTime;

    [SerializeField] private int killsToLifesteal;

    public int killCount = 0;

    public override void OnEnemyEnter(Enemy enemy, Collider2D collider)
    {
        enemy.Scare(scareTime, scare);
        enemy.Damage(this, damage);
    }

    public override void OnKill(Enemy enemy)
    {
        killCount++;
        if (killCount > killsToLifesteal)
        {
            killCount = 0;
            player.GiveHeartIfNeeded();
        }
    }
}
