using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBall : MonoBehaviour
{
    [SerializeField] private float speed;//�ړ����x
    [SerializeField] private bool isMoveDirection;//�ړ���������Btrue�Ȃ獶�Afalse�Ȃ�E
    private Rigidbody2D rb2D;
    Animator animator;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
    }
    
    //�ړ�
    void Move()
    {
        //��
        if (isMoveDirection)
        {
            rb2D.velocity = new Vector2(Vector2.left.x * speed, rb2D.velocity.y);
            animator.SetTrigger("isSpikeBall-Left");
        }
        //�E
        else 
        {
            rb2D.velocity = new Vector2(Vector2.right.x * speed, rb2D.velocity.y);
            animator.SetTrigger("isSpikeBall-Right");
        }        
    }

    //�ǂɐG�ꂽ��ړ����������ς���
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "LeftCheck") isMoveDirection = true;       

        if (collider.gameObject.tag == "RightCheck") isMoveDirection = false;       
    }

    //�v���C���[���_���[�W���󂯂��Q���ɓ����������\���ɂ���
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player"
            || collision.gameObject.tag == "Trap") 
        {
            enabled = false;
            Destroy(this.gameObject);
        }
    }
}
