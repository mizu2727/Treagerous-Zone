using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificOutZone : MonoBehaviour
{
    [SerializeField] bool isTrap;
    private string trapTag = "Trap";

    //自身に触れた特定のオブジェクトを削除する
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(trapTag)  && isTrap) Destroy(other.gameObject);
    }
}
