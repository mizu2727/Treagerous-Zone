using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificOutZone : MonoBehaviour
{
    [SerializeField] bool isTrap;

    //���g�ɐG�ꂽ����̃I�u�W�F�N�g���폜����
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Trap" && isTrap) Destroy(other.gameObject);
    }
}
