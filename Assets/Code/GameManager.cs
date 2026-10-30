using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    const float ORIGIN_SPEED = 3;
    public static float globalSpeed;
    public static float score;
    public static bool isLive;
    public static bool isStart;
    public static GameObject GameoverUI;
    public GameObject gameOverUI;
    public GameObject[] Texts;

    void Awake()
    {
        isLive = true;
        score = 0;
        if (!PlayerPrefs.HasKey("Score"))
            PlayerPrefs.SetFloat("Score", 0);

        Application.targetFrameRate = 60;

        GameoverUI = gameOverUI;

        score = 0;
    }
    void Update()
    {
        if (isStart)
        {
            score += Time.deltaTime * 2;

        }

        if (isLive)
        {
            globalSpeed = ORIGIN_SPEED + score * 0.001f;
        }
    }

    public static void GameOver()
    {
        isLive = false;
        isStart = false;
        print("뒤짐");
        GameoverUI.SetActive(true);
        //        AudioManager.instance.PlaySfx(AudioManager.Sfx.gameover);

        float highScore = PlayerPrefs.GetFloat("Score");
        PlayerPrefs.SetFloat("Score", Mathf.Max(highScore, score));
        //        AudioManager.instance.StopBgm();
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);

    }

    public void GameStart()
    {

        SceneManager.LoadScene(1);
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
