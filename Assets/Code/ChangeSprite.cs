using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    public Sprite[] sprites;
    SpriteRenderer sp;

    void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    public void Change()
    {
        int random = Random.Range(0, sprites.Length);
        sp.sprite = sprites[random];
    }
}
