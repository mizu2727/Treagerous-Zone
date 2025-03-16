using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //ステージクリア系
    [SerializeField] private Goal goal;//ゴール

    //Audio系
    private AudioSource audioSource = null;//AudioSourece
    [SerializeField] private BGM BGMScript;//メインゲームBGM
    [SerializeField] private AudioClip clickButtonSE;//クリックSE

    //その他
    public static GameController instance;//インスタンス化
    [SerializeField] private PlayerMove player;//プレイヤー
    [SerializeField] private bool isDebug;//デバッグモード

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
        //ステージクリア時
        if (goal.isGoal)
        {
            UIController.instance.ResultScreen();
        }

        //プレイヤーのライフの情報
        //ポーズからタイトルへ戻る際に発生するエラーを防ぐために下記のif文が必要
        if (Time.timeScale == 1) UIController.instance.PlayerLife();
    }

    //ゲームクリア時
    public async void GameClear() 
    {
        if (goal.isGoal)
        {
            BGMScript.StopBGM();

            await UIController.instance.Result();
        }
    }

    //ゲームオーバー時
    public async void GameOver()
    {
        if (player.isDead)
        {
            BGMScript.StopBGM();
            enabled = false;
            await UIController.instance.Retry();
        }
    }

    //ステージ選択へ戻る
    public void ReturnToStageSelect() 
    {
        GameController.instance.PlayAudioSE(clickButtonSE);
        SceneManager.LoadScene("StageSelectScene");
    }

    //タイトルへ戻る
    public void ReturnToTitle()    
    {
        GameController.instance.PlayAudioSE(clickButtonSE);
        SceneManager.LoadScene("TitleScene");

    }

    //SEを鳴らす
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
