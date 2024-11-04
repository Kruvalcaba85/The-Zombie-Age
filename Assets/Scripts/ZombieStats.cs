using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class ZombieStats : MonoBehaviour
{
    public int health;
    private int currentHealth;
    public GameObject experienceOrbPrefab;
    public float dropChance = 0.5f;
    public int damagetoPlayer = 2;
    private Weapon weapon;

    public float despawnDistance = 20f;
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<playerController>().transform;
        currentHealth = health;
        weapon = FindObjectOfType<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Die();
        }

        if(Vector2.Distance(transform.position, player.position) >= despawnDistance)
        {
            ReturnEnemy();
        }
    }

    public void damageEnemy(int damage)
    {
        Debug.Log(damage);
        currentHealth -= damage;
    }

    public void Die()
    {
        Debug.Log("Zombie died, checking for drop");
        if (Random.value < dropChance)
        {
            Instantiate(experienceOrbPrefab, transform.position, Quaternion.identity);
            Debug.Log("Orb Dropped!");
        }
        Destroy(gameObject);
        weapon.addWeaponExperience();
        
    }

    public void OnDestroy()
    {
        if (Application.isPlaying)
        {
            EnemySpawner es = FindObjectOfType<EnemySpawner>();
            if (es != null)
            {
                es.OnEnemyKilled();
            }
        }
    }
    void ReturnEnemy()
    {
        EnemySpawner es = FindObjectOfType<EnemySpawner>();
        transform.position = player.position + es.relativeSpawnPoints[Random.Range(0, es.relativeSpawnPoints.Count)].position;
    }
}
