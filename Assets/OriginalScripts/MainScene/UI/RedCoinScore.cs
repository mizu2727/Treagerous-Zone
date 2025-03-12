using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedCoinScore : MonoBehaviour
{
    private Text redCoinText = null;//�e�L�X�g
    private int oldRedCoinCount = 0;//�Â��ԃR�C���̏��

    //�Q�[���J�n���ɐԃR�C���̎擾������\��
    void Start()
    {
        redCoinText = GetComponent<Text>();
        if (UIController.instance != null) redCoinText.text = "�~" + UIController.instance.redCoinCount;      
    }
    
    void Update()
    {
        //�ԃR�C�����擾�����ۂɃe�L�X�g���X�V����
        if (oldRedCoinCount != UIController.instance.redCoinCount)
        {
            redCoinText.text = "�~" + UIController.instance.redCoinCount;
            oldRedCoinCount = UIController.instance.redCoinCount;
        }
    }
}
