using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int senceChangeNumber;//Scenes In Build�ɓo�^����Ă���V�[���̃r���h�ԍ�

    //Audio�n
    private AudioSource audioSource;//AudioSource
    [SerializeField] private BGM BGMScript;//�X�e�[�W�I��BGM
    [SerializeField] private AudioClip clickButtonSE;//�N���b�NSE

    private void Start()
    {
        //�|�[�Y��ʂ���X�e�[�W�I���֖߂����ꍇ�A�ꎞ��~���������邽�߂ɕK�v
        Time.timeScale = 1;
        audioSource = GetComponent<AudioSource>();
    }

    //�N���b�N���Ɏw��̃r���h�ԍ�������V�[�������[�h����
    public void OnPointerClick(PointerEventData eventData)
    {
        BGMScript.StopBGM();
        PlayAudioSE(clickButtonSE);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(senceChangeNumber);
    }

    //SE��炷
    public void PlayAudioSE(AudioClip audioClip)
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
        else
        {
            Debug.Log("No Setting AudioSource!");
        }
    }
}
