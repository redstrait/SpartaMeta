using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionUIController : MonoBehaviour
{
    public GameObject interactionPopup;  // 상호작용 안내 UI (에디터에서 연결)

    private void OnTriggerEnter2D(Collider2D other) // 트리거와 오브젝트의 접촉
    {
        if (other.CompareTag("Player")) // 접촉한 오브젝트의 태그 검사 - Player인가?
        {
            interactionPopup.SetActive(true); // 플레이어가 접근하면 팝업 표시
        }
    }

    private void OnTriggerExit2D(Collider2D other) // 트리거와 오브젝트의 접촉 해제
    {
        if (other.CompareTag("Player")) // 접촉이 끊긴 오브젝트의 태그 검사 - Player인가?
        {
            interactionPopup.SetActive(false); // 플레이어가 벗어나면 팝업 숨김
        }
    }
}
