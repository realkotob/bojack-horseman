using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardMatch
{
public class RandomSFX : MonoBehaviour
{

    [SerializeField]
    private List<AudioSource> audioSources = new List<AudioSource>();

    void Start()
    {
    }

    public void Play()
    {
        int randomIndex = Random.Range(0, audioSources.Count);
        audioSources[randomIndex].Play();
    }
}
}
