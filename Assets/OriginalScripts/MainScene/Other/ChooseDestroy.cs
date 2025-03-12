using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseDestroy : MonoBehaviour
{
    //�v���C���[�����̃X�N���v�g���A�^�b�`���Ă���I�u�W�F�N�g�ɐG�ꂽ�ꍇ�A���̃I�u�W�F�N�g���\���ɂ��鏈�����s��

    [SerializeField] private bool isPlayer;//�v���C���[���I�u�W�F�N�g�ɐG��Ă��\������Ȃ�true

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
