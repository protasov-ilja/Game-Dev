using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour {

    private AudioSource destroyAudio;

    void Awake()
    {
        destroyAudio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name.Contains("Ball"))
        {
            destroyAudio.Play();
            Destroy(col.gameObject, 0f); // Wait = 0 seconds
        }

        GameController.instance.LoseLife();
    }
}
