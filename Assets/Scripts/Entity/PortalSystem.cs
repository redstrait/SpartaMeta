using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class PortalSystem : MonoBehaviour
{
    public string miniGameSceneName = "FlappyBirdScene"; // 진입할 미니게임 씬 이름
    public GameObject portalGuideUIPopup;  // 상호작용 안내 UI (에디터에서 연결)
    public GameObject npcTalking; // npc 대화창
    public TextMeshProUGUI npcTalkingText; // npc 대사
    public GameObject answerUI; // 답변 선택지 UI
    public GameObject nextTalkGuide; // 대화 텍스트 출력 완료 & 입력 대기 안내 아이콘

    private bool isTalking; // 대화 여부 체크
    private float savedSpeed; // npc 대화 시, 기존 플레이어 이동 속도 저장용 변수
    private int chooseAnswer; // 플레이어가 고른 대화 선택지

    StatHandler statHandler;
    public StatHandler StatHandler { get { return statHandler; } }

    private void OnTriggerEnter2D(Collider2D other) // 트리거와 오브젝트의 접촉
    {
        if (other.CompareTag("Player")) // 접촉한 오브젝트의 태그 검사 - Player인가?
        {
            portalGuideUIPopup.SetActive(true); // 플레이어가 접근하면 팝업 표시
            Debug.Log("상호작용 영역 진입 & 미니 게임 진입 가능");
        }
    }

    private void OnTriggerExit2D(Collider2D other) // 트리거와 오브젝트의 접촉 해제
    {
        if (other.CompareTag("Player")) // 접촉이 끊긴 오브젝트의 태그 검사 - Player인가?
        {
            portalGuideUIPopup.SetActive(false); // 플레이어가 벗어나면 팝업 숨김
            Debug.Log("상호작용 영역 이탈 & 미니 게임 진입 불가");
        }
    }

    private void Start()
    {
        statHandler = FindObjectOfType<StatHandler>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isTalking == false) // E 키 입력 시 & 대화 중이 아닐 시
        {
            savedSpeed = statHandler.speed; // 기존 플레이어 이동 속도 저장
            statHandler.speed = 0; // 플레이어 이동 속도 0으로 설정 - 대화 중 이탈 차단

            portalGuideUIPopup.SetActive(false); // 상호작용 안내 UI 비활성화 - 이미 상호작용 중이므로

            npcTalking.SetActive(true); // 대화창 활성화
            answerUI.SetActive(true); // 답변 선택지창 활성화
            npcTalkingText.text = $"Hello, Are you ready to explore the cave?";

            isTalking = true; // 대화 여부 true 처리
            //if (CanEnterMiniGame()) // 진입 가능 여부 확인 메서드 호출 - true일 경우
            //{
            //    EnterMiniGame(); // 미니 게임 실행 메서드 호출
            //}
        }

        if (isTalking == true) // 대화 중일 경우
        {
            if (answerUI.activeSelf == true) // 선택지창이 활성화된 경우
            {
                if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) // 1번 답변 선택 시
                {
                    chooseAnswer = 1; // 플레이어가 고른 선택지 저장 - 1번

                    AfterChooseAnswer();
                    npcTalkingText.text = $"Good Luck, traveler. God bless you.\n- Press the space bar to start the game. -";
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) // 2번 답변 선택 시
                {
                    chooseAnswer = 2; // 플레이어가 고른 선택지 저장 - 2번

                    AfterChooseAnswer();
                    npcTalkingText.text = $"Come back when you feel prepared, traveler.\n- Press the Space bar to end the conversation. -";
                }
            }

            else // 선택치 창이 활성화되지 않은 경우
            {
                if(chooseAnswer == 1 & Input.GetKeyDown(KeyCode.Space)) // 앞서 고른 선택지가 1번일 경우
                {
                    ExitConversation(); // 대화창 비활성화
                    EnterMiniGame(); // 미니 게임 Scene 진입
                }
                else if(chooseAnswer == 2 & Input.GetKeyDown(KeyCode.Space)) // 앞서 고른 선택지가 2번일 경우
                {
                    ExitConversation(); // 대화창 비활성화
                    AfterConversation(); // 상호작용 아이콘 활성화 여부 & 이동속도 복구
                }
            }
        }
    }

    private bool CanEnterMiniGame() // 미니 게임 진입 가능 여부 확인
    {
        // 예시: 상호작용 가능 영역에 들어가 있고, UI가 켜져 있을 때 등
        if (portalGuideUIPopup.activeSelf == true) // PortalGuideUIPopup가 활성화되어 있는 경우
        {
            return true; // 진입 가능
        }
        return false; // 진입 불가
    }

    private void EnterMiniGame() // 미니 게임 실행 - 씬 진입
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(miniGameSceneName);
    }

    private void AfterChooseAnswer() // 대화 중 선택지 고른 후
    {
        answerUI.SetActive(false); // 답변 선택지창 비활성화
        nextTalkGuide.SetActive(true); // 대사 출력 완료 및 입력 대기 아이콘 활성화
    }

    private void ExitConversation() // 대화 종료
    {
        npcTalking.SetActive(false); // 대화창 비활성화
        nextTalkGuide.SetActive(false); // 대사 출력 완료 & 입력 대기 아이콘 비활성화
    }

    private void AfterConversation() // 대화 종료 후 상호작용 아이콘 활성화 여부 & 이동속도 복구
    {
        portalGuideUIPopup.SetActive(true); // 상호작용 안내 UI 재활성화 - 기존 상호작용이 끝났으므로
        statHandler.speed = savedSpeed; // 플레이어 이동 속도 복구
        isTalking = false; // 대화 여부 false 처리
    }
}
