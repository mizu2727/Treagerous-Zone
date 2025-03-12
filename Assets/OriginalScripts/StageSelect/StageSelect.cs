using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int senceChangeNumber;//Scenes In Buildに登録されているシーンのビルド番号

    //Audio系
    private AudioSource audioSource;//AudioSource
    [SerializeField] private BGM BGMScript;//ステージ選択BGM
    [SerializeField] private AudioClip clickButtonSE;//クリックSE

    private void Start()
    {
        //ポーズ画面からステージ選択へ戻った場合、一時停止を解除するために必要
        Time.timeScale = 1;
        audioSource = GetComponent<AudioSource>();
    }

    //クリック時に指定のビルド番号があるシーンをロードする
    public void OnPointerClick(PointerEventData eventData)
    {
        BGMScript.StopBGM();
        PlayAudioSE(clickButtonSE);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(senceChangeNumber);
    }

    //SEを鳴らす
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
