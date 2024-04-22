using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnCollision : MonoBehaviour
{
    public AudioClip hitSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyTag"))
        {
            AudioSource.PlayClipAtPoint(hitSound, transform.position);
        }
    }
}