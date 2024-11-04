using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombiebullet : MonoBehaviour
{
    public int bulletDamage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerStats>().bulletDamagePlayer(bulletDamage);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // in order to get rid of bullets in case they do not hit anything
        Destroy(this.gameObject, 3);
    }
}
