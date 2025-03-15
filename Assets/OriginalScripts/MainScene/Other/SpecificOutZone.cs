using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificOutZone : MonoBehaviour
{
    [SerializeField] bool isTrap;
    private string trapTag = "Trap";

    //���g�ɐG�ꂽ����̃I�u�W�F�N�g���폜����
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(trapTag)  && isTrap) Destroy(other.gameObject);
    }
}
