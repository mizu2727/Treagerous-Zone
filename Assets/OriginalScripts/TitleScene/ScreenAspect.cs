using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenAspect : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod]

    //ゲーム開始時にAwake関数より前にこのメソッドを呼び出す
    static void RuntimeMethodLoad()
    {
        // スクリーンサイズを指定
        Screen.SetResolution(1920, 1080, false);
    }
}
