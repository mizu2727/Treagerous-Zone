using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificOutZone : MonoBehaviour
{
    [SerializeField] bool isTrap;

    //自身に触れた特定のオブジェクトを削除する
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Trap" && isTrap) Destroy(other.gameObject);
    }
}
