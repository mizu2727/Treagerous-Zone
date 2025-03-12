using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private PlayerMove player;//�v���C���[
    [SerializeField] private Goal goal;//�S�[��
    [SerializeField] private GameObject pausePanel;//�|�[�Y�p�l��

    //Audio�n
    public BGM BGMScript;//���C���Q�[��BGM
    public AudioClip pauseSE;//�N���b�NSE
    
    //P�L�[�Ń|�[�Y/�|�[�Y����
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) 
            && player.isDead == false && goal.isGoal == false)
        {
            GameController.instance.PlayAudioSE(pauseSE);
            if (Time.timeScale == 1)
            {
                BGMScript.PauseBGM();
                Time.timeScale = 0;

                //�|�[�Y���ɑ���UI(��:�s�b�P���Ȃ�)�ɐG��Ȃ��悤�ɂ��邽�߂ɕK�v
                pausePanel.transform.SetAsLastSibling();
                pausePanel.SetActive(true);
            }
            else
            {
                OnClickedClosePauseButton();
            }
        }
    }

    //�|�[�Y����
    public void OnClickedClosePauseButton()    
    {
        GameController.instance.PlayAudioSE(pauseSE);
        BGMScript.UnPauseBGM();
        pausePanel.SetActive(false);
        Time.timeScale = 1;     
    }

    //�X�e�[�W�I���֖߂�
    public void OnClickedReturnToStageSelectButton() 
    {
        BGMScript.StopBGM();
        GameController.instance.ReturnToStageSelect();
    }

    //�^�C�g���֖߂�
    public void OnClickedReturnToTitleButton() 
    {
        BGMScript.StopBGM();
        GameController.instance.ReturnToTitle();       
    }
}
