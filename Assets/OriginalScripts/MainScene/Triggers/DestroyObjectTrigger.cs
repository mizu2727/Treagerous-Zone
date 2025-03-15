using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] destroyObjects;//���������I�u�W�F�N�g  
    private bool isDestroyObjectTrigger = false;//㩍쓮����
    private string playerTag = "Player";//�^�O
    [SerializeField] private AudioClip destroyObjectTriggerSE;//㩍쓮SE

    //�v���C���[�����g�ɐG�ꂽ��I�u�W�F�N�g�����ł���㩂��쓮����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag)
            && isDestroyObjectTrigger == false)
        {
            isDestroyObjectTrigger = true;//2��ȏ�쓮�����Ȃ����߂ɕK�v
            GameController.instance.PlayAudioSE(destroyObjectTriggerSE);
            for (int i = 0; i < destroyObjects.Length; i++)
            {
                Destroy(destroyObjects[i]);               
            }
        }
    }
}
