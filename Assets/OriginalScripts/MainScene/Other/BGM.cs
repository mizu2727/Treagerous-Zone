using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    [SerializeField] private AudioSource AudioSourceBGM;//BGM
    [SerializeField] private bool isDebug;//fobO[h

    void Start()
    {
        if (isDebug) return;
        PlayBGM();
    }

    // BGMÄ¶
    public void PlayBGM()
    {
        AudioSourceBGM.Play();
    }

    // BGMêâ~
    public void PauseBGM()
    {
        AudioSourceBGM.Pause();
    }

    // BGMêâ~ð
    public void UnPauseBGM()
    {
        AudioSourceBGM.UnPause();
    }

    //BGMâ~
    public void StopBGM()
    {
        AudioSourceBGM.Stop();
    }
}
