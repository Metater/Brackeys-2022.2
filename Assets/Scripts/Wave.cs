using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public List<WaveEnemy> enemies;
}

[System.Serializable]
public class WaveEnemy
{
    public int enemyType;
    public Vector4 enemyCount;

    public int GetEnemyCountForWave(float wave, float variation)
    {
        float count = Utils.Get(wave, enemyCount);
        count *= variation;
        return Mathf.RoundToInt(count);
    }
}