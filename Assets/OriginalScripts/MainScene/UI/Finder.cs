using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finder : MonoBehaviour
{
    [SerializeField] private GameObject finderChooseBarPanel;//�I���o�[
    [SerializeField] private Goal goal;//Goal
    public bool isFinder;//�T�m�@��I��/�I������

    //�Q�[���J�n���ɑI���o�[���\��
    void Start()
    {
        finderChooseBarPanel.SetActive(false);
    }

    private void Update()
    {
        //�X�e�[�W�N���A���ŁA�u���b�N���󂷎�i���Ȃ������߂ɕK�v
        if (goal.isGoal) OFFFinder();    
    }

    //�N���b�N���ɒT�m�@��I��/�I��������Ԃɂ���
    public void OnClick()
    {
        //�|�[�Y��ʒ��ɒT�m�@��I��/�I��������Ԃ�؂�ւ������Ȃ����߂ɕK�v
        if (Time.timeScale == 0 || goal.isGoal) return;

        if (!isFinder) ONFinder();
        else OFFFinder();
    }

    void ONFinder() 
    {
        isFinder = true;
        finderChooseBarPanel.SetActive(true);
        UIController.instance.toolMode = UIController.ToolMode.Finder;
    }

    public void OFFFinder()
    {
        if (finderChooseBarPanel.activeSelf) 
        {
            isFinder = false;
            finderChooseBarPanel.SetActive(false);
        }         
    }
}
