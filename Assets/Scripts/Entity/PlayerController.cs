using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController // 로비에서의 Player 스크립트
{
    private Camera camera; // 메인 카메라 참조

    protected override void Start()
    {
        base.Start();
        camera = Camera.main;

        // 미니 게임에서의 Player 사망 여부 초기화
        // 미니 게임 Scene에서 초기화 시, 사망한 생태로 Obstacle 트리거에 접촉 중이던 Player가 Main Scene 이동 과정에서 사라진 것을 장애물 통과로 인식해 점수가 오르는 현상 발생.
        Player.isDead = false; 
    }

    protected override void HandleAction()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); // 좌우 이동
        float vertical = Input.GetAxisRaw("Vertical"); // 상하 이동

        movementDirection = new Vector2(horizontal, vertical).normalized;

        Vector2 mousePosition = Input.mousePosition;
        Vector2 worldPos = camera.ScreenToWorldPoint(mousePosition);

        lookDirection = (worldPos - (Vector2)transform.position);

        if (lookDirection.magnitude < 0.9f) // 시선 방향 계산
        {
            lookDirection = Vector2.zero;
        }
        else
        {
            lookDirection = lookDirection.normalized;
        }
    }
}
