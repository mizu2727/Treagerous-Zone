using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DamageBlock : MonoBehaviour, IPointerClickHandler
{ 
    [SerializeField] private Pickaxe pickaxe;//ピッケル
    [SerializeField] private Finder finder;//探知機
    [SerializeField] private int hp;//ブロックの耐久力
    [SerializeField] private int damage = 1;//ブロックのダメージ量
    [SerializeField] private AudioClip damageBlockBreakSE;//ブロック破壊時SE

    //スプライト系
    private SpriteRenderer spriteRenderer;
    public Sprite damageSprite01;
    public Sprite damageSprite02;
   
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //ピッケルを選択した状態で自身をクリックした場合、自身の耐久力を減らす
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

            //ブロック破壊時にSEを鳴らす
            if (hp <= 0)
            {
                GameController.instance.PlayAudioSE(damageBlockBreakSE);
                gameObject.SetActive(false);
            }
        }
    }

    //探知機を選択した状態で自身に触れた場合、自身の色を変更する
    void OnMouseEnter()
    {
        if (finder.isFinder && !pickaxe.isPickaxe) 
        {
            this.GetComponent<SpriteRenderer>().color += new Color(-0.5f, -0.5f, -0.5f, 0);
        }     
    }

    //探知機を選択した状態で自身から離れた場合、自身の色を元に戻す
    void OnMouseExit()
    {
        if (finder.isFinder && !pickaxe.isPickaxe) 
        {
            this.GetComponent<SpriteRenderer>().color += new Color(0.5f, 0.5f, 0.5f, 0);
        }      
    }
}
