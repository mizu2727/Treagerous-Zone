using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //�R���|�[�l���g�n
    Rigidbody2D rb2d;
    Animator animator;
    private SpriteRenderer spriteRenderer;
 
    //����n
    
    enum SituationY// �v���C���[��Y���̏��
    {
        GROUND = 1,
        UP = 2,
        DOWN = 3,
    }

    [SerializeField] private float gravity = 15;//�d��
    private float speedX;//X���̑��x
    private float speedY;//Y���̑��x
    [SerializeField] private float speed = 3;//�ړ����x
    [SerializeField] private float jumpPower = 12.5f;//�W�����v��
    private float jumpTime = 0;//�W�����v����
    [SerializeField] private float jumpLimitTime = 0.6f;//�W�����v�ł���ő厞��       
    bool jumpKey = false; // �W�����v�L�[
    bool keyLock = false; // �L�[���͂��󂯕t���Ȃ�
    Vector2 addVelocity = Vector2.zero;//�ǉ��̈ړ����x(ObjectMove�p)

    //�̗͌n
    public int hp;//�̗�
    private const int maxhp = 3;//�ő�̗�
    private int damage = 1;//�_���[�W��
    [SerializeField] private bool isDamage = true;//�_���[�W����
    [SerializeField] private float invincibleTime = 3;//���G����
    [SerializeField] private float flashTime = 0.5f;//�_�Ŏ���

    //�^�O�n
    private string outZoneTag = "OutZone";
    private string trapTag = "Trap";
    private string trapFloorTag = "TrapFloor";
    private string moveTrapFloorTag = "MoveTrapFloor";
    private string moveFloorTag = "MoveFloor";
    private string movePlatformTag = "MovePlatform";
    private string floorTag = "Floor";
    private string damageBlockTag = "DamageBlock";
    private string platformTag = "Platform";


    //SE�n
    [SerializeField] private AudioClip jumpSE;//�W�����vSE
    [SerializeField] private AudioClip deadSE;//���S��SE
    [SerializeField] private AudioClip playerDamageSE;//�_���[�WSE

    //���̑�
    [SerializeField] private SituationY situationY = SituationY.GROUND;
    [SerializeField] private ContactFilter2D ground;//�n�ʂ̔���
    [SerializeField] private ContactFilter2D head;//�v���C���[�̓�
    [SerializeField] private Goal goal;//Goal
    public bool isDead;//���S����
    private ObjectMove objectMove;//ObjectMove
    [SerializeField] private bool isDebug;//�f�o�b�O���[�h

    //�̗�
    public int HP()
    {
        return hp;
    }

    //���S����
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
        //���S���A�X�e�[�W�N���A���Ƀv���C���[�̑��삪�o���Ȃ��悤�ɂ���
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

        //�̗͂��ő�HP�𒴂��Ȃ��悤�ɂ���
        if (maxhp <= hp) hp = maxhp;

        Move();

        var isTouched = GetComponent<Rigidbody2D>().IsTouching(ground);
        var isHeadTouched = GetComponent<Rigidbody2D>().IsTouching(head);
    }

    private void FixedUpdate()
    {
        if (isDead || goal.isGoal) return;

        //�v���C���[��X���AY���̓��e(�ړ�����I�u�W�F�N�g�̉e�����܂߂�)
        addVelocity = Vector2.zero;
        
        if (objectMove != null) addVelocity = objectMove.GetVelocity();     
        
        rb2d.velocity = new Vector2(speedX, speedY) + addVelocity;

        Jump();
    }

    //�_���[�W����/���������؂�ւ�(�f�o�b�O���̂�)
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

    //���C�t��1���₷(�f�o�b�O���̂�)
    void DebugLifeUp() 
    {
        hp++;
        Debug.Log("DebugLifeUp()");
    }

    //���ړ�
    void Move() 
    {
        //�E
        if (Input.GetKey("right") || Input.GetKey("d"))
        {
            speedX = speed;
            this.transform.localScale = new Vector2(4, this.transform.localScale.y);
        }
        //��
        else if (Input.GetKey("left") || Input.GetKey("a"))
        {
            speedX = -speed;
            this.transform.localScale = new Vector2(-4, this.transform.localScale.y);
        }
    }

    //�W�����v(�W�����v�����\)
    void Jump() 
    {
        switch (situationY)
        {
            // �ڒn��
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

            // �㏸��
            case SituationY.UP:
                jumpTime += Time.deltaTime;

                if (jumpKey)
                {
                    speedY = jumpPower;
                    speedY -= (gravity * Mathf.Pow(jumpTime, jumpLimitTime));
                    //Mathf.Pow(a,b)�ca��b��̒l���o��///
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

            // ������
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
        //�ޗ�����
        if (collider.gameObject.CompareTag(outZoneTag) || hp <= 0)
        {
            isDead = true;
            GameController.instance.PlayAudioSE(deadSE);
        }
    }

    async void OnCollisionEnter2D(Collision2D collision)
    {
        //�_���[�W����
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

        //ObjectMove�̃R���|�[�l���g�̎擾
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
        //�v���C���[���n�ʂɐG��Ă��邩�𔻒肷��
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
            keyLock = true; // �L�[��������b�N����
        }

        //�v���C���[�̓����V��ɐG��Ă��邩�𔻒肷��
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

        //�_���[�W����
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

        //ObjectMove�̃R���|�[�l���g�̎擾
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
        //�ړ�����I�u�W�F�N�g�̈ړ����x�ɉe������Ȃ�
        if (collision.gameObject.CompareTag(moveFloorTag) 
            || collision.gameObject.CompareTag(movePlatformTag)
            || collision.gameObject.CompareTag(moveTrapFloorTag))
        {
            objectMove = null;
        }
    }

    //�_���[�W���󂯂����莞�ԃ_���[�W������I�t�ɂ���
    async UniTask PlayerDamage()
    {
        Color color = spriteRenderer.color;

        //�v���C���[����莞�ԓ_�ł���
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

    //���S��A�v���C���[���\���ɂ���
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
