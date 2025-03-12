using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePanel : MonoBehaviour
{
    public GameObject[] lifes;//体力のUI 
 
    //プレイヤーの体力が変化した場合、それに応じて体力のUIも変化する
    public void UpdateLife(int hp)
    {
        for (int i = 0; i < lifes.Length; i++)
        {
            if (i < hp) lifes[i].SetActive(true);
            else lifes[i].SetActive(false);
        }
    }
}
