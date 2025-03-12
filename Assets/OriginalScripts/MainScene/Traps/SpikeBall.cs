using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBall : MonoBehaviour
{
    [SerializeField] private float speed;//移動速度
    [SerializeField] private bool isMoveDirection;//移動する向き。trueなら左、falseなら右
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
    
    //移動
    void Move()
    {
        //左
        if (isMoveDirection)
        {
            rb2D.velocity = new Vector2(Vector2.left.x * speed, rb2D.velocity.y);
            animator.SetTrigger("isSpikeBall-Left");
        }
        //右
        else 
        {
            rb2D.velocity = new Vector2(Vector2.right.x * speed, rb2D.velocity.y);
            animator.SetTrigger("isSpikeBall-Right");
        }        
    }

    //壁に触れたら移動する向きを変える
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "LeftCheck") isMoveDirection = true;       

        if (collider.gameObject.tag == "RightCheck") isMoveDirection = false;       
    }

    //プレイヤーかダメージを受ける障害物に当たったら非表示にする
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
