using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedCoinScore : MonoBehaviour
{
    private Text redCoinText = null;//テキスト
    private int oldRedCoinCount = 0;//古い赤コインの情報

    //ゲーム開始時に赤コインの取得枚数を表示
    void Start()
    {
        redCoinText = GetComponent<Text>();
        if (UIController.instance != null) redCoinText.text = "×" + UIController.instance.redCoinCount;      
    }
    
    void Update()
    {
        //赤コインを取得した際にテキストを更新する
        if (oldRedCoinCount != UIController.instance.redCoinCount)
        {
            redCoinText.text = "×" + UIController.instance.redCoinCount;
            oldRedCoinCount = UIController.instance.redCoinCount;
        }
    }
}
