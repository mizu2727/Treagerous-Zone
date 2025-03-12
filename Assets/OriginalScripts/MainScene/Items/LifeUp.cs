using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUp : MonoBehaviour
{
    [SerializeField] private PlayerMove player;//�v���C���[
    [SerializeField] private int lifeUp = 1;//�v���C���[�̗̑͂𑝂₷��
    [SerializeField] private AudioClip lifeUpSE;//�̗͂𑝂₷SE

    void OnTriggerEnter2D(Collider2D collider)
    {
        //�v���C���[�����g�ɐG�ꂽ��̗͂𑝂₷
        if (collider.gameObject.tag == "Player")
        {
            GameController.instance.PlayAudioSE(lifeUpSE);
            player.hp += lifeUp;
            Destroy(this.gameObject);
        }
    }
}
