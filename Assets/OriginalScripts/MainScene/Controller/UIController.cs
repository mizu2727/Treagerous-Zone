using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    //�X�e�[�W�N���A�n
    [SerializeField] GameObject clearPanel;//�N���A�p�l��//
    [SerializeField] private GameObject clearText;//�N���A�e�L�X�g//
    private float clearPanelAlpha = 0;//�N���A�p�l���̓��ߓx//
    [SerializeField] private float clearPanelAlphaSpeed = 0.01f;//�N���A�p�l���̓��ߑ��x//
    [SerializeField] private GameObject returnToStageSelectButton;//�X�e�[�W�I���֖߂�{�^��//
    [SerializeField] private GameObject returnToTitleButton;//�^�C�g���֖߂�{�^��//

    //���̑�
    public static UIController instance;//�C���X�^���X��
    int buildIndex;//�V�[���̃r���h�ԍ�
    [SerializeField] private Pickaxe pickaxe;//�s�b�P��
    [SerializeField] private Finder finder;//�T�m�@
    [SerializeField] private PlayerMove player;//�v���C���[
    public LifePanel lifePanel;//�v���C���[�̗̑�//
    public int redCoinCount = 0;//�ԃR�C���̎擾����
    [SerializeField] private GameObject tooBadText;//�v���C���[���S���̃e�L�X�g

    public enum ToolMode//UI�̑I����� 
    {
        OFF,
        Pickaxe,
        Finder
    }

    public ToolMode toolMode = ToolMode.OFF;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);

        buildIndex = SceneManager.GetActiveScene().buildIndex;

        //�`���[�g���A���E�X�e�[�W1�̂݁A�T�m�@���\��
        if (buildIndex == 2 || buildIndex == 3) Destroy(finder.gameObject);

        ////�v���C���[���S���E�X�e�[�W�N���A����UI���Q�[���J�n���̏�Ԃɂ���
        tooBadText.SetActive(false);
        clearPanel.SetActive(false);
        clearPanel.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        clearText.SetActive(false);
        returnToStageSelectButton.SetActive(false);
        returnToTitleButton.SetActive(false);
    }

    //UI�̃I��/�I�t�̐؂�ւ�
    void Update()
    {
        if(!pickaxe.isPickaxe && !finder.isFinder) toolMode = ToolMode.OFF;

        switch (toolMode) 
        {
            case ToolMode.OFF:
                pickaxe.OFFPickaxe();
                finder.OFFFinder();
                break;

            case ToolMode.Pickaxe:
                finder.OFFFinder();
                break;

            case ToolMode.Finder:
                pickaxe.OFFPickaxe();
                break;
        }
    }

    //�N���A�e�L�X�g���X�e�[�W�I���֖߂�{�^���A�^�C�g���֖߂�{�^���̏��ɕ\������
    public IEnumerator Result()
    {
        //cearPanel�̕`�揇�Ԃ���Ԏ�O�ɂ��āA����UI���N���A�p�l���ŉB�����߂ɕK�v
        clearPanel.transform.SetAsLastSibling();
        clearPanel.SetActive(true);


        //�N���A�p�l���̔w�i�����X�ɈÂ�����
        clearPanel.GetComponent<Image>().color = new Color(0, 0, 0, clearPanelAlpha);
        clearPanelAlpha += clearPanelAlphaSpeed;

        yield return new WaitForSeconds(3);
        clearText.SetActive(true);

        yield return new WaitForSeconds(2);
        returnToStageSelectButton.SetActive(true);
        returnToTitleButton.SetActive(true);
    }

    //�v���C���[���S���̃e�L�X�g��\�����V�[�������[�h���ă��X�^�[�g�̏��ŏ�������
    public IEnumerator Retry()
    {
        yield return new WaitForSeconds(0.5f);
        tooBadText.SetActive(true);

        yield return new WaitForSeconds(2.5f);
        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.name);
    }

    //�v���C���[�̃��C�t�̏��
    public void PlayerLife() 
    {     
        lifePanel.UpdateLife(player.HP());
    }
}
