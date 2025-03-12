using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoveTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] objectMoves;//�ړ����������I�u�W�F�N�g
    [SerializeField] private Vector3[] specificPosition;//����̈ʒu
    [SerializeField] private Vector3[] speed;//�ړ����x
    [SerializeField] private int objectMode;//���[�h�̐؂�ւ�  
    private bool isObjectMoveTrigger = false;//㩍쓮����
    [SerializeField] private AudioClip objectMoveTriggerSE;//㩍쓮SE

    void Update()
    {
        //�ړ�������
        if (isObjectMoveTrigger)
        {
            for (int i = 0; i < objectMoves.Length; i++)
            {
                //�I�u�W�F�N�g������̈ʒu�ɒ������ꍇ�̏������e
                if (objectMoves[i].transform.position == specificPosition[i])
                {
                    //�������e�̓��[�h�ɂ���ĈقȂ�
                    switch (objectMode)
                    {
                        //�폜
                        case 1:
                            objectMoves[i].transform.position = specificPosition[i];
                            objectMoves[i].SetActive(false);
                            break;

                        //��~
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

    //�v���C���[�����g�ɐG�ꂽ��I�u�W�F�N�g���ړ�������㩂��쓮����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player"
            && isObjectMoveTrigger == false)
        {
            isObjectMoveTrigger = true;//2��ȏ�쓮�����Ȃ����߂ɕK�v
            GameController.instance.PlayAudioSE(objectMoveTriggerSE);
        }
    }
}
