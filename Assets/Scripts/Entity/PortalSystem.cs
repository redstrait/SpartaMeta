using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class PortalSystem : MonoBehaviour
{
    public string miniGameSceneName = "FlappyBirdScene"; // 진입할 미니게임 씬 이름
    public GameObject PortalGuideUIPopup;  // 상호작용 안내 UI (에디터에서 연결)

    private void OnTriggerEnter2D(Collider2D other) // 트리거와 오브젝트의 접촉
    {
        if (other.CompareTag("Player")) // 접촉한 오브젝트의 태그 검사 - Player인가?
        {
            PortalGuideUIPopup.SetActive(true); // 플레이어가 접근하면 팝업 표시
            Debug.Log("상호작용 영역 진입 & 미니 게임 진입 가능");
        }
    }

    private void OnTriggerExit2D(Collider2D other) // 트리거와 오브젝트의 접촉 해제
    {
        if (other.CompareTag("Player")) // 접촉이 끊긴 오브젝트의 태그 검사 - Player인가?
        {
            PortalGuideUIPopup.SetActive(false); // 플레이어가 벗어나면 팝업 숨김
            Debug.Log("상호작용 영역 이탈 & 미니 게임 진입 불가");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // 스페이스 바 입력 시
        {
            if (CanEnterMiniGame()) // 진입 가능 여부 확인 메서드 호출 - true일 경우
            {
                EnterMiniGame(); // 미니 게임 실행 메서드 호출
            }
        }
    }

    private bool CanEnterMiniGame() // 미니 게임 진입 가능 여부 확인
    {
        // 예시: 상호작용 가능 영역에 들어가 있고, UI가 켜져 있을 때 등
        if(PortalGuideUIPopup.activeSelf == true) // PortalGuideUIPopup가 활성화되어 있는 경우
        {
            return true; // 진입 가능
        }
        return false; // 진입 불가
    }

    private void EnterMiniGame() // 미니 게임 실행 - 씬 진입
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(miniGameSceneName);
    }
}
