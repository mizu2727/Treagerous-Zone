using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollObject : MonoBehaviour
{
    public float startPosition;//スクロール開始地点
    public float endPosition;//スクロール終了地点
    public float speed;//スクロール速度
    public bool isDirection;//スクロールの向きの判定
    private float direction = 1;//スクロールの向きの決定

    private void Start()
    {
        //isDirectionがtrueなら右向き、falseなら左向き
        if (!isDirection) direction = -1;
    }

    void Update()
    {
        //オブジェクトを指定の速度と向きでスクロールさせる
        transform.Translate(speed * Time.deltaTime * direction,0,0);

        //オブジェクトが終了地点までスクロールしたかを判定
        if (transform.position.x <= endPosition) Scroll();
    }

    //オブジェクトの位置を開始地点へ指定する
    void Scroll() 
    {
        float difference = transform.position.x - endPosition;
        Vector3 resetPosition = transform.position;
        resetPosition.x = startPosition + difference;
        transform.position = resetPosition;
    }
}
