using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeObject : MonoBehaviour
{
    public GameObject[] objs;

    public void Change()
    {
        int random = Random.Range(0, objs.Length);

        for (int i = 0; i < objs.Length; i++)
        {
            transform.GetChild(i).gameObject.SetActive(random == i);
        }
    }
}
