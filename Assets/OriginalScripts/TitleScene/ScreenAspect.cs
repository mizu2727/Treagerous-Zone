using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenAspect : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod]

    //�Q�[���J�n����Awake�֐����O�ɂ��̃��\�b�h���Ăяo��
    static void RuntimeMethodLoad()
    {
        // �X�N���[���T�C�Y���w��
        Screen.SetResolution(1920, 1080, false);
    }
}
