using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private Player player;

    [SerializeField] private List<Enemy> enemyPrefabs;

    [SerializeField] private float spawnRadius;
    [SerializeField] private float spawnPeriod;

    private float timer = 0f;

    private void Update()
    {
        if (timer <= 0f)
        {
            timer += spawnPeriod;

            float radians = Random.Range(0f, 2 * Mathf.PI);
            Vector3 spawnPos = player.position + new Vector3(Mathf.Cos(radians), Mathf.Sin(radians)) * spawnRadius;
            var enemy = Instantiate(enemyPrefabs[0], spawnPos, Quaternion.identity);
            enemy.Init(player);
        }

        timer -= Time.deltaTime;
    }
}
