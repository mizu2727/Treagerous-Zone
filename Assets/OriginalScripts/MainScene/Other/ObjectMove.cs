using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    Rigidbody2D rb2D;
    [SerializeField] private GameObject[] specificPoint;//����̒n�_
    [SerializeField] private float speed;//�i�s���x
    private int nowPoint = 0;//�I�u�W�F�N�g���ʉ߂�������̒n�_�̔ԍ�
    private int nextPoint = 0;//���̓���̒n�_�̔ԍ�
    private bool isReturnPoint = false;//�I�_�Ő܂�Ԃ��Ă��邩�𔻒�Btrue�Ȃ�܂�Ԃ��Đi�s�Afalse�Ȃ�ʏ�i�s
    private Vector2 oldPosition = Vector2.zero;//�I�u�W�F�N�g�̑O�̈ʒu
    private Vector2 myvelocity = Vector2.zero;//�I�u�W�F�N�g�̌��݂̈ʒu
    [SerializeField] private int mode;//�I�_�ɒ��������̏����̐؂�ւ�

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        //se2d = GetComponent<SurfaceEffector2D>();

        //�I�u�W�F�N�g�������ʒu�ɐݒ肷�鏈��
        if (specificPoint != null && 0 < specificPoint.Length
            && rb2D != null) 
        {
            //object��0�Ԗڂ̈ʒu�ɐݒ肷��
            rb2D.position = specificPoint[0].transform.position;
            //�����ʒu���L�^����
            oldPosition = rb2D.position;
        }
    }

    //���݈ʒu�̎擾
    public Vector2 GetVelocity() 
    {
        return myvelocity;
    }

    void FixedUpdate()
    {
        //�I�u�W�F�N�g�������ʒu�̏ꍇ�ɐi�s���J�n����
        if (specificPoint != null && 1 < specificPoint.Length
            && rb2D != null) 
        {
                if (!isReturnPoint) nextPoint = nowPoint + 1;//�ʏ�i�s
                else  nextPoint = nowPoint - 1;//�܂�Ԃ��i�s

                //�ڕW�ł��鎟�̒n�_�܂Ői�s����
                if (0.1f < Vector2.Distance(transform.position, specificPoint[nextPoint].transform.position))
                {
                    //���ݒn���玟�̒n�_�ւ̃x�N�g�����쐬
                    Vector2 toVector
                        = Vector2.MoveTowards(
                            transform.position,
                            specificPoint[nextPoint].transform.position,
                            speed * Time.deltaTime);

                    //���̒n�_�܂ł��쐬���ꂽ�x�N�g���ɉ����Đi�s
                    rb2D.MovePosition(toVector);
                }
                else
                {
                    //���̒n�_�ɒ������ۂɖڕW�̒n�_��1��֕ύX
                    rb2D.MovePosition(specificPoint[nextPoint].transform.position);
                    if (!isReturnPoint) ++nowPoint;
                    else --nowPoint;

                    //�I�_�ɒ������ꍇ�̏���
                    if (specificPoint.Length <= nowPoint + 1 && !isReturnPoint)
                    {
                        switch (mode)
                        {
                            //�폜
                            case 1:
                                enabled = false;
                                Destroy(this.gameObject);
                                break;

                            //�܂�Ԃ�
                            case 2:
                                isReturnPoint = true;
                                break;

                            default:
                                break;
                        }
                    }
                    //�n�_�ɒ������ꍇ�̏���
                    else if (nowPoint <= 0) isReturnPoint = false;
                }
        }

        //�I�u�W�F�N�g�̐i�񂾋����̎擾
        myvelocity = (rb2D.position - oldPosition) / Time.deltaTime;

        //�O�̈ʒu���L�^����
        oldPosition = rb2D.position;

    }
}
