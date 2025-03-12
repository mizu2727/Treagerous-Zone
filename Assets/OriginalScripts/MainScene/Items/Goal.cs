using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool isGoal = false;//�X�e�[�W�N���A����
    [SerializeField] private AudioClip goalSE;//�X�e�[�W�N���ASE
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        //�v���C���[�����g�ɐG�ꂽ��X�e�[�W�N���A
        if (collision.gameObject.tag == "Player")
        {
            GameController.instance.PlayAudioSE(goalSE);
            isGoal = true;
            Destroy(this.gameObject);
        }
    }
}
