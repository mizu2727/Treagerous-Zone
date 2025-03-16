using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private string playerTag = "Player";//タグ
    public bool isGoal = false;//ステージクリア判定
    [SerializeField] private AudioClip goalSE;//ステージクリアSE

    void OnCollisionEnter2D(Collision2D collision)
    {
        //プレイヤーが自身に触れたらステージクリア
        if (collision.gameObject.CompareTag(playerTag))
        {
            GameController.instance.PlayAudioSE(goalSE);
            isGoal = true;
            GameController.instance.GameClear();
            Destroy(this.gameObject);
        }
    }
}
