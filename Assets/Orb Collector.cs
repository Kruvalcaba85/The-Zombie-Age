using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbCollector : MonoBehaviour
{
    public float pullSpeed = 5f; // Speed at which orbs are pulled towards the player

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Orb"))
        {
            // Optionally store orbs to pull multiple orbs at once
            StartCoroutine(PullOrb(other.transform));
        }
    }

    private IEnumerator PullOrb(Transform orb)
    {
        while (orb != null && Vector2.Distance(transform.position, orb.position) > 0.1f)
        {
            // Pull the orb towards the player
            orb.position = Vector2.MoveTowards(orb.position, transform.position, pullSpeed * Time.deltaTime);
            yield return null; // Wait for the next frame
        }

        if (orb != null)
        {
            Destroy(orb.gameObject);
        }
    }
}
