using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool isGoal = false;//ステージクリア判定
    [SerializeField] private AudioClip goalSE;//ステージクリアSE
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        //プレイヤーが自身に触れたらステージクリア
        if (collision.gameObject.tag == "Player")
        {
            GameController.instance.PlayAudioSE(goalSE);
            isGoal = true;
            Destroy(this.gameObject);
        }
    }
}
