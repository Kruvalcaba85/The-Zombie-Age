using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<EnemyGroup> enemyGroups;
        public int waveQuota; // total number of enemies to spawn in wave
        public float spawnInterval; // the interval at which to spawn enemies
        public float spawnCount; // the number of enemies spawned in current wave

    }

    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public int enemyCount; //number of enemies in the wave
        public int spawnCount; //number of enemies of the current type already spawned in
        public GameObject enemyPrefab; 
    }

    public List<Wave> waves; // A list of all the waves in the game
    public int currentWaveCount; // index of current wave

    [Header("Spawner Atrributes")]
    float spawnTimer; // timer for determining when to spawn next enemy
    public int enemiesAlive;
    public int maxEnemiesAllowed; //max amount of enemies allowed at once
    public bool maxEnemiesReached = false;
    public float waveInterval; // interval between waves


    Transform player;
    [Header("Spawn Positions")]
    public List<Transform> relativeSpawnPoints; // list storing all relative spawn points


    [SerializeField]
    private GameObject zombiePrefab;

    private bool waveStarted = false;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<playerController>().transform;
        CalculateWaveQuota();
        //StartCoroutine(spawnEnemy(zombieInterval, zombiePrefab));
        //StartCoroutine(SpawnEnemies());
    }

    private void Update()
    {
        if( !waveStarted && currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount == 0) //check if wave has ended and next wave should start
        {
            StartCoroutine(BeginNextWave());
            waveStarted = true;
        }
        spawnTimer += Time.deltaTime;

        //check if its time to spawn next enemy
        if(spawnTimer >= waves[currentWaveCount].spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemies();
        }
    }

    IEnumerator BeginNextWave()
    {
        //wave for 'waveinterval' seconds before next wave
        yield return new WaitForSeconds(waveInterval);

        //if there are still waves to go, move on to next wave
        if(currentWaveCount < waves.Count - 1)
        {
            currentWaveCount++;
            CalculateWaveQuota();
            waveStarted = false;
        }
    }
    /*
    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5f, 5), Random.Range(-6f, 6), 0), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }
    */
    /*
    private IEnumerator SpawnEnemies()
    {
        while (spawning)
        {
            GameObject newEnemy = Instantiate(zombiePrefab, new Vector3(Random.Range(-5f, -5f), Random.Range(-6f, 6f), 0), Quaternion.identity);
            yield return new WaitForSeconds(zombieInterval);
        }
    }
    */
    void CalculateWaveQuota()
    {
        int currentWaveQuota = 0;
        foreach(var enemyGroup in waves[currentWaveCount].enemyGroups)
        {
            currentWaveQuota += enemyGroup.enemyCount;
        }

        waves[currentWaveCount].waveQuota = currentWaveQuota;
        //Debug.LogWarning(currentWaveQuota);
    }
    
    void SpawnEnemies()
    {
        //checks if the minimum number of enemies in the wave have been spawned
        if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota && !maxEnemiesReached)
        {
            //spawn each type of enemy until quota is filled
            foreach(var enemyGroup in waves[currentWaveCount].enemyGroups)
            {
                // check if the minimum number of enemies of this type have been spawned
                if(enemyGroup.spawnCount < enemyGroup.enemyCount)
                {
                    if(enemiesAlive >= maxEnemiesAllowed)
                    {
                        maxEnemiesReached = true;
                        return;
                    }
                    Instantiate(enemyGroup.enemyPrefab, player.position + relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Count)].position, Quaternion.identity); 
                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                    enemiesAlive++;
                }
            }
        }
        //reset flag if the number of enemies is less than the allowed amount
        if(enemiesAlive < maxEnemiesAllowed)
        {
            maxEnemiesReached = false;
        }
    }

    public void OnEnemyKilled()
    {
        enemiesAlive--;
    }
}
