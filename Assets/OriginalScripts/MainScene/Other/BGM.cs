using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    [SerializeField] private AudioSource AudioSourceBGM;//BGM
    [SerializeField] private bool isDebug;//�f�o�b�O���[�h

    void Start()
    {
        if (isDebug) return;
        PlayBGM();
    }

    // BGM�Đ�
    public void PlayBGM()
    {
        AudioSourceBGM.Play();
    }

    // BGM�ꎞ��~
    public void PauseBGM()
    {
        AudioSourceBGM.Pause();
    }

    // BGM�ꎞ��~����
    public void UnPauseBGM()
    {
        AudioSourceBGM.UnPause();
    }

    //BGM��~
    public void StopBGM()
    {
        AudioSourceBGM.Stop();
    }
}
