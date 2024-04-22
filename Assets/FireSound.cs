using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSound : MonoBehaviour
{
    public AudioClip fireSound;

    private void Awake()
    {
        AudioManager.Instance.PlaySoundAtPoint(fireSound, transform.position);
    }
}