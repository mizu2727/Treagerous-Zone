using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePanel : MonoBehaviour
{
    public GameObject[] lifes;//�̗͂�UI 
 
    //�v���C���[�̗̑͂��ω������ꍇ�A����ɉ����đ̗͂�UI���ω�����
    public void UpdateLife(int hp)
    {
        for (int i = 0; i < lifes.Length; i++)
        {
            if (i < hp) lifes[i].SetActive(true);
            else lifes[i].SetActive(false);
        }
    }
}
