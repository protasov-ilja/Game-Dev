using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayThisSound : MonoBehaviour
{
    private AudioSource bunceAudio;

    void Awake()
    {
        bunceAudio = GetComponent<AudioSource> ();
    }

    void OnCollisionEnter(Collision other)
    {
        bunceAudio.Play();
    }
}
