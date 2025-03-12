using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    //ステージクリア系
    [SerializeField] GameObject clearPanel;//クリアパネル//
    [SerializeField] private GameObject clearText;//クリアテキスト//
    private float clearPanelAlpha = 0;//クリアパネルの透過度//
    [SerializeField] private float clearPanelAlphaSpeed = 0.01f;//クリアパネルの透過速度//
    [SerializeField] private GameObject returnToStageSelectButton;//ステージ選択へ戻るボタン//
    [SerializeField] private GameObject returnToTitleButton;//タイトルへ戻るボタン//

    //その他
    public static UIController instance;//インスタンス化
    int buildIndex;//シーンのビルド番号
    [SerializeField] private Pickaxe pickaxe;//ピッケル
    [SerializeField] private Finder finder;//探知機
    [SerializeField] private PlayerMove player;//プレイヤー
    public LifePanel lifePanel;//プレイヤーの体力//
    public int redCoinCount = 0;//赤コインの取得枚数
    [SerializeField] private GameObject tooBadText;//プレイヤー死亡時のテキスト

    public enum ToolMode//UIの選択状態 
    {
        OFF,
        Pickaxe,
        Finder
    }

    public ToolMode toolMode = ToolMode.OFF;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);

        buildIndex = SceneManager.GetActiveScene().buildIndex;

        //チュートリアル・ステージ1のみ、探知機を非表示
        if (buildIndex == 2 || buildIndex == 3) Destroy(finder.gameObject);

        ////プレイヤー死亡時・ステージクリア時のUIをゲーム開始時の状態にする
        tooBadText.SetActive(false);
        clearPanel.SetActive(false);
        clearPanel.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        clearText.SetActive(false);
        returnToStageSelectButton.SetActive(false);
        returnToTitleButton.SetActive(false);
    }

    //UIのオン/オフの切り替え
    void Update()
    {
        if(!pickaxe.isPickaxe && !finder.isFinder) toolMode = ToolMode.OFF;

        switch (toolMode) 
        {
            case ToolMode.OFF:
                pickaxe.OFFPickaxe();
                finder.OFFFinder();
                break;

            case ToolMode.Pickaxe:
                finder.OFFFinder();
                break;

            case ToolMode.Finder:
                pickaxe.OFFPickaxe();
                break;
        }
    }

    //クリアテキスト→ステージ選択へ戻るボタン、タイトルへ戻るボタンの順に表示する
    public IEnumerator Result()
    {
        //cearPanelの描画順番を一番手前にして、他のUIをクリアパネルで隠すために必要
        clearPanel.transform.SetAsLastSibling();
        clearPanel.SetActive(true);


        //クリアパネルの背景を徐々に暗くする
        clearPanel.GetComponent<Image>().color = new Color(0, 0, 0, clearPanelAlpha);
        clearPanelAlpha += clearPanelAlphaSpeed;

        yield return new WaitForSeconds(3);
        clearText.SetActive(true);

        yield return new WaitForSeconds(2);
        returnToStageSelectButton.SetActive(true);
        returnToTitleButton.SetActive(true);
    }

    //プレイヤー死亡時のテキストを表示→シーンをロードしてリスタートの順で処理する
    public IEnumerator Retry()
    {
        yield return new WaitForSeconds(0.5f);
        tooBadText.SetActive(true);

        yield return new WaitForSeconds(2.5f);
        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.name);
    }

    //プレイヤーのライフの情報
    public void PlayerLife() 
    {     
        lifePanel.UpdateLife(player.HP());
    }
}
