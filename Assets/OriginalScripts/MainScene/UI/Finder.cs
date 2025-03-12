using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finder : MonoBehaviour
{
    [SerializeField] private GameObject finderChooseBarPanel;//選択バー
    [SerializeField] private Goal goal;//Goal
    public bool isFinder;//探知機を選択/選択解除

    //ゲーム開始時に選択バーを非表示
    void Start()
    {
        finderChooseBarPanel.SetActive(false);
    }

    private void Update()
    {
        //ステージクリア時で、ブロックを壊す手段をなくすために必要
        if (goal.isGoal) OFFFinder();    
    }

    //クリック時に探知機を選択/選択解除状態にする
    public void OnClick()
    {
        //ポーズ画面中に探知機を選択/選択解除状態を切り替えさせないために必要
        if (Time.timeScale == 0 || goal.isGoal) return;

        if (!isFinder) ONFinder();
        else OFFFinder();
    }

    void ONFinder() 
    {
        isFinder = true;
        finderChooseBarPanel.SetActive(true);
        UIController.instance.toolMode = UIController.ToolMode.Finder;
    }

    public void OFFFinder()
    {
        if (finderChooseBarPanel.activeSelf) 
        {
            isFinder = false;
            finderChooseBarPanel.SetActive(false);
        }         
    }
}
