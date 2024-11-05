using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeZombie : MonoBehaviour
{
    public float attackRange = 1.5f; // The range within which the zombie can attack
    public float attackCooldown = 1f; // Time between attacks
    public int attackDamage = 2; // Damage dealt to the player
    private Transform player; // Reference to the player
    private bool isPlayerInRange; // Track if the player is in range
    private float lastAttackTime; // Track the time of the last attack
    private Animator anim; // Animator component for sword swing animation
    public Transform sword;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;
        anim = GetComponent<Animator>();

        sword = transform.Find("Zombie Weapon");
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            // Check distance to the player
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            isPlayerInRange = distanceToPlayer <= attackRange;

            if (isPlayerInRange && Time.time >= lastAttackTime + attackCooldown)
            {
                Attack(); // Call the attack method if in range and cooldown is over
            }
        }
    }

    void Attack()
    {
        lastAttackTime = Time.time; // Reset cooldown
        anim.SetTrigger("SwordSwing"); // Trigger the sword swing animation

        if (sword != null)
        {
            Vector3 direction = (player.position - sword.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            sword.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        // Optionally, you can deal damage to the player here
        PlayerStats playerstats = player.GetComponent<PlayerStats>();
        if (playerstats != null)
        {
            playerstats.bulletDamagePlayer(attackDamage); // Assuming TakeDamage is a method in your playerController
        }
    }
}
