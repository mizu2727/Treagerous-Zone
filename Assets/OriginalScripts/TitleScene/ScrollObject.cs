using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollObject : MonoBehaviour
{
    public float startPosition;//�X�N���[���J�n�n�_
    public float endPosition;//�X�N���[���I���n�_
    public float speed;//�X�N���[�����x
    public bool isDirection;//�X�N���[���̌����̔���
    private float direction = 1;//�X�N���[���̌����̌���

    private void Start()
    {
        //isDirection��true�Ȃ�E�����Afalse�Ȃ獶����
        if (!isDirection) direction = -1;
    }

    void Update()
    {
        //�I�u�W�F�N�g���w��̑��x�ƌ����ŃX�N���[��������
        transform.Translate(speed * Time.deltaTime * direction,0,0);

        //�I�u�W�F�N�g���I���n�_�܂ŃX�N���[���������𔻒�
        if (transform.position.x <= endPosition) Scroll();
    }

    //�I�u�W�F�N�g�̈ʒu���J�n�n�_�֎w�肷��
    void Scroll() 
    {
        float difference = transform.position.x - endPosition;
        Vector3 resetPosition = transform.position;
        resetPosition.x = startPosition + difference;
        transform.position = resetPosition;
    }
}
