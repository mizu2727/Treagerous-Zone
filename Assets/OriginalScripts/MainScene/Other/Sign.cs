using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
{
    [SerializeField] private GameObject signPanel;//サインパネル
    [SerializeField] private GameObject clickHereButton;//クリックボタン
    private float clickButtonPositionY = 2;//クリックボタンのY座標
    [SerializeField] private int signKeyCounter;//文章番号の設定

    //プレイヤーが看板に近づいた際にクリックボタンとパネルを表示し、文章番号を指定の数字にする
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            clickHereButton.transform.position 
                = new Vector2(this.transform.position.x,this.transform.position.y + clickButtonPositionY);
            clickHereButton.SetActive(true);
            SignController.instance.signKey += signKeyCounter;
        }
    }

    //クリックボタンを常に看板の近くに表示する
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            clickHereButton.transform.position
                = new Vector2(this.transform.position.x, this.transform.position.y + clickButtonPositionY);
        }
    }

    //プレイヤーが看板から離れた際にクリックボタンとサインパネルを非表示にし、文章番号を0にリセット
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (clickHereButton.activeSelf) clickHereButton.SetActive(false);        

        if (signPanel.activeSelf) signPanel.SetActive(false);

        SignController.instance.signKey = 0;
    }
}
