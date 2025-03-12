using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseDestroy : MonoBehaviour
{
    //プレイヤーがこのスクリプトをアタッチしているオブジェクトに触れた場合、このオブジェクトを非表示にする処理を行う

    [SerializeField] private bool isPlayer;//プレイヤーがオブジェクトに触れても表示するならtrue

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player") 
        {
            if (isPlayer) return;

            this.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (isPlayer) return;

            this.gameObject.SetActive(false);
        }
    }
}
