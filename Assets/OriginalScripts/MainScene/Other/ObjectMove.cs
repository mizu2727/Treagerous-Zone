using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    Rigidbody2D rb2D;
    [SerializeField] private GameObject[] specificPoint;//特定の地点
    [SerializeField] private float speed;//進行速度
    private int nowPoint = 0;//オブジェクトが通過した特定の地点の番号
    private int nextPoint = 0;//次の特定の地点の番号
    private bool isReturnPoint = false;//終点で折り返しているかを判定。trueなら折り返して進行、falseなら通常進行
    private Vector2 oldPosition = Vector2.zero;//オブジェクトの前の位置
    private Vector2 myvelocity = Vector2.zero;//オブジェクトの現在の位置
    [SerializeField] private int mode;//終点に着いた時の処理の切り替え

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        //se2d = GetComponent<SurfaceEffector2D>();

        //オブジェクトを初期位置に設定する処理
        if (specificPoint != null && 0 < specificPoint.Length
            && rb2D != null) 
        {
            //objectを0番目の位置に設定する
            rb2D.position = specificPoint[0].transform.position;
            //初期位置を記録する
            oldPosition = rb2D.position;
        }
    }

    //現在位置の取得
    public Vector2 GetVelocity() 
    {
        return myvelocity;
    }

    void FixedUpdate()
    {
        //オブジェクトが初期位置の場合に進行を開始する
        if (specificPoint != null && 1 < specificPoint.Length
            && rb2D != null) 
        {
                if (!isReturnPoint) nextPoint = nowPoint + 1;//通常進行
                else  nextPoint = nowPoint - 1;//折り返し進行

                //目標である次の地点まで進行する
                if (0.1f < Vector2.Distance(transform.position, specificPoint[nextPoint].transform.position))
                {
                    //現在地から次の地点へのベクトルを作成
                    Vector2 toVector
                        = Vector2.MoveTowards(
                            transform.position,
                            specificPoint[nextPoint].transform.position,
                            speed * Time.deltaTime);

                    //次の地点までを作成されたベクトルに沿って進行
                    rb2D.MovePosition(toVector);
                }
                else
                {
                    //次の地点に着いた際に目標の地点を1つ先へ変更
                    rb2D.MovePosition(specificPoint[nextPoint].transform.position);
                    if (!isReturnPoint) ++nowPoint;
                    else --nowPoint;

                    //終点に着いた場合の処理
                    if (specificPoint.Length <= nowPoint + 1 && !isReturnPoint)
                    {
                        switch (mode)
                        {
                            //削除
                            case 1:
                                enabled = false;
                                Destroy(this.gameObject);
                                break;

                            //折り返す
                            case 2:
                                isReturnPoint = true;
                                break;

                            default:
                                break;
                        }
                    }
                    //始点に着いた場合の処理
                    else if (nowPoint <= 0) isReturnPoint = false;
                }
        }

        //オブジェクトの進んだ距離の取得
        myvelocity = (rb2D.position - oldPosition) / Time.deltaTime;

        //前の位置を記録する
        oldPosition = rb2D.position;

    }
}
