using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRandomizer : MonoBehaviour
{

    public AudioClip[] footstepsAudio;
    AudioClip[] footstepsAudio2;
    public AudioSource AudioSource;

    void Awake()
    {
        AudioSource = GetComponent<AudioSource>();

        footstepsAudio2 = Resources.LoadAll<AudioClip>("Footstep(Snow and Grass)");

    }

    public void PlayRandom()
    {
        AudioSource.clip = footstepsAudio2[Random.Range(0, 29)];
        AudioSource.Play();
    }
}
