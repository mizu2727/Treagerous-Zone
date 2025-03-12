using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixerScript : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;//AudioMixer
    [SerializeField] private Slider BGMSlider;//BGM�̃X���C�_�[
    [SerializeField] private Slider SESlider;//SE�̃X���C�_�[
    [SerializeField] private float minBGM = -80;//�ŏ�BGM
    [SerializeField] private float maxBGM;//�ő�BGM
    [SerializeField] private float minSE = -80;//�ŏ�SE
    [SerializeField] private float maxSE;//�ő�SE

    private void Start()
    {
        //BGM�̉��ʒ���
        if (BGMSlider != null)
        {
            //BGMSlider��value�l���ω������ꍇ�ABGM�̉��ʂ����̕ω������l�ɂ���
            BGMSlider.onValueChanged.AddListener((value) =>
            {
                value = Mathf.Clamp01(value);
                float decibel = 20f * Mathf.Log10(value);
                decibel = Mathf.Clamp(decibel, minBGM, maxBGM);
                audioMixer.SetFloat("BGM Volume", decibel);
            });
        }
        else Debug.Log("BGM�̒������ł��܂���");

        //SE�̉��ʒ���
        if (SESlider != null)
        {
            //SESlider��value�l���ω������ꍇ�ASE�̉��ʂ����̕ω������l�ɂ���
            SESlider.onValueChanged.AddListener((value) =>
            {
                value = Mathf.Clamp01(value);
                float decibel = 20f * Mathf.Log10(value);
                decibel = Mathf.Clamp(decibel, minSE, maxSE);
                audioMixer.SetFloat("SE Volume", decibel);
            });
        }
        else Debug.Log("SE�̒������ł��܂���");
    }
}
