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

    public Transform firePoint;
    public GameObject bulletPrefab;
    private ZombieMovement zombieMovement;

    // Start is called before the first frame update
    void Start()
    {
        zombieMovement = GetComponent<ZombieMovement>();
        target = GameObject.Find("Player").transform;
        timeToFire = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;

        float distance = Vector2.Distance(target.position, transform.position);

        Vector2 direction = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // If the zombie is within shooting range, stop movement
        if (distance <= distanceToStop)
        {
            zombieMovement.moveSpeed = 0f;
            
            if(timeToFire <= 0f)
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
            GameObject bulletInstance = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bulletInstance.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
            bulletInstance.GetComponent<bullet>().bulletDamage = bulletDamage;
    }
}
