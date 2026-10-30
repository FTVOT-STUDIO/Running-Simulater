using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.score += 20;
            AudioManager.instance.PlaySfx(AudioManager.Sfx.coin);
            gameObject.SetActive(false);
        }
    }
}
