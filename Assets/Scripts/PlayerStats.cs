using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    public int currentHealth, maxHealth, currentExperience, MaxExperience, currentLevel;

    private playerController playerController;
    private ZombieStats zombieStats;
    private Zombiebullet zombieBullet;
    public AudioSource DamageSound;
    public AudioSource DeathSound;
    public bool isDead = false;
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
            zombieDamagePlayer();
        }
    }

    void zombieDamagePlayer()
    {
        if (isDead) return;
        DamageSound.Play();
        Debug.Log("Player was damaged.");
        currentHealth -= zombieStats.damagetoPlayer;

        if (currentHealth <= 0)
        {
            HandleDeath();
        }
    }

    public void bulletDamagePlayer(int damage)
    {
        if (isDead) return;
        DamageSound.Play();
        Debug.LogWarning("Player damaged");
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            HandleDeath();
        }
    }

    void HandleDeath()
    {
        if (!isDead) // Ensure the death logic only runs once
        {
            isDead = true; // Set the flag to indicate player is dead
            DeathSound.Play();
            Debug.Log("Player died :(");
            playerController.DisableMovement();
        }
    }
}
