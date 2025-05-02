using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class PortalSystem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) // 트리거와 타 오브젝트 접촉
    {
        if (other.CompareTag("Player")) // 접촉한 오브젝트가 Player 태그를 갖고 있는가
        {
            Debug.Log("상호 작용 발동");

            // 게임 실행
        }
    }
}
