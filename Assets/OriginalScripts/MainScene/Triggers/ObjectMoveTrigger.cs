using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoveTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] objectMoves;//移動させたいオブジェクト
    [SerializeField] private Vector3[] specificPosition;//特定の位置
    [SerializeField] private Vector3[] speed;//移動速度
    [SerializeField] private int objectMode;//モードの切り替え  
    private bool isObjectMoveTrigger = false;//罠作動判定
    [SerializeField] private AudioClip objectMoveTriggerSE;//罠作動SE

    void Update()
    {
        //移動させる
        if (isObjectMoveTrigger)
        {
            for (int i = 0; i < objectMoves.Length; i++)
            {
                //オブジェクトが特定の位置に着いた場合の処理内容
                if (objectMoves[i].transform.position == specificPosition[i])
                {
                    //処理内容はモードによって異なる
                    switch (objectMode)
                    {
                        //削除
                        case 1:
                            objectMoves[i].transform.position = specificPosition[i];
                            objectMoves[i].SetActive(false);
                            break;

                        //停止
                        case 2:
                            objectMoves[i].transform.position = specificPosition[i];
                            break;

                        default:
                            break;
                    }
                }
                else 
                {
                    objectMoves[i].transform.position += speed[i];
                }
            }
        }
    }

    //プレイヤーが自身に触れたらオブジェクトを移動させる罠が作動する
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player"
            && isObjectMoveTrigger == false)
        {
            isObjectMoveTrigger = true;//2回以上作動させないために必要
            GameController.instance.PlayAudioSE(objectMoveTriggerSE);
        }
    }
}
