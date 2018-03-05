using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSound : MonoBehaviour
{
    public AudioSource buttonClickAudio;
    public AudioSource gridClickAudio;

    public void PlayButtonClick()
    {
        buttonClickAudio.Play();
    }

    public void PlayGridClick()
    {
        gridClickAudio.Play();
    }
}
