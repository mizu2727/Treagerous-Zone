using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private PlayerMove player;//プレイヤー
    [SerializeField] private Goal goal;//ゴール
    [SerializeField] private GameObject pausePanel;//ポーズパネル

    //Audio系
    public BGM BGMScript;//メインゲームBGM
    public AudioClip pauseSE;//クリックSE
    
    //Pキーでポーズ/ポーズ解除
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

                //ポーズ時に他のUI(例:ピッケルなど)に触れないようにするために必要
                pausePanel.transform.SetAsLastSibling();
                pausePanel.SetActive(true);
            }
            else
            {
                OnClickedClosePauseButton();
            }
        }
    }

    //ポーズ解除
    public void OnClickedClosePauseButton()    
    {
        GameController.instance.PlayAudioSE(pauseSE);
        BGMScript.UnPauseBGM();
        pausePanel.SetActive(false);
        Time.timeScale = 1;     
    }

    //ステージ選択へ戻る
    public void OnClickedReturnToStageSelectButton() 
    {
        BGMScript.StopBGM();
        GameController.instance.ReturnToStageSelect();
    }

    //タイトルへ戻る
    public void OnClickedReturnToTitleButton() 
    {
        BGMScript.StopBGM();
        GameController.instance.ReturnToTitle();       
    }
}
