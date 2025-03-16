using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //�X�e�[�W�N���A�n
    [SerializeField] private Goal goal;//�S�[��

    //Audio�n
    private AudioSource audioSource = null;//AudioSourece
    [SerializeField] private BGM BGMScript;//���C���Q�[��BGM
    [SerializeField] private AudioClip clickButtonSE;//�N���b�NSE

    //���̑�
    public static GameController instance;//�C���X�^���X��
    [SerializeField] private PlayerMove player;//�v���C���[
    [SerializeField] private bool isDebug;//�f�o�b�O���[�h

    private void Awake()
    {       
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);
              
        Time.timeScale = 1;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        //�X�e�[�W�N���A��
        if (goal.isGoal)
        {
            UIController.instance.ResultScreen();
        }

        //�v���C���[�̃��C�t�̏��
        //�|�[�Y����^�C�g���֖߂�ۂɔ�������G���[��h�����߂ɉ��L��if�����K�v
        if (Time.timeScale == 1) UIController.instance.PlayerLife();
    }

    //�Q�[���N���A��
    public async void GameClear() 
    {
        if (goal.isGoal)
        {
            BGMScript.StopBGM();

            await UIController.instance.Result();
        }
    }

    //�Q�[���I�[�o�[��
    public async void GameOver()
    {
        if (player.isDead)
        {
            BGMScript.StopBGM();
            enabled = false;
            await UIController.instance.Retry();
        }
    }

    //�X�e�[�W�I���֖߂�
    public void ReturnToStageSelect() 
    {
        GameController.instance.PlayAudioSE(clickButtonSE);
        SceneManager.LoadScene("StageSelectScene");
    }

    //�^�C�g���֖߂�
    public void ReturnToTitle()    
    {
        GameController.instance.PlayAudioSE(clickButtonSE);
        SceneManager.LoadScene("TitleScene");

    }

    //SE��炷
    public void PlayAudioSE(AudioClip audioClip) 
    {
        if (audioSource != null && !isDebug)
        {
            audioSource.PlayOneShot(audioClip);
        }
        else 
        {
            Debug.Log("No Setting AudioSource!");
        }
    }
}
