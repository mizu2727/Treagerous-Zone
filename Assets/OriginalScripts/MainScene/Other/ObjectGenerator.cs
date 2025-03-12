using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] objectPrefab;//生成したいオブジェクト
    [SerializeField] private Vector3[] objectPosition;//オブジェクト生成位置
    [SerializeField] private float[] StartTime;//経過時間
    [SerializeField] private float[] waitTime;//生成間隔
    
    void Update()
    {
        for (int i = 0; i < objectPrefab.Length; i++) 
        {           
            StartTime[i] += Time.deltaTime;

            //一定間隔で生成後、経過時間をリセットする
            if (waitTime[i] < StartTime[i])
            {
                GameObject instanceObject = Instantiate(objectPrefab[i]);
                instanceObject.transform.position = objectPosition[i];
                StartTime[i] = 0;
            }
        }      
    }
}
