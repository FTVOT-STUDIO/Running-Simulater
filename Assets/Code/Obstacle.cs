using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public GameObject[] coins;

    void Awake()
    {
        int childCount = transform.childCount; // 자식 오브젝트 개수 가져오기
        coins = new GameObject[childCount]; // 배열 초기화

        for (int i = 0; i < childCount; i++)
        {
            coins[i] = transform.GetChild(i).gameObject; // 자식 오브젝트 추가
        }
    }

    public void InitCoin()
    {
        for (int i = 0; i < coins.Length; i++)
        {
            coins[i].SetActive(true);
        }
    }
}
