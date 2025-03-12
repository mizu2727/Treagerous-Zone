using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DamageBlock : MonoBehaviour, IPointerClickHandler
{ 
    [SerializeField] private Pickaxe pickaxe;//�s�b�P��
    [SerializeField] private Finder finder;//�T�m�@
    [SerializeField] private int hp;//�u���b�N�̑ϋv��
    [SerializeField] private int damage = 1;//�u���b�N�̃_���[�W��
    [SerializeField] private AudioClip damageBlockBreakSE;//�u���b�N�j��SE

    //�X�v���C�g�n
    private SpriteRenderer spriteRenderer;
    public Sprite damageSprite01;
    public Sprite damageSprite02;
   
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //�s�b�P����I��������ԂŎ��g���N���b�N�����ꍇ�A���g�̑ϋv�͂����炷
        if (pickaxe.isPickaxe && !finder.isFinder && Time.timeScale == 1)
        {  
            hp -= damage;

            switch (hp) 
            {
                case 2:
                    spriteRenderer.sprite = damageSprite01;
                    break;

                case 1:
                    spriteRenderer.sprite = damageSprite02;
                    break;
            }

            //�u���b�N�j�󎞂�SE��炷
            if (hp <= 0)
            {
                GameController.instance.PlayAudioSE(damageBlockBreakSE);
                gameObject.SetActive(false);
            }
        }
    }

    //�T�m�@��I��������ԂŎ��g�ɐG�ꂽ�ꍇ�A���g�̐F��ύX����
    void OnMouseEnter()
    {
        if (finder.isFinder && !pickaxe.isPickaxe) 
        {
            this.GetComponent<SpriteRenderer>().color += new Color(-0.5f, -0.5f, -0.5f, 0);
        }     
    }

    //�T�m�@��I��������ԂŎ��g���痣�ꂽ�ꍇ�A���g�̐F�����ɖ߂�
    void OnMouseExit()
    {
        if (finder.isFinder && !pickaxe.isPickaxe) 
        {
            this.GetComponent<SpriteRenderer>().color += new Color(0.5f, 0.5f, 0.5f, 0);
        }      
    }
}
