using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource WalkAudioSouce;

    bool SetAudio;

    private void Awake()
    {
        WalkAudioSouce = GetComponent<AudioSource>();
    }

    public void WalkAudioPlay(bool isRun)
    {
        if(!SetAudio)
            StartCoroutine(WalkAudio(isRun));
    }

    IEnumerator WalkAudio(bool Run)
    {
        SetAudio = true;
        WalkAudioSouce.Play();
        yield return new WaitForSeconds(Run ? 0.2f : 0.4f);
        SetAudio = false;
    }
}
