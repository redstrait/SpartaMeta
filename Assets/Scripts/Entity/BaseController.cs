using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;

    [SerializeField] private SpriteRenderer characterRenderer;

    protected Vector2 movementDirection = Vector2.zero; // 이동 방향
    public Vector2 MovementDirection { get { return movementDirection; } }

    protected Vector2 lookDirection = Vector2.zero; // 시선 방향
    public Vector2 LookDirection { get { return lookDirection; } }

    protected AnimationHandler animationHandler;

    protected StatHandler statHandler;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<AnimationHandler>();
        statHandler = GetComponent<StatHandler>();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        HandleAction();
        Rotate(lookDirection);
    }

    protected virtual void FixedUpdate()
    {
        Movment(movementDirection);
    }

    protected virtual void HandleAction()
    {

    }

    private void Movment(Vector2 direction) // 이동
    {
        direction = direction * statHandler.Speed; // 이동 속도

        _rigidbody.velocity = direction;
        animationHandler.Move(direction); // 애니메이션 호출
    }

    private void Rotate(Vector2 direction) // 시선 회전
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) > 90f; // 90도보다 클 때 왼쪽을 바라봄

        characterRenderer.flipX = isLeft; // 캐릭터 이미지를 왼족으로 회전
    }
}
