using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrollar : MonoBehaviour
{
    public int count;
    public float speed;

    void Start()
    {
        count = transform.childCount;
    }

    void Update()
    {
        if (!GameManager.isLive)
            return;

        float totalSpeed = GameManager.globalSpeed * speed * Time.deltaTime * -1f;
        transform.Translate(totalSpeed, 0, 0);
    }
}
