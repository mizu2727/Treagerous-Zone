using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe : MonoBehaviour
{
    [SerializeField] private GameObject pickaxeChooseBarPanel;//�I���o�[
    [SerializeField] private Goal goal;//Goal
    public bool isPickaxe;//�s�b�P����I��/�I������

    //�Q�[���J�n���ɑI���o�[���\��
    void Start()
    {       
        pickaxeChooseBarPanel.SetActive(false);
    }

    private void Update()
    {
        //�X�e�[�W�N���A���ŁA�u���b�N���󂷎�i���Ȃ������߂ɕK�v
        if (goal.isGoal) OFFPickaxe();
    }

    //�N���b�N���Ƀs�b�P����I��/�I��������Ԃɂ���
    public void OnClick()
    {
        //�|�[�Y��ʒ��Ƀs�b�P����I��/�I��������Ԃ�؂�ւ������Ȃ����߂ɕK�v
        if (Time.timeScale == 0 || goal.isGoal) return;
       
        if (!isPickaxe) OnPickaxe();
        else OFFPickaxe();
        //Debug.Log("isPickaxe =" + isPickaxe);
    }

    void OnPickaxe() 
    {
        isPickaxe = true;
        pickaxeChooseBarPanel.SetActive(true);
        UIController.instance.toolMode = UIController.ToolMode.Pickaxe;
    }

    public void OFFPickaxe()
    {
        if (pickaxeChooseBarPanel.activeSelf)
        {
            isPickaxe = false;
            pickaxeChooseBarPanel.SetActive(false);
        }   
    }
}
