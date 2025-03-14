using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignController : MonoBehaviour
{
    public static SignController instance;//インスタンス化
    [SerializeField] private GameObject signPanel;//サインパネル
    [SerializeField] private GameObject clickHereButton;//クリックボタン

    [SerializeField] private TreagerousZoneSentence treagerousZoneSentence; // エクセルデータを格納
    [SerializeField] public int signKey = 1;//文章番号
    [SerializeField] private Text signText;//テキスト(エクセルのsentence)

    [SerializeField] private float writeSpeed = 0;//書くスピード。数値が小さいほど素早く書く
    private bool isWrite = false;//書いてる途中かの判定

    public AudioClip clickButtonSE;//クリックSE

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        Clean();
    }

    //テキストを書く
    void Write(string s)
    {
        writeSpeed = 0;

        StartCoroutine(IEWrite(s));
    }

    //テキスト内容をリセット
    void Clean()
    {
        signText.text = "";
    }



    //ボタンクリック時の内容
    public void OnClick()
    {
        signPanel.SetActive(true);
        GameController.instance.PlayAudioSE(clickButtonSE);

        //前のメッセージが書いてる途中かを判断。書き途中ならtrue
        if (isWrite) writeSpeed = 0;       
        else
        {
            switch (signKey)
            {
                //各看板において、一番最後に表示するべきテキストを表示。次の文章番号の用意は行わない
                case 2:
                case 5:
                case 7:
                case 9:
                case 11:
                case 16:
                case 24:
                    Clean();
                    clickHereButton.SetActive(false);

                    //エクセルデータ型.リスト型[番号].カラム名
                    Write(treagerousZoneSentence.signMessage[signKey].sentence);
                    break;

                //文章番号に対応しているテキストを書く＆次の文章番号を用意
                default:
                    Clean();
                    Write(treagerousZoneSentence.signMessage[signKey].sentence);
                    signKey++;
                    break;
            }
        }
    }

    //テキストを一文字ずつ表示
    IEnumerator IEWrite(string s)
    {
        isWrite = true;

        for (int i = 0; i < s.Length; i++)
        {
            signText.text += s.Substring(i, 1);

            yield return new WaitForSeconds(writeSpeed);
        }
        isWrite = false;
    }
}
