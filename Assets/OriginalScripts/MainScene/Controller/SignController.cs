using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignController : MonoBehaviour
{
    public static SignController instance;//�C���X�^���X��
    [SerializeField] private GameObject signPanel;//�T�C���p�l��
    [SerializeField] private GameObject clickHereButton;//�N���b�N�{�^��

    [SerializeField] private TreagerousZoneSentence treagerousZoneSentence; // �G�N�Z���f�[�^���i�[
    [SerializeField] public int signKey = 1;//���͔ԍ�
    [SerializeField] private Text signText;//�e�L�X�g(�G�N�Z����sentence)

    [SerializeField] private float writeSpeed = 0;//�����X�s�[�h�B���l���������قǑf��������
    private bool isWrite = false;//�����Ă�r�����̔���

    public AudioClip clickButtonSE;//�N���b�NSE

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        Clean();
    }

    //�e�L�X�g������
    void Write(string s)
    {
        writeSpeed = 0;

        StartCoroutine(IEWrite(s));
    }

    //�e�L�X�g���e�����Z�b�g
    void Clean()
    {
        signText.text = "";
    }



    //�{�^���N���b�N���̓��e
    public void OnClick()
    {
        signPanel.SetActive(true);
        GameController.instance.PlayAudioSE(clickButtonSE);

        //�O�̃��b�Z�[�W�������Ă�r�����𔻒f�B�����r���Ȃ�true
        if (isWrite) writeSpeed = 0;       
        else
        {
            switch (signKey)
            {
                //�e�Ŕɂ����āA��ԍŌ�ɕ\������ׂ��e�L�X�g��\���B���̕��͔ԍ��̗p�ӂ͍s��Ȃ�
                case 2:
                case 5:
                case 7:
                case 9:
                case 11:
                case 16:
                case 24:
                    Clean();
                    clickHereButton.SetActive(false);

                    //�G�N�Z���f�[�^�^.���X�g�^[�ԍ�].�J������
                    Write(treagerousZoneSentence.signMessage[signKey].sentence);
                    break;

                //���͔ԍ��ɑΉ����Ă���e�L�X�g�����������̕��͔ԍ���p��
                default:
                    Clean();
                    Write(treagerousZoneSentence.signMessage[signKey].sentence);
                    signKey++;
                    break;
            }
        }
    }

    //�e�L�X�g���ꕶ�����\��
    IEnumerator IEWrite(string s)
    {
        isWrite = true;

        for (int i = 0; i < s.Length; i++)
        {
            signText.text += s.Substring(i, 1);

            yield return new WaitForSeconds(writeSpeed);
        }
        isWrite = false;
    }
}
