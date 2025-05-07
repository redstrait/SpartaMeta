using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;
    Rigidbody2D _regidbody;
    UIManager uIManager;

    public float flapForce = 6f; // ���� ��
    public float forwardSpeed = 3f; // ���� ��
    public static bool isDead = false; // ��� ����
    float deathCooldown = 0f; // ����� �ʿ��� ���� ���� �ð�

    bool isFlap = false; // ���� ����

    public bool godMode = false; // ���� ��� Ȱ��ȭ ����

    GameManager gameManager;

    private void Awake()
    {
        uIManager = FindObjectOfType<UIManager>(); // uImanager �ʱ�ȭ
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
        // �Ͻ� ���� ������ ���
        if(Time.timeScale == 0f)
        {
            // �����̽��� or ��Ŭ�� �� ���� ����
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                uIManager.tutorialUI.SetActive(false); // Ʃ�丮�� UI ��Ȱ��ȭ
                Time.timeScale = 1f; // �Ͻ� ���� ����
            }
        }

        if (isDead) // ����� ���
        {
            if (deathCooldown <= 0) // deathCooldown�� 0 ���϶��
            {
                // �����̽��� or ��Ŭ�� �� ���� �����
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    gameManager.RestartGame();
                }
            }
            else // deathCooldown�� 0 ���ϰ� �ƴ϶��
            {
                deathCooldown -= Time.deltaTime; // �ǽð����� deathCooldown ����
            }
        }
        else // ������ ���
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                isFlap = true; // ���� ���� Ȱ��ȭ
            }
        }
    }

    private void FixedUpdate()
    {
        if (isDead) return;

        Vector3 velocity = _regidbody.velocity; // ���ӵ� - ���������� �ް� �ִ� �� ȣ��
        velocity.x = forwardSpeed; // ���� ���ӵ��� �׻� ���� ���� ����

        if (isFlap) // ������ ���¶��
        {
            velocity.y += flapForce; // ���� ���ӵ��� flapForce ���ϱ�
            isFlap = false; // ���� ó�� �Ϸ� �� �ʱ�ȭ
        }

        // Vector3�� ����ü(Struct)�̹Ƿ� ��ü�� velocity�� ��ȭ�� �� ���� �ƴ�
        // ���ϴ� �ش� ���� �ٽ� ��ü�� �־��ִ� �۾�
        _regidbody.velocity = velocity;

        // =============

        float angle = Mathf.Clamp((_regidbody.velocity.y * 10), -90, 90); // angle ���� - ���� ����ó�� ����
        transform.rotation = Quaternion.Euler(0, 0, angle); // z���� angle�� ��ŭ ȸ��
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (godMode) return; // ���� ��尡 ���� �ִٸ� ��ȭ ����

        if (isDead) return; // �̹� ����� ����� ��ȭ ����


        isDead = true; // ��� ó��
        deathCooldown = 1f; // ��� �� 1�� �� ���� ����� ����

        animator.SetInteger("IsDie", 1); // Animator�� isDie �Ķ���͸� 1�� ����
        gameManager.GameOver();
    }
}