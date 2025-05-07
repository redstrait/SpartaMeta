using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class PortalSystem : MonoBehaviour
{
    public string miniGameSceneName = "FlappyBirdScene"; // ������ �̴ϰ��� �� �̸�
    public GameObject portalGuideUIPopup;  // ��ȣ�ۿ� �ȳ� UI (�����Ϳ��� ����)
    public GameObject npcTalking; // npc ��ȭâ
    public TextMeshProUGUI npcTalkingText; // npc ���
    public GameObject answerUI; // �亯 ������ UI
    public GameObject nextTalkGuide; // ��ȭ �ؽ�Ʈ ��� �Ϸ� & �Է� ��� �ȳ� ������

    private bool isTalking; // ��ȭ ���� üũ
    private float savedSpeed; // npc ��ȭ ��, ���� �÷��̾� �̵� �ӵ� ����� ����
    private int chooseAnswer; // �÷��̾ �� ��ȭ ������

    StatHandler statHandler;
    public StatHandler StatHandler { get { return statHandler; } }

    private void OnTriggerEnter2D(Collider2D other) // Ʈ���ſ� ������Ʈ�� ����
    {
        if (other.CompareTag("Player")) // ������ ������Ʈ�� �±� �˻� - Player�ΰ�?
        {
            portalGuideUIPopup.SetActive(true); // �÷��̾ �����ϸ� �˾� ǥ��
            Debug.Log("��ȣ�ۿ� ���� ���� & �̴� ���� ���� ����");
        }
    }

    private void OnTriggerExit2D(Collider2D other) // Ʈ���ſ� ������Ʈ�� ���� ����
    {
        if (other.CompareTag("Player")) // ������ ���� ������Ʈ�� �±� �˻� - Player�ΰ�?
        {
            portalGuideUIPopup.SetActive(false); // �÷��̾ ����� �˾� ����
            Debug.Log("��ȣ�ۿ� ���� ��Ż & �̴� ���� ���� �Ұ�");
        }
    }

    private void Start()
    {
        statHandler = FindObjectOfType<StatHandler>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isTalking == false) // E Ű �Է� �� & ��ȭ ���� �ƴ� ��
        {
            savedSpeed = statHandler.speed; // ���� �÷��̾� �̵� �ӵ� ����
            statHandler.speed = 0; // �÷��̾� �̵� �ӵ� 0���� ���� - ��ȭ �� ��Ż ����

            portalGuideUIPopup.SetActive(false); // ��ȣ�ۿ� �ȳ� UI ��Ȱ��ȭ - �̹� ��ȣ�ۿ� ���̹Ƿ�

            npcTalking.SetActive(true); // ��ȭâ Ȱ��ȭ
            answerUI.SetActive(true); // �亯 ������â Ȱ��ȭ
            npcTalkingText.text = $"Hello, Are you ready to explore the cave?";

            isTalking = true; // ��ȭ ���� true ó��
            //if (CanEnterMiniGame()) // ���� ���� ���� Ȯ�� �޼��� ȣ�� - true�� ���
            //{
            //    EnterMiniGame(); // �̴� ���� ���� �޼��� ȣ��
            //}
        }

        if (isTalking == true) // ��ȭ ���� ���
        {
            if (answerUI.activeSelf == true) // ������â�� Ȱ��ȭ�� ���
            {
                if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) // 1�� �亯 ���� ��
                {
                    chooseAnswer = 1; // �÷��̾ �� ������ ���� - 1��

                    AfterChooseAnswer();
                    npcTalkingText.text = $"Good Luck, traveler. God bless you.\n- Press the space bar to start the game. -";
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) // 2�� �亯 ���� ��
                {
                    chooseAnswer = 2; // �÷��̾ �� ������ ���� - 2��

                    AfterChooseAnswer();
                    npcTalkingText.text = $"Come back when you feel prepared, traveler.\n- Press the Space bar to end the conversation. -";
                }
            }

            else // ����ġ â�� Ȱ��ȭ���� ���� ���
            {
                if(chooseAnswer == 1 & Input.GetKeyDown(KeyCode.Space)) // �ռ� �� �������� 1���� ���
                {
                    ExitConversation(); // ��ȭâ ��Ȱ��ȭ
                    EnterMiniGame(); // �̴� ���� Scene ����
                }
                else if(chooseAnswer == 2 & Input.GetKeyDown(KeyCode.Space)) // �ռ� �� �������� 2���� ���
                {
                    ExitConversation(); // ��ȭâ ��Ȱ��ȭ
                    AfterConversation(); // ��ȣ�ۿ� ������ Ȱ��ȭ ���� & �̵��ӵ� ����
                }
            }
        }
    }

    private bool CanEnterMiniGame() // �̴� ���� ���� ���� ���� Ȯ��
    {
        // ����: ��ȣ�ۿ� ���� ������ �� �ְ�, UI�� ���� ���� �� ��
        if (portalGuideUIPopup.activeSelf == true) // PortalGuideUIPopup�� Ȱ��ȭ�Ǿ� �ִ� ���
        {
            return true; // ���� ����
        }
        return false; // ���� �Ұ�
    }

    private void EnterMiniGame() // �̴� ���� ���� - �� ����
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(miniGameSceneName);
    }

    private void AfterChooseAnswer() // ��ȭ �� ������ �� ��
    {
        answerUI.SetActive(false); // �亯 ������â ��Ȱ��ȭ
        nextTalkGuide.SetActive(true); // ��� ��� �Ϸ� �� �Է� ��� ������ Ȱ��ȭ
    }

    private void ExitConversation() // ��ȭ ����
    {
        npcTalking.SetActive(false); // ��ȭâ ��Ȱ��ȭ
        nextTalkGuide.SetActive(false); // ��� ��� �Ϸ� & �Է� ��� ������ ��Ȱ��ȭ
    }

    private void AfterConversation() // ��ȭ ���� �� ��ȣ�ۿ� ������ Ȱ��ȭ ���� & �̵��ӵ� ����
    {
        portalGuideUIPopup.SetActive(true); // ��ȣ�ۿ� �ȳ� UI ��Ȱ��ȭ - ���� ��ȣ�ۿ��� �������Ƿ�
        statHandler.speed = savedSpeed; // �÷��̾� �̵� �ӵ� ����
        isTalking = false; // ��ȭ ���� false ó��
    }
}
