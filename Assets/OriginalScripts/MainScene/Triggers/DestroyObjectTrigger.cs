using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] destroyObjects;//消したいオブジェクト  
    private bool isDestroyObjectTrigger = false;//罠作動判定
    private string playerTag = "Player";//タグ
    [SerializeField] private AudioClip destroyObjectTriggerSE;//罠作動SE

    //プレイヤーが自身に触れたらオブジェクトが消滅する罠が作動する
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag)
            && isDestroyObjectTrigger == false)
        {
            isDestroyObjectTrigger = true;//2回以上作動させないために必要
            GameController.instance.PlayAudioSE(destroyObjectTriggerSE);
            for (int i = 0; i < destroyObjects.Length; i++)
            {
                Destroy(destroyObjects[i]);               
            }
        }
    }
}
