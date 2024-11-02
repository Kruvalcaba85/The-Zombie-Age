using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject zombiePrefab;

    [SerializeField]
    private float zombieInterval = 0.001f;


    private bool spawning = true;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(spawnEnemy(zombieInterval, zombiePrefab));
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5f, 5), Random.Range(-6f, 6), 0), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }

    private IEnumerator SpawnEnemies()
    {
        while (spawning)
        {
            GameObject newEnemy = Instantiate(zombiePrefab, new Vector3(Random.Range(-5f, -5f), Random.Range(-6f, 6f), 0), Quaternion.identity);
            yield return new WaitForSeconds(zombieInterval);
        }
    }
}
