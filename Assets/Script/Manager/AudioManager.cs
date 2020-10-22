using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;

    bool SetAudio;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void WalkAudioPlay(bool isRun)
    {
        if(!SetAudio)
            StartCoroutine(WalkAudio(isRun));
    }

    IEnumerator WalkAudio(bool Run)
    {
        SetAudio = true;
        audioSource.Play();
        yield return new WaitForSeconds(Run ? 0.15f : 0.3f);
        SetAudio = false;
    }
}
