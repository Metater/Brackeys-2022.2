using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WaveManager : MonoBehaviour
{
    [SerializeField] private Player player;

    [SerializeField] private Shop shop;

    [SerializeField] private Vector4 variationMin;
    [SerializeField] private Vector4 variationMax;

    [SerializeField] private List<GameObject> spawnLocations;

    [SerializeField] private List<Wave> waves;
    [Space(300f)]

    [SerializeField] TMP_Text waveText;
    [SerializeField] TMP_Text spidersText;

    [SerializeField] private List<Enemy> enemyPrefabs;

    [SerializeField] private float waveCooldown;
    [SerializeField] private float spawnPeriod;

    [SerializeField] private GameManager gameManager;

    public int wave = 0;

    private float timer = 0f;

    private State state = State.Cooldown;

    private List<Vector2Int> waveToSpawn;
    private List<int> spawned;

    private List<Enemy> enemies;

    private bool shoppedThisRound = false;

    public bool tutorialComplete = false;

    private void Awake()
    {
        enemies = new();

        timer = waveCooldown;

        waveToSpawn = new();
        spawned = new();
    }

    /*
        float radians = Random.Range(0f, 2 * Mathf.PI);
        Vector3 spawnPos = player.transform.position + new Vector3(Mathf.Cos(radians), Mathf.Sin(radians)) * spawnRadius;
        var enemy = Instantiate(enemyPrefabs[0], spawnPos, Quaternion.identity);
        enemy.Init(player);
    */

    private void Update()
    {
        if (!tutorialComplete)
        {
            return;
        }

        spidersText.text = "Spiders Remaining: " + enemies.Count;

        /*
        if (Input.GetKeyDown(KeyCode.P))
        {
            wave++;
            waveText.text = "Wave: " + wave;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            player.money++;
        }
        */
        if (timer <= 0f)
        {
            switch (state)
            {
                case State.Shopping:
                    if (!shop.isOpen && !shoppedThisRound)
                    {
                        shop.OpenShop();
                        player.money += Random.Range(2, 6);
                        shoppedThisRound = true;
                    }
                    break;
                case State.Cooldown:
                    waveToSpawn = GetWave(wave);
                    spawned.Clear();
                    foreach (var thing in waveToSpawn)
                    {
                        spawned.Add(0);
                    }
                    state = State.Spawning;
                    timer = spawnPeriod;
                    break;
                case State.Spawning:
                    state = State.Killing;
                    break;
                case State.Killing:

                    break;
            }
        }

        switch (state)
        {
            case State.Shopping:
                if (!shop.isOpen && shoppedThisRound)
                {
                    state = State.Cooldown;
                    timer = waveCooldown;
                    wave++;
                    waveText.text = "Wave: " + wave;
                    shoppedThisRound = false;
                }
                break;
            case State.Cooldown:
                break;
            case State.Spawning:
                float step = (spawnPeriod - timer) / spawnPeriod;
                for (int i = 0; i < waveToSpawn.Count; i++)
                {
                    var enemy = waveToSpawn[i];
                    int enemyType = enemy.x;
                    int toSpawn = enemy.y;
                    float s = (float)spawned[i] / (float)toSpawn;
                    if (s < step && spawned[i] < toSpawn)
                    {
                        Spawn(enemyPrefabs[enemyType]);
                        spawned[i]++;
                    }
                }
                break;
            case State.Killing:
                enemies.RemoveAll(e => e == null);
                if (enemies.Count == 0)
                {
                    // ur done, next wave
                    state = State.Shopping;
                    gameManager.WipeGroundedRocks();
                }
                break;
        }

        timer -= Time.deltaTime;
    }

    private void Spawn(Enemy enemy)
    {
        // at random location in a list of spawn locations
        var spawnPos = spawnLocations[Random.Range(0, spawnLocations.Count)].transform.position;
        var e = Instantiate(enemy, spawnPos, Quaternion.identity);
        e.Init(player);
        enemies.Add(e);
    }

    public void SpawnMinion(Vector2 position)
    {
        var e = Instantiate(enemyPrefabs[0], position, Quaternion.identity);
        e.Init(player);
        enemies.Add(e);
    }

    private List<Vector2Int> GetWave(int wave)
    {
        //float variation = GetVariation(wave);
        float variation = 1;
        Wave w = waves[wave % waves.Count];
        List<Vector2Int> waveEnemies = new();
        foreach (var enemy in w.enemies)
        {
            int count = enemy.GetEnemyCountForWave(wave, variation);
            waveEnemies.Add(new Vector2Int(enemy.enemyType, count));
        }
        return waveEnemies;
    }

    private float GetVariation(int wave)
    {
        float min = Utils.Get(wave, variationMin);
        float max = Utils.Get(wave, variationMax);
        return Random.Range(min, max);
    }

    enum State
    {
        Shopping,
        Cooldown,
        Spawning,
        Killing
    }
}
