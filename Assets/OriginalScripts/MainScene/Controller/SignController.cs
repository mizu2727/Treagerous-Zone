using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignController : MonoBehaviour
{
    public static SignController instance;//�C���X�^���X��
    [SerializeField] private GameObject signPanel;//�T�C���p�l��
    [SerializeField] private GameObject clickHereButton;//�N���b�N�{�^��
    [SerializeField] private Text signText;//�e�L�X�g
    public int signKey = 1;//���͔ԍ�
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

    //���͓��e
    static Dictionary<int, string> signMessage = new Dictionary<int, string>()
    {
        {0,"" },
        {1,"���L�[�AA�L�[�c���ړ�\n�E�L�[�AD�L�[�c�E�ړ�" },
        {2,"��L�[�AW�L�[�c�W�����v\n��L�[�AW�L�[�𒷉�������قǃW�����v���Ԃ������Ȃ�܂�" },
        {3,"�}�E�X�J�[�\�����s�b�P���ɍ��킹����Ԃō��N���b�N����ƁA�s�b�P����I���A�I���������ł��܂�" },
        {4,"�s�b�P����I��������ԂŉE�̃u���b�N�ɃJ�[�\�������킹�č��N���b�N����ƁA�u���b�N�̌`������܂�" },
        {5,"3��N���b�N����ƃu���b�N�����܂�" },
        {6,"�_���[�W���󂯂Ă��܂��ƁA����̃��C�t��1�����Ă��܂��܂�" },
        {7,"���C�t��0�ɂȂ�Ȃ��悤�ɋC��t���܂��傤" },
        {8,"������H�ׂ�ƁA���C�t��1�񕜂ł��܂�" },
        {9,"�������A���C�t��3��葝���邱�Ƃ͂���܂���" },
        {10,"�`���ł͗l�X��㩂��d�|�����Ă��܂�" },
        {11,"���ӂ��Ȃ���i�݂܂��傤" },
        {12,"�_�C�A�͊e�X�e�[�W��1�����݂��A���肷��ƃX�e�[�W�N���A�ƂȂ�܂�" },
        {13,"�܂��A�B���v�f�Ƃ��Ċe�X�e�[�W��3���ԃR�C�������݂��܂�" },
        {14,"���ЏW�߂Ă݂Ă�������" },
        {15,"�`�����ɋx�e�������ꍇ�́AP�L�[�ŋx�e���܂��傤" },
        {16,"����Ń`���[�g���A���͈ȏ�ł�" },
        {17,"�s�b�P���Ŕj��ł���u���b�N�́A�X�e�[�W�̕ǂȂǂɋ[�Ԃ��Ă��邱�Ƃ�����܂�" },
        {18,"���̂��߁A�[�Ԃ��Ă���u���b�N���������͎̂���̋Ƃł�" },
        {19,"����Ȏ��ɂ͒T�m�@���g���܂�" },
        {20,"�}�E�X�J�[�\���������̒T�m�@�ɍ��킹����Ԃō��N���b�N����ƁA�T�m�@��I���A�I���������ł��܂�" },
        {21,"�T�m�@��I��������Ԃŉ������Ǝv�����ǂȂǂɃJ�[�\�������킹�܂�" },
        {22,"�J�[�\�������킹���������[�Ԃ����u���b�N�̏ꍇ�́A�F���ω����܂�" },
        {23,"���̕��@�Ō����o�����u���b�N���s�b�P���Ŕj�󂵂܂�" },
        {24,"�����ɉE���̕ǂɑ΂��ĒT�m�@���g���Ă݂܂��傤" },
    };

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
                    Write(signMessage[signKey]);
                    break;

                //���͔ԍ��ɑΉ����Ă���e�L�X�g�����������̕��͔ԍ���p��
                default:
                    Clean();
                    Write(signMessage[signKey]);
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
