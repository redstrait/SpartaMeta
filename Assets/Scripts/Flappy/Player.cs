using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;
    Rigidbody2D _regidbody;
    UIManager uIManager;

    public float flapForce = 6f; // 점프 힘
    public float forwardSpeed = 3f; // 전진 힘
    public static bool isDead = false; // 사망 여부
    float deathCooldown = 0f; // 사망에 필요한 접촉 유지 시간

    bool isFlap = false; // 점프 여부

    public bool godMode = false; // 무적 모드 활성화 여부

    GameManager gameManager;

    private void Awake()
    {
        uIManager = FindObjectOfType<UIManager>(); // uImanager 초기화
    }

    void Start()
    {
        gameManager = GameManager.Instance;

        animator = GetComponentInChildren<Animator>();
        _regidbody = GetComponent<Rigidbody2D>();

        if (animator == null)
        {
            Debug.LogError("Not Founded Animator");
        }

        if (_regidbody == null)
        {
            Debug.LogError("Not Founded Rigidbody");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 일시 정지 상태일 경우
        if(Time.timeScale == 0f)
        {
            // 스페이스바 or 좌클릭 시 게임 시작
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                uIManager.tutorialUI.SetActive(false); // 튜토리얼 UI 비활성화
                Time.timeScale = 1f; // 일시 정지 해제
            }
        }

        if (isDead) // 사망한 경우
        {
            if (deathCooldown <= 0) // deathCooldown이 0 이하라면
            {
                // 스페이스바 or 좌클릭 시 게임 재시작
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    gameManager.RestartGame();
                }
            }
            else // deathCooldown이 0 이하가 아니라면
            {
                deathCooldown -= Time.deltaTime; // 실시간으로 deathCooldown 감소
            }
        }
        else // 생존한 경우
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                isFlap = true; // 점프 여부 활성화
            }
        }
    }

    private void FixedUpdate()
    {
        if (isDead) return;

        Vector3 velocity = _regidbody.velocity; // 가속도 - 물리적으로 받고 있는 힘 호출
        velocity.x = forwardSpeed; // 수평 가속도에 항상 같은 값을 삽입

        if (isFlap) // 점프한 상태라면
        {
            velocity.y += flapForce; // 수직 가속도에 flapForce 더하기
            isFlap = false; // 점프 처리 완료 후 초기화
        }

        // Vector3는 구조체(Struct)이므로 본체의 velocity에 변화를 준 것이 아님
        // 이하는 해당 값을 다시 본체에 넣어주는 작업
        _regidbody.velocity = velocity;

        // =============

        float angle = Mathf.Clamp((_regidbody.velocity.y * 10), -90, 90); // angle 각도 - 값은 다음처럼 제한
        transform.rotation = Quaternion.Euler(0, 0, angle); // z축을 angle값 만큼 회전
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (godMode) return; // 무적 모드가 켜져 있다면 변화 없음

        if (isDead) return; // 이미 사망한 경우라면 변화 없음


        isDead = true; // 사망 처리
        deathCooldown = 1f; // 사망 후 1초 뒤 게임 재시작 가능

        animator.SetInteger("IsDie", 1); // Animator의 isDie 파라미터를 1로 설정
        gameManager.GameOver();
    }
}