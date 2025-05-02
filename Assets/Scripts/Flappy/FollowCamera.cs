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

        // 카메라와 플레이어 사이의 거리를 offsetX에 저장
        offsetX = transform.position.x - target.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;

        Vector3 pos = transform.position; // 카메라 위치
        pos.x = target.position.x + offsetX; // 타겟 위치와 offsetX 만큼 떨어진 거리
        transform.position = pos;
    }
}
