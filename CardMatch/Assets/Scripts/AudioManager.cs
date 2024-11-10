using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace CardMatch
{
public class AudioManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private GameObject matchSoundObject;

    [SerializeField]
    private GameObject mismatchSoundObject;

    [SerializeField]
    private AudioSource levelCompleteSource;

    private Queue<AudioSource> matchSoundQueue = new Queue<AudioSource>();
    private Queue<AudioSource> mismatchSoundQueue = new Queue<AudioSource>();

    void Start()
    {
        matchSoundQueue.Enqueue(matchSoundObject.GetComponent<AudioSource>());
        mismatchSoundQueue.Enqueue(mismatchSoundObject.GetComponent<AudioSource>());
    }

    public void PlayMatchSound()
    {
        PlaySoundFromQueue(matchSoundQueue);
    }

    public void PlayMismatchSound()
    {
        PlaySoundFromQueue(mismatchSoundQueue);
    }

    public void PlayLevelComplete()
    {
        levelCompleteSource.Play();
    }

    private void PlaySoundFromQueue(Queue<AudioSource> audioSourceQueue)
    {
        var nextSound = audioSourceQueue.Dequeue();
        if (nextSound.isPlaying)
        {
            var newSound = Instantiate(nextSound, Vector3.zero, Quaternion.identity, nextSound.transform.parent);
            var audioSourceComponent = newSound.GetComponent<AudioSource>();

            audioSourceComponent.Play();
            audioSourceQueue.Enqueue(audioSourceComponent);
        }
        else
        {
            nextSound.Play();
            audioSourceQueue.Enqueue(nextSound);
        }
    }
}
}