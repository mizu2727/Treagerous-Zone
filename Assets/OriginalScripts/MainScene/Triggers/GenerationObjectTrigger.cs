using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationObjectTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] generationObjects;//�����������I�u�W�F�N�g   
    private bool isGenerationObjectTrigger = false;//㩍쓮����
    private string playerTag = "Player";//�^�O
    [SerializeField] private AudioClip generationObjectTriggerSE;//㩍쓮SE

    //�v���C���[�����g�ɐG�ꂽ��I�u�W�F�N�g�����������㩂��쓮����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag)
            && isGenerationObjectTrigger == false)
        {
            isGenerationObjectTrigger = true;//2��ȏ�㩂��쓮�����Ȃ����߂ɕK�v
            GameController.instance.PlayAudioSE(generationObjectTriggerSE);
            for (int i = 0; i < generationObjects.Length; i++)
            {
                generationObjects[i].SetActive(true);
            }
        }
    }
}
