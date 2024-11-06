using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterZombie : MonoBehaviour
{
    public Transform target;
    public float distanceToShoot = 5f;
    public float distanceToStop = 3f;
    public float fireForce = 20f;
    public int bulletDamage;
    public float fireRate = 1f;
    public float timeToFire;
    public AudioSource shootSound;

    public Transform firePoint;
    public GameObject bulletPrefab;
    private ZombieMovement zombieMovement;

    void Start()
    {
        zombieMovement = GetComponent<ZombieMovement>();
        target = GameObject.Find("Player").transform;
        timeToFire = 1f;
    }

    void Update()
    {
        if (target == null) return;

        float distance = Vector2.Distance(target.position, transform.position);

        // Rotate the firePoint to aim at the player
        Vector2 direction = (target.position - firePoint.position).normalized;
        firePoint.up = direction; // Aim firePoint only towards the player for shooting

        // Control movement and shooting
        if (distance <= distanceToStop)
        {
            zombieMovement.moveSpeed = 0f;

            if (timeToFire <= 0f)
            {
                Shoot();
                timeToFire = fireRate;
            }
        }
        else
        {
            zombieMovement.moveSpeed = 2f;
        }

        if (timeToFire > 0f)
        {
            timeToFire -= Time.deltaTime;
        }
    }

    private void Shoot()
    {
        shootSound.Play();
        GameObject bulletInstance = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bulletInstance.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
        bulletInstance.GetComponent<Zombiebullet>().bulletDamage = bulletDamage;
    }
}
