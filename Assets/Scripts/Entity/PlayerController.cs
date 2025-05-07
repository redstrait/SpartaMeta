using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController // �κ񿡼��� Player ��ũ��Ʈ
{
    private Camera camera; // ���� ī�޶� ����

    protected override void Start()
    {
        base.Start();
        camera = Camera.main;

        // �̴� ���ӿ����� Player ��� ���� �ʱ�ȭ
        // �̴� ���� Scene���� �ʱ�ȭ ��, ����� ���·� Obstacle Ʈ���ſ� ���� ���̴� Player�� Main Scene �̵� �������� ����� ���� ��ֹ� ����� �ν��� ������ ������ ���� �߻�.
        Player.isDead = false; 
    }

    protected override void HandleAction()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); // �¿� �̵�
        float vertical = Input.GetAxisRaw("Vertical"); // ���� �̵�

        movementDirection = new Vector2(horizontal, vertical).normalized;

        Vector2 mousePosition = Input.mousePosition;
        Vector2 worldPos = camera.ScreenToWorldPoint(mousePosition);

        lookDirection = (worldPos - (Vector2)transform.position);

        if (lookDirection.magnitude < 0.9f) // �ü� ���� ���
        {
            lookDirection = Vector2.zero;
        }
        else
        {
            lookDirection = lookDirection.normalized;
        }
    }
}
