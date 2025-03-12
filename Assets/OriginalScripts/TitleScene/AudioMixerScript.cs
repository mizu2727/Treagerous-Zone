using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixerScript : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;//AudioMixer
    [SerializeField] private Slider BGMSlider;//BGMのスライダー
    [SerializeField] private Slider SESlider;//SEのスライダー
    [SerializeField] private float minBGM = -80;//最小BGM
    [SerializeField] private float maxBGM;//最大BGM
    [SerializeField] private float minSE = -80;//最小SE
    [SerializeField] private float maxSE;//最大SE

    private void Start()
    {
        //BGMの音量調整
        if (BGMSlider != null)
        {
            //BGMSliderのvalue値が変化した場合、BGMの音量をその変化した値にする
            BGMSlider.onValueChanged.AddListener((value) =>
            {
                value = Mathf.Clamp01(value);
                float decibel = 20f * Mathf.Log10(value);
                decibel = Mathf.Clamp(decibel, minBGM, maxBGM);
                audioMixer.SetFloat("BGM Volume", decibel);
            });
        }
        else Debug.Log("BGMの調整ができません");

        //SEの音量調整
        if (SESlider != null)
        {
            //SESliderのvalue値が変化した場合、SEの音量をその変化した値にする
            SESlider.onValueChanged.AddListener((value) =>
            {
                value = Mathf.Clamp01(value);
                float decibel = 20f * Mathf.Log10(value);
                decibel = Mathf.Clamp(decibel, minSE, maxSE);
                audioMixer.SetFloat("SE Volume", decibel);
            });
        }
        else Debug.Log("SEの調整ができません");
    }
}
