using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCoin : MonoBehaviour
{
    private string playerTag = "Player";//タグ
    [SerializeField] private AudioClip redCoinSE;//赤コイン取得SE

    void OnTriggerEnter2D(Collider2D collider)
    {
        //プレイヤーが自身に触れたら赤コインを取得
        if (collider.gameObject.CompareTag(playerTag))
        {   
            if (UIController.instance != null)
            {
                GameController.instance.PlayAudioSE(redCoinSE);
                UIController.instance.redCoinCount++;
                Destroy(this.gameObject);
            }      
        }
    }
}
