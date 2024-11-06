using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class expOrb : MonoBehaviour
{
    public int experiencePoint = 10;
    public AudioSource xpGain;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            xpGain.Play();
            collision.GetComponent<PlayerStats>().AddExperience(experiencePoint);
            Destroy(gameObject);
        }
    }
}
