using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUp : MonoBehaviour
{
    [SerializeField] private PlayerMove player;//プレイヤー
    [SerializeField] private int lifeUp = 1;//プレイヤーの体力を増やす量
    [SerializeField] private AudioClip lifeUpSE;//体力を増やすSE

    void OnTriggerEnter2D(Collider2D collider)
    {
        //プレイヤーが自身に触れたら体力を増やす
        if (collider.gameObject.tag == "Player")
        {
            GameController.instance.PlayAudioSE(lifeUpSE);
            player.hp += lifeUp;
            Destroy(this.gameObject);
        }
    }
}
