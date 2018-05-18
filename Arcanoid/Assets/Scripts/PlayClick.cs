using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayClick : MonoBehaviour
{
    public AudioSource Click;

    void PlayClickAudio()
    {
        Click.Play();
    }
}
