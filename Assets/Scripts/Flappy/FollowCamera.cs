using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    float offsetX;


    // Start is called before the first frame update
    void Start()
    {
        if (target == null)
            return;

        // ī�޶�� �÷��̾� ������ �Ÿ��� offsetX�� ����
        offsetX = transform.position.x - target.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;

        Vector3 pos = transform.position; // ī�޶� ��ġ
        pos.x = target.position.x + offsetX; // Ÿ�� ��ġ�� offsetX ��ŭ ������ �Ÿ�
        transform.position = pos;
    }
}
