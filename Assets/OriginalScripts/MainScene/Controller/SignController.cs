using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignController : MonoBehaviour
{
    public static SignController instance;//インスタンス化
    [SerializeField] private GameObject signPanel;//サインパネル
    [SerializeField] private GameObject clickHereButton;//クリックボタン
    [SerializeField] private Text signText;//テキスト
    public int signKey = 1;//文章番号
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

    //文章内容
    static Dictionary<int, string> signMessage = new Dictionary<int, string>()
    {
        {0,"" },
        {1,"左キー、Aキー…左移動\n右キー、Dキー…右移動" },
        {2,"上キー、Wキー…ジャンプ\n上キー、Wキーを長押しするほどジャンプ時間が長くなります" },
        {3,"マウスカーソルをピッケルに合わせた状態で左クリックすると、ピッケルを選択、選択解除ができます" },
        {4,"ピッケルを選択した状態で右のブロックにカーソルを合わせて左クリックすると、ブロックの形が崩れます" },
        {5,"3回クリックするとブロックが壊れます" },
        {6,"ダメージを受けてしまうと、左上のライフが1つ減ってしまいます" },
        {7,"ライフが0にならないように気を付けましょう" },
        {8,"お肉を食べると、ライフが1つ回復できます" },
        {9,"ただし、ライフが3つより増えることはありません" },
        {10,"冒険では様々な罠が仕掛けられています" },
        {11,"注意しながら進みましょう" },
        {12,"ダイアは各ステージに1つずつ存在し、入手するとステージクリアとなります" },
        {13,"また、隠し要素として各ステージに3つずつ赤コインが存在します" },
        {14,"ぜひ集めてみてください" },
        {15,"冒険中に休憩したい場合は、Pキーで休憩しましょう" },
        {16,"これでチュートリアルは以上です" },
        {17,"ピッケルで破壊できるブロックは、ステージの壁などに擬態していることもあります" },
        {18,"そのため、擬態しているブロックを見抜くのは至難の業です" },
        {19,"そんな時には探知機を使います" },
        {20,"マウスカーソルを左下の探知機に合わせた状態で左クリックすると、探知機を選択、選択解除ができます" },
        {21,"探知機を選択した状態で怪しいと思った壁などにカーソルを合わせます" },
        {22,"カーソルを合わせた部分が擬態したブロックの場合は、色が変化します" },
        {23,"この方法で見つけ出したブロックをピッケルで破壊します" },
        {24,"試しに右側の壁に対して探知機を使ってみましょう" },
    };

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
                    Write(signMessage[signKey]);
                    break;

                //文章番号に対応しているテキストを書く＆次の文章番号を用意
                default:
                    Clean();
                    Write(signMessage[signKey]);
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
