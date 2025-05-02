using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowCamera : MonoBehaviour
{
    public Transform target;      // ���� ��� (�÷��̾�)
    public float smoothSpeed = 5f; // �ε巯�� �̵� �ӵ�
    public Vector2 minBounds;     // ī�޶� ������ �� �ִ� �ּ� ��ġ // ������ ���ϴ� ������
    public Vector2 maxBounds;     // ī�޶� ������ �� �ִ� �ִ� ��ġ // ������ ���� ������

    private Vector3 offset;       // ī�޶�� �÷��̾� ���� �ʱ� �Ÿ�

    void Start()
    {
        // �ʱ� �Ÿ� ���� (���� z �ุ -10)
        offset = transform.position - target.position;
    }

    // LateUpdate()�� ����ϴ� ������ ��� ĳ���� �̵��� ���� �Ŀ� ī�޶� ���󰡴� ������ ����� ����
    void LateUpdate()
    {
        // ���󰡾� �� ��ġ ��� (z�� ����)
        Vector3 desiredPosition = target.position + offset;
        desiredPosition.z = transform.position.z;

        // ��ġ ���� ����
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minBounds.x, maxBounds.x);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, minBounds.y, maxBounds.y);

        // �ε巴�� �̵�
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
    }

    private void OnDrawGizmosSelected() // ī�޶� �̵� ���� ������ Scene View���� �ð�ȭ
    {
        Gizmos.color = Color.red;

        Vector3 center = new Vector3((minBounds.x + maxBounds.x) / 2f, (minBounds.y + maxBounds.y) / 2f);
        Vector3 size = new Vector3(maxBounds.x - minBounds.x, maxBounds.y - minBounds.y);

        Gizmos.DrawWireCube(center, size);
    }
}
