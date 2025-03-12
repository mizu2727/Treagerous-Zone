using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    //Canvas系
    [SerializeField] private Canvas titlesCanvas;//タイトル画面のCanvas
    [SerializeField] private Canvas optionsCanvas;//オプション画面のCanvas

    //Audio系
    private AudioSource audioSource;//AudioSource
    [SerializeField] private BGM BGMScript;//タイトルBGM
    [SerializeField] private AudioClip clickButtonSE;//クリックSE

    //開始時にオプション画面のCanvasを非表示
    private void Awake()
    {
        optionsCanvas.enabled = false;

        //ポーズ画面からタイトルへ戻った場合に、一時停止を解除するために必要
        Time.timeScale = 1;
    }

    //開始時にタイトル画面のCanvasの表示
    private void Start()
    {
        titlesCanvas.enabled = true;            
        audioSource = GetComponent<AudioSource>();
    }

    //ゲームスタート
    public void OnStartButtonClicked()
    {
        BGMScript.StopBGM();
        PlayAudioSE(clickButtonSE);
        
        SceneManager.LoadScene("StageSelectScene");
    }

    //オプションを開く
    public void OnOptionButtonClicked() 
    {
        PlayAudioSE(clickButtonSE);
        titlesCanvas.enabled = false;
        optionsCanvas.enabled = true;
    }

    //効果音のテスト
    public void ONSETestButton() 
    {
        PlayAudioSE(clickButtonSE);
    }

    //オプションを閉じる
    public void OnReturnButtonClicked()
    {       
        PlayAudioSE(clickButtonSE);
        titlesCanvas.enabled = true;
        optionsCanvas.enabled = false;
    }

    //ゲーム終了
    public void EndGame()
    {
        BGMScript.StopBGM();
        Application.Quit();
    }
    
    //SEを鳴らすメソッド
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
