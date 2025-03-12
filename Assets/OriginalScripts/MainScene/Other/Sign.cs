using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
{
    [SerializeField] private GameObject signPanel;//�T�C���p�l��
    [SerializeField] private GameObject clickHereButton;//�N���b�N�{�^��
    private float clickButtonPositionY = 2;//�N���b�N�{�^����Y���W
    [SerializeField] private int signKeyCounter;//���͔ԍ��̐ݒ�

    //�v���C���[���Ŕɋ߂Â����ۂɃN���b�N�{�^���ƃp�l����\�����A���͔ԍ����w��̐����ɂ���
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            clickHereButton.transform.position 
                = new Vector2(this.transform.position.x,this.transform.position.y + clickButtonPositionY);
            clickHereButton.SetActive(true);
            SignController.instance.signKey += signKeyCounter;
        }
    }

    //�N���b�N�{�^������ɊŔ̋߂��ɕ\������
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            clickHereButton.transform.position
                = new Vector2(this.transform.position.x, this.transform.position.y + clickButtonPositionY);
        }
    }

    //�v���C���[���Ŕ��痣�ꂽ�ۂɃN���b�N�{�^���ƃT�C���p�l�����\���ɂ��A���͔ԍ���0�Ƀ��Z�b�g
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (clickHereButton.activeSelf) clickHereButton.SetActive(false);        

        if (signPanel.activeSelf) signPanel.SetActive(false);

        SignController.instance.signKey = 0;
    }
}
