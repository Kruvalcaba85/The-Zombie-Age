using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    public int currentHealth, maxHealth, currentExperience, MaxExperience, currentLevel;

    private playerController playerController;
    private ZombieStats zombieStats;
    public void Awake()
    {
        playerController = GetComponent<playerController>();
    }

    public void AddExperience(int amount)
    {
        currentExperience += amount * currentLevel;
        if(currentExperience >= MaxExperience)
        {
            Debug.Log("Leveled Up!");
            LevelUp();
            Debug.Log("New level is " + currentLevel);
        }
    }

    void LevelUp()
    {
        currentLevel++;
        currentExperience -= MaxExperience;
        MaxExperience = currentLevel * 50;
        IncreaseHealth();
        IncreaseSpeed();
    }
    
    void IncreaseHealth()
    {
        currentHealth = maxHealth;
        maxHealth = maxHealth * currentLevel;
    }
    void IncreaseSpeed()
    {
        playerController.moveSpeed += 0.5f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            zombieStats = collision.gameObject.GetComponent<ZombieStats>();
            damagePlayer();
        }
    }

    void damagePlayer()
    {
        Debug.Log("Player was damaged.");
        currentHealth -= zombieStats.damagetoPlayer;

        if (currentHealth <= 0)
        {
            Debug.Log("Player died :(");
            playerController.DisableMovement();
        }
    }
}
