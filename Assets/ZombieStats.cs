using System.Collections;
using System.Collections.Generic;
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
    // Start is called before the first frame update
    void Start()
    {
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
}
