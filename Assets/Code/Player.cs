using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public float jumpPower;
    public float longJumpPower;
    bool isJumpKey;
    public bool isGround;
    public bool isLive;
    public int jumpCount;
    public int maxJumpCount = 1;


    Rigidbody2D rigid;
    Animator anim;
    Collider2D coll;
    public GameObject jumpColl;
    SpriteRenderer sp;

    void Awake()
    {

        sp = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Invoke("GameStart", 6);
        StartCoroutine(MovePlayerToStartPosition());

    }

    void Start()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.dog);
        AudioManager.instance.PlayBgm(AudioManager.Bgm.MainTheme0);
    }

    void GameStart()
    {
        GameManager.isStart = true;
    }
    IEnumerator MovePlayerToStartPosition()
    {
        float startX = transform.position.x;  // 현재 X 위치
        float endX = -3f;                      // 목표 X 위치
        float speed = 2f;                      // 이동 속도
        float journeyLength = Mathf.Abs(endX - startX); // 이동할 거리

        float startTime = Time.time;

        while (Mathf.Abs(transform.position.x - endX) > 0.01f)
        {
            float distanceCovered = (Time.time - startTime) * speed;  // 현재까지 이동한 거리
            float fractionOfJourney = distanceCovered / journeyLength;  // 이동 비율
            transform.position = new Vector3(Mathf.Lerp(startX, endX, fractionOfJourney), transform.position.y, transform.position.z);
            yield return null;  // 프레임마다 업데이트
        }

        transform.position = new Vector3(endX, transform.position.y, transform.position.z);  // 정확하게 목표 위치에 도달하도록 보정
    }
    void Update()
    {
        if (!GameManager.isLive)
            return;

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && jumpCount < maxJumpCount)
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jumpCount++;
        }

        isJumpKey = Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0);
    }

    void FixedUpdate()
    {
        if (!GameManager.isLive)
            return;

        if (isJumpKey && !isGround)
        {
            longJumpPower = Mathf.Lerp(longJumpPower, 0, 0.1f);
            rigid.AddForce(Vector2.up * longJumpPower, ForceMode2D.Impulse);

        }
    }

    void OnCollisionStay2D(Collision2D other)
    {

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isGround = true;
            anim.SetBool("isGround", true);
            longJumpPower = 2;
            jumpCount = 0;
        }


    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            AudioManager.instance.PlaySfx(AudioManager.Sfx.falling);
            isGround = true;
            anim.SetBool("isGround", true);
            longJumpPower = 2;
        }


        if (other.gameObject.CompareTag("Obstacle"))
        {
            StartCoroutine(OnDead());

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            AudioManager.instance.PlaySfx(AudioManager.Sfx.jump);
            isGround = false;
            anim.SetBool("isGround", false);
        }

    }



    IEnumerator OnDead()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.dead);
        AudioManager.instance.StopBgm();
        rigid.AddForce(Vector2.up * 12, ForceMode2D.Impulse);
        anim.SetTrigger("OnHtt");
        coll.enabled = false;
        jumpColl.SetActive(false);
        sp.flipY = true;
        GameManager.isStart = false;
        GameManager.isLive = false;
        yield return new WaitForSeconds(1.5f);
        GameManager.GameOver();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.gameover);
    }
}
