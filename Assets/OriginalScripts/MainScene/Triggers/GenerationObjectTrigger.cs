using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationObjectTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] generationObjects;//生成したいオブジェクト   
    private bool isGenerationObjectTrigger = false;//罠作動判定
    private string playerTag = "Player";//タグ
    [SerializeField] private AudioClip generationObjectTriggerSE;//罠作動SE

    //プレイヤーが自身に触れたらオブジェクトが生成される罠が作動する
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag)
            && isGenerationObjectTrigger == false)
        {
            isGenerationObjectTrigger = true;//2回以上罠を作動させないために必要
            GameController.instance.PlayAudioSE(generationObjectTriggerSE);
            for (int i = 0; i < generationObjects.Length; i++)
            {
                generationObjects[i].SetActive(true);
            }
        }
    }
}
