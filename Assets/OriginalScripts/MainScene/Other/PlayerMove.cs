using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //コンポーネント系
    Rigidbody2D rb2d;
    Animator animator;
    private SpriteRenderer spriteRenderer;
 
    //操作系
    
    enum SituationY// プレイヤーのY軸の状態
    {
        GROUND = 1,
        UP = 2,
        DOWN = 3,
    }

    [SerializeField] private float gravity = 15;//重力
    private float speedX;//X軸の速度
    private float speedY;//Y軸の速度
    [SerializeField] private float speed = 3;//移動速度
    [SerializeField] private float jumpPower = 12.5f;//ジャンプ力
    private float jumpTime = 0;//ジャンプ時間
    [SerializeField] private float jumpLimitTime = 0.6f;//ジャンプできる最大時間       
    bool jumpKey = false; // ジャンプキー
    bool keyLock = false; // キー入力を受け付けない
    Vector2 addVelocity = Vector2.zero;//追加の移動速度(ObjectMove用)

    //体力系
    public int hp;//体力
    private const int maxhp = 3;//最大体力
    private int damage = 1;//ダメージ量
    [SerializeField] private bool isDamage = true;//ダメージ判定
    [SerializeField] private float invincibleTime = 3;//無敵時間
    [SerializeField] private float flashTime = 0.5f;//点滅時間

    //タグ系
    private string outZoneTag = "OutZone";
    private string trapTag = "Trap";
    private string trapFloorTag = "TrapFloor";
    private string moveTrapFloorTag = "MoveTrapFloor";
    private string moveFloorTag = "MoveFloor";
    private string movePlatformTag = "MovePlatform";
    private string floorTag = "Floor";
    private string damageBlockTag = "DamageBlock";
    private string platformTag = "Platform";


    //SE系
    [SerializeField] private AudioClip jumpSE;//ジャンプSE
    [SerializeField] private AudioClip deadSE;//死亡時SE
    [SerializeField] private AudioClip playerDamageSE;//ダメージSE

    //その他
    [SerializeField] private SituationY situationY = SituationY.GROUND;
    [SerializeField] private ContactFilter2D ground;//地面の判定
    [SerializeField] private ContactFilter2D head;//プレイヤーの頭
    [SerializeField] private Goal goal;//Goal
    public bool isDead;//死亡判定
    private ObjectMove objectMove;//ObjectMove
    [SerializeField] private bool isDebug;//デバッグモード

    //体力
    public int HP()
    {
        return hp;
    }

    //死亡判定
    public bool IsDead()
    { 
        return isDead;
    }

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    

    void Update()
    {
        //死亡時、ステージクリア時にプレイヤーの操作が出来ないようにする
        if (isDead)
        {         
            return;
        }
        else if (goal.isGoal)
        {
            rb2d.velocity = new Vector2(0, 0);
            return;
        }
        else 
        {
            speedX = Input.GetAxis("Horizontal");
            animator.SetFloat("RunSpeed", Mathf.Abs(speedX));

            if (Input.GetKey("w") || Input.GetKey("up"))
            {
                jumpKey = !keyLock;
            }
            else
            {
                jumpKey = false;
                keyLock = false;
            }
        }
       
        if (isDebug && Input.GetKeyDown("g")) DebugNoDamage();
        
        if (isDebug && Input.GetKeyDown("h")) DebugLifeUp();

        //体力が最大HPを超えないようにする
        if (maxhp <= hp) hp = maxhp;

        Move();

        var isTouched = GetComponent<Rigidbody2D>().IsTouching(ground);
        var isHeadTouched = GetComponent<Rigidbody2D>().IsTouching(head);
    }

    private void FixedUpdate()
    {
        if (isDead || goal.isGoal) return;

        //プレイヤーのX軸、Y軸の内容(移動するオブジェクトの影響も含める)
        addVelocity = Vector2.zero;
        
        if (objectMove != null) addVelocity = objectMove.GetVelocity();     
        
        rb2d.velocity = new Vector2(speedX, speedY) + addVelocity;

        Jump();
    }

    //ダメージ無効/無効解除切り替え(デバッグ時のみ)
    void DebugNoDamage() 
    {
        if (isDamage)
        {
            isDamage = false;
            spriteRenderer.color = new Color(255, 0, 0, 1);
        }
        else
        {
            isDamage = true;
            spriteRenderer.color = new Color(255, 255, 255, 1);
        }

        Debug.Log("DebugNoDamage().isDamage = " + isDamage);
    }

    //ライフを1つ増やす(デバッグ時のみ)
    void DebugLifeUp() 
    {
        hp++;
        Debug.Log("DebugLifeUp()");
    }

    //横移動
    void Move() 
    {
        //右
        if (Input.GetKey("right") || Input.GetKey("d"))
        {
            speedX = speed;
            this.transform.localScale = new Vector2(4, this.transform.localScale.y);
        }
        //左
        else if (Input.GetKey("left") || Input.GetKey("a"))
        {
            speedX = -speed;
            this.transform.localScale = new Vector2(-4, this.transform.localScale.y);
        }
    }

    //ジャンプ(ジャンプ調整可能)
    void Jump() 
    {
        switch (situationY)
        {
            // 接地時
            case SituationY.GROUND:
                if (jumpKey)
                {
                    speedY = 0;
                    GameController.instance.PlayAudioSE(jumpSE);
                    situationY = SituationY.UP;
                }
                else if (!rb2d.IsTouching(ground))
                {
                    situationY = SituationY.DOWN;
                }
                else
                {
                    if (speedY < gravity) speedY = -gravity;
                    else speed -= gravity;
                }               

                break;

            // 上昇時
            case SituationY.UP:
                jumpTime += Time.deltaTime;

                if (jumpKey)
                {
                    speedY = jumpPower;
                    speedY -= (gravity * Mathf.Pow(jumpTime, jumpLimitTime));
                    //Mathf.Pow(a,b)…aのb乗の値を出力///
                }
                else
                {
                    jumpTime += Time.deltaTime;
                    speedY = jumpPower;
                    speedY -= (gravity * Mathf.Pow(jumpTime, jumpLimitTime));          
                }

                if (speedY < 0)
                {
                    situationY = SituationY.DOWN;
                    speedY = 0;
                    jumpTime = 0.1f;
                }
                break;

            // 落下時
            case SituationY.DOWN:
                jumpTime += Time.deltaTime;
                speedY = 0;
                speedY = -(gravity * Mathf.Pow(jumpTime, jumpLimitTime));

                break;

            default:
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        //奈落判定
        if (collider.gameObject.CompareTag(outZoneTag) || hp <= 0)
        {
            isDead = true;
            GameController.instance.PlayAudioSE(deadSE);
        }
    }

    async void OnCollisionEnter2D(Collision2D collision)
    {
        //ダメージ判定
        if ((collision.gameObject.CompareTag(trapTag) 
            || collision.gameObject.CompareTag(trapFloorTag)
            || collision.gameObject.CompareTag(moveTrapFloorTag)) 
            && goal.isGoal == false && isDead == false && isDamage)
        {

            isDamage = false;
            hp -= damage;           

            if (hp <= 0) 
            {
                isDead = true;
                await Dead();
                return;
            }

            GameController.instance.PlayAudioSE(playerDamageSE);
            gameObject.layer = LayerMask.NameToLayer("PlayerDamage");
            await PlayerDamage(); 
        }

        //ObjectMoveのコンポーネントの取得
        if (situationY == SituationY.GROUND && 
            (collision.gameObject.CompareTag(moveFloorTag)
            || collision.gameObject.CompareTag(movePlatformTag)
            || collision.gameObject.CompareTag(floorTag))) 
        {
           objectMove = collision.gameObject.GetComponent<ObjectMove>();
        }
    }

    private async void OnCollisionStay2D(Collision2D collision)
    {
        //プレイヤーが地面に触れているかを判定する
        if (rb2d.IsTouching(ground) && situationY == SituationY.DOWN && 
            (collision.gameObject.CompareTag(floorTag) 
            || collision.gameObject.CompareTag(damageBlockTag)
            || collision.gameObject.CompareTag(trapFloorTag)
            || collision.gameObject.CompareTag(moveFloorTag)
            || collision.gameObject.CompareTag(platformTag) 
            || collision.gameObject.CompareTag(movePlatformTag)
            || collision.gameObject.CompareTag(moveTrapFloorTag)))
        {
            situationY = SituationY.GROUND;
            speedY = 0;
            jumpTime = 0;
            keyLock = true; // キー操作をロックする
        }

        //プレイヤーの頭が天井に触れているかを判定する
        if (rb2d.IsTouching(head) && situationY == SituationY.UP &&
            (collision.gameObject.CompareTag(floorTag) 
            || collision.gameObject.CompareTag(damageBlockTag)
            || collision.gameObject.CompareTag(trapFloorTag)
            || collision.gameObject.CompareTag(moveFloorTag)
            || collision.gameObject.CompareTag(moveTrapFloorTag)))
        {
            situationY = SituationY.DOWN;
            speedY = -jumpPower;
            jumpTime = 0.1f;
        }

        //ダメージ判定
        if ((collision.gameObject.CompareTag(trapTag)
            || collision.gameObject.CompareTag(trapFloorTag)
            || collision.gameObject.CompareTag(moveTrapFloorTag))
            && goal.isGoal == false && isDead == false && isDamage)
        {
          isDamage = false;
          hp -= damage;

          if (hp <= 0)
          {
              isDead = true;
              await Dead();
              return;
          }
            GameController.instance.PlayAudioSE(playerDamageSE);
            gameObject.layer = LayerMask.NameToLayer("PlayerDamage");
            await PlayerDamage();
            
        }

        //ObjectMoveのコンポーネントの取得
        if (situationY == SituationY.GROUND &&
            (collision.gameObject.CompareTag(moveFloorTag)
            || collision.gameObject.CompareTag(movePlatformTag)
            || collision.gameObject.CompareTag(moveTrapFloorTag)))
        {
            objectMove = collision.gameObject.GetComponent<ObjectMove>();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //移動するオブジェクトの移動速度に影響されない
        if (collision.gameObject.CompareTag(moveFloorTag) 
            || collision.gameObject.CompareTag(movePlatformTag)
            || collision.gameObject.CompareTag(moveTrapFloorTag))
        {
            objectMove = null;
        }
    }

    //ダメージを受けたら一定時間ダメージ判定をオフにする
    async UniTask PlayerDamage()
    {
        Color color = spriteRenderer.color;

        //プレイヤーが一定時間点滅する
        for (int i = 0; i < invincibleTime; i++)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(flashTime));
            spriteRenderer.color = new Color(color.r,color.g,color.b,0);

            await UniTask.Delay(TimeSpan.FromSeconds(flashTime));
            spriteRenderer.color = new Color(color.r,color.g,color.b,1);
        }
        spriteRenderer.color = color;
        isDamage = true;
    }

    //死亡後、プレイヤーを非表示にする
    async UniTask Dead() 
    {
        rb2d.velocity = new Vector2(0, 0);
        animator.Play("Player-Death");
        GameController.instance.PlayAudioSE(deadSE);
        Debug.Log("Dead");
        GameController.instance.GameOver();

        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        Destroy(gameObject);
    }
}
