using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    public static void PLayClickSound()
    {
        AudioSource clickSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        clickSource.pitch = Random.Range(0.9f, 1.1f);
        clickSource.Play();
    }
}
