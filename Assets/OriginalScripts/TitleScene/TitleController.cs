using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    //Canvas�n
    [SerializeField] private Canvas titlesCanvas;//�^�C�g����ʂ�Canvas
    [SerializeField] private Canvas optionsCanvas;//�I�v�V������ʂ�Canvas

    //Audio�n
    private AudioSource audioSource;//AudioSource
    [SerializeField] private BGM BGMScript;//�^�C�g��BGM
    [SerializeField] private AudioClip clickButtonSE;//�N���b�NSE

    //�J�n���ɃI�v�V������ʂ�Canvas���\��
    private void Awake()
    {
        optionsCanvas.enabled = false;

        //�|�[�Y��ʂ���^�C�g���֖߂����ꍇ�ɁA�ꎞ��~���������邽�߂ɕK�v
        Time.timeScale = 1;
    }

    //�J�n���Ƀ^�C�g����ʂ�Canvas�̕\��
    private void Start()
    {
        titlesCanvas.enabled = true;            
        audioSource = GetComponent<AudioSource>();
    }

    //�Q�[���X�^�[�g
    public void OnStartButtonClicked()
    {
        BGMScript.StopBGM();
        PlayAudioSE(clickButtonSE);
        
        SceneManager.LoadScene("StageSelectScene");
    }

    //�I�v�V�������J��
    public void OnOptionButtonClicked() 
    {
        PlayAudioSE(clickButtonSE);
        titlesCanvas.enabled = false;
        optionsCanvas.enabled = true;
    }

    //���ʉ��̃e�X�g
    public void ONSETestButton() 
    {
        PlayAudioSE(clickButtonSE);
    }

    //�I�v�V���������
    public void OnReturnButtonClicked()
    {       
        PlayAudioSE(clickButtonSE);
        titlesCanvas.enabled = true;
        optionsCanvas.enabled = false;
    }

    //�Q�[���I��
    public void EndGame()
    {
        BGMScript.StopBGM();
        Application.Quit();
    }
    
    //SE��炷���\�b�h
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
