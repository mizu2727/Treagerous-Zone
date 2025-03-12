using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] objectPrefab;//�����������I�u�W�F�N�g
    [SerializeField] private Vector3[] objectPosition;//�I�u�W�F�N�g�����ʒu
    [SerializeField] private float[] StartTime;//�o�ߎ���
    [SerializeField] private float[] waitTime;//�����Ԋu
    
    void Update()
    {
        for (int i = 0; i < objectPrefab.Length; i++) 
        {           
            StartTime[i] += Time.deltaTime;

            //���Ԋu�Ő�����A�o�ߎ��Ԃ����Z�b�g����
            if (waitTime[i] < StartTime[i])
            {
                GameObject instanceObject = Instantiate(objectPrefab[i]);
                instanceObject.transform.position = objectPosition[i];
                StartTime[i] = 0;
            }
        }      
    }
}
