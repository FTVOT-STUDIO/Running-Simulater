using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundChanger : MonoBehaviour
{
    public Sprite[] backgroundSprites; // 배경 스프라이트 배열
    public float fadeDuration = 1f; // 페이드 인/아웃 지속 시간
    private SpriteRenderer[] childRenderers; // 자식의 SpriteRenderer 배열
    private int currentBackgroundIndex = -1; // 현재 배경 인덱스 (-1로 초기화)
    private int currentScoreRange = -1; // 현재 점수 구간
    private bool isChanging = false; // 배경 변경 중인지 확인

    void Awake()
    {
        // 자식 오브젝트의 SpriteRenderer 가져오기
        childRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    void Update()
    {
        // 현재 점수에 따라 구간 설정 및 배경 변경
        float scoreRange = GetScoreRange((int)GameManager.score);
        if (scoreRange != currentScoreRange && !isChanging)
        {
            currentScoreRange = (int)scoreRange; // 점수 구간 갱신
            ChangeBackground((int)scoreRange); // 배경 변경
        }
    }

    int GetScoreRange(int score)
    {
        // 점수 구간을 반환
        if (score >= 1000) return 0;
        return -1;
    }

    void ChangeBackground(int index)
    {
        if (index < 0 || index >= backgroundSprites.Length) return; // 유효하지 않은 인덱스 무시
        currentBackgroundIndex = index; // 현재 배경 인덱스 갱신

        // 자식의 모든 SpriteRenderer에 대해 페이드 효과 적용
        StartCoroutine(FadeBackground(backgroundSprites[index]));
    }

    IEnumerator FadeBackground(Sprite newBackground)
    {
        isChanging = true;

        // 모든 자식에 대해 페이드 아웃
        foreach (var renderer in childRenderers)
        {
            StartCoroutine(FadeOut(renderer));
        }

        // 페이드 아웃 완료를 기다림
        yield return new WaitForSeconds(fadeDuration);

        // 스프라이트 교체
        foreach (var renderer in childRenderers)
        {
            renderer.sprite = newBackground;
        }

        // 모든 자식에 대해 페이드 인
        foreach (var renderer in childRenderers)
        {
            StartCoroutine(FadeIn(renderer));
        }

        // 페이드 인 완료를 기다림
        yield return new WaitForSeconds(fadeDuration);

        isChanging = false;
    }

    IEnumerator FadeOut(SpriteRenderer renderer)
    {
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(1, 0, t / fadeDuration);
            renderer.color = new Color(1, 1, 1, alpha); // 알파 값 조정
            yield return null;
        }
        renderer.color = new Color(1, 1, 1, 0); // 완전 투명
    }

    IEnumerator FadeIn(SpriteRenderer renderer)
    {
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(0, 1, t / fadeDuration);
            renderer.color = new Color(1, 1, 1, alpha); // 알파 값 조정
            yield return null;
        }
        renderer.color = new Color(1, 1, 1, 1); // 완전 불투명
    }
}
