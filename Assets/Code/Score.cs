using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    float highScore;
    public bool isHighScore;

    Text UiText;

    void OnEnable()
    {
        UiText.text = GameManager.score.ToString("F0");
    }

    void Awake()
    {
        UiText = GetComponent<Text>();

        if (isHighScore)
        {
            highScore = PlayerPrefs.GetFloat("Score");
            UiText.text = highScore.ToString("F0");
        }
    }

    void LateUpdate()
    {
        if (!GameManager.isLive)
            return;
        if (isHighScore && GameManager.score < highScore)
            return;
        UiText.text = GameManager.score.ToString("F0");
    }
}
