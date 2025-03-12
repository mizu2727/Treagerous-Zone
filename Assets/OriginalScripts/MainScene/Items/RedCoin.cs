using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCoin : MonoBehaviour
{
    [SerializeField] private AudioClip redCoinSE;//�ԃR�C���擾SE

    void OnTriggerEnter2D(Collider2D collider)
    {
        //�v���C���[�����g�ɐG�ꂽ��ԃR�C�����擾
        if (collider.gameObject.tag == "Player")
        {   
            if (UIController.instance != null)
            {
                GameController.instance.PlayAudioSE(redCoinSE);
                UIController.instance.redCoinCount++;
                Destroy(this.gameObject);
            }      
        }
    }
}
