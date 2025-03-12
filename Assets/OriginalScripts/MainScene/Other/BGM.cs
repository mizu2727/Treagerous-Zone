using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    [SerializeField] private AudioSource AudioSourceBGM;//BGM
    [SerializeField] private bool isDebug;//デバッグモード

    void Start()
    {
        if (isDebug) return;
        PlayBGM();
    }

    // BGM再生
    public void PlayBGM()
    {
        AudioSourceBGM.Play();
    }

    // BGM一時停止
    public void PauseBGM()
    {
        AudioSourceBGM.Pause();
    }

    // BGM一時停止解除
    public void UnPauseBGM()
    {
        AudioSourceBGM.UnPause();
    }

    //BGM停止
    public void StopBGM()
    {
        AudioSourceBGM.Stop();
    }
}
