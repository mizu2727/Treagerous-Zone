using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe : MonoBehaviour
{
    [SerializeField] private GameObject pickaxeChooseBarPanel;//選択バー
    [SerializeField] private Goal goal;//Goal
    public bool isPickaxe;//ピッケルを選択/選択解除

    //ゲーム開始時に選択バーを非表示
    void Start()
    {       
        pickaxeChooseBarPanel.SetActive(false);
    }

    private void Update()
    {
        //ステージクリア時で、ブロックを壊す手段をなくすために必要
        if (goal.isGoal) OFFPickaxe();
    }

    //クリック時にピッケルを選択/選択解除状態にする
    public void OnClick()
    {
        //ポーズ画面中にピッケルを選択/選択解除状態を切り替えさせないために必要
        if (Time.timeScale == 0 || goal.isGoal) return;
       
        if (!isPickaxe) OnPickaxe();
        else OFFPickaxe();
        //Debug.Log("isPickaxe =" + isPickaxe);
    }

    void OnPickaxe() 
    {
        isPickaxe = true;
        pickaxeChooseBarPanel.SetActive(true);
        UIController.instance.toolMode = UIController.ToolMode.Pickaxe;
    }

    public void OFFPickaxe()
    {
        if (pickaxeChooseBarPanel.activeSelf)
        {
            isPickaxe = false;
            pickaxeChooseBarPanel.SetActive(false);
        }   
    }
}
