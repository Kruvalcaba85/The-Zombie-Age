using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private float meleeSpeed;
    [SerializeField] private int damage;

    private float timeUntilMelee;
    private bool isAttacking; // New boolean to prevent multiple triggers
    private HashSet<Collider2D> hitEnemies; // Track hit enemies
    public AudioSource swingingSound;

    void Start()
    {
        timeUntilMelee = 0f;
        isAttacking = false;
        hitEnemies = new HashSet<Collider2D>();
    }

    public void Attack()
    {
        if (timeUntilMelee <= 0f && !isAttacking)
        {
            if (Input.GetMouseButtonDown(0))
            {
                swingingSound.Play();
                anim.SetTrigger("Attack");
                timeUntilMelee = meleeSpeed;
                isAttacking = true; // Set to true once attack starts
                hitEnemies.Clear();
            }
        }
    }
    void Update()
    {
        if (timeUntilMelee > 0f)
        {
            timeUntilMelee -= Time.deltaTime;
            if (timeUntilMelee <= 0f)
            {
                isAttacking = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && isAttacking)
        {
            Debug.Log("Enemy Hit");
            collision.gameObject.GetComponent<ZombieStats>().damageEnemy(damage);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Remove the enemy from the hit tracking when the sword exits their collider
        if (collision.CompareTag("Enemy"))
        {
            hitEnemies.Remove(collision);
        }
    }

}
