using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public  float fireForce = 20f;

    public int maxAmmo = 10;
    public int currentAmmo;
    public float reloadTime = 2f;
    private bool isReloading = false;
    public int maxKillCount = 5;
    public int currentKillCount = 0; //public variables are set in inspector
    public int bulletDamage;
    public AudioSource shootingSound;
    public AudioSource lastRoundSound;
    public AudioSource reloadSound;
    public AudioSource reloadCompleteSound;

    private PlayerStats playerStats;

    private void Start()
    {
        currentAmmo = maxAmmo; //initialize the pistol with a full mag
        bulletDamage = 1;
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerStats = player.GetComponent<PlayerStats>();
        }

    }
    public void Fire()
    {
        if (isReloading) return;

        if(currentAmmo == 1)
        {
            GameObject bulletInstance = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bulletInstance.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
            bulletInstance.GetComponent<bullet>().bulletDamage = bulletDamage;

            currentAmmo--;
            lastRoundSound.Play();
        }
        if (currentAmmo > 1)
        {
            GameObject bulletInstance = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bulletInstance.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
            bulletInstance.GetComponent<bullet>().bulletDamage = bulletDamage;

            currentAmmo--;

            shootingSound.Play();
        }
        else
        {
            Debug.Log("Out of ammo! Reloading...");
            StartCoroutine(Reload());
        }
        
    }
    public void addWeaponExperience()
    {
        currentKillCount++;
        if (currentKillCount >= maxKillCount)
        {
            levelUp();
        }
    }

    void levelUp()
    {
        currentAmmo = maxAmmo + 5;
        maxAmmo += 5;
        Debug.Log("new Current Ammo is " + currentAmmo);
        currentKillCount = 0;
        maxKillCount += 5 + playerStats.currentLevel;
        fireForce++;
        bulletDamage++;
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        shootingSound.Stop();
        reloadSound.Play();
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
        Debug.Log("Reload complete!");
        reloadCompleteSound.Play();

    }

}
