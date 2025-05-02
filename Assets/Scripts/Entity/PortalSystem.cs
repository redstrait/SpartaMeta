using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class PortalSystem : MonoBehaviour
{
    public string miniGameSceneName = "FlappyBirdScene"; // ������ �̴ϰ��� �� �̸�
    public GameObject PortalGuideUIPopup;  // ��ȣ�ۿ� �ȳ� UI (�����Ϳ��� ����)

    private void OnTriggerEnter2D(Collider2D other) // Ʈ���ſ� ������Ʈ�� ����
    {
        if (other.CompareTag("Player")) // ������ ������Ʈ�� �±� �˻� - Player�ΰ�?
        {
            PortalGuideUIPopup.SetActive(true); // �÷��̾ �����ϸ� �˾� ǥ��
            Debug.Log("��ȣ�ۿ� ���� ���� & �̴� ���� ���� ����");
        }
    }

    private void OnTriggerExit2D(Collider2D other) // Ʈ���ſ� ������Ʈ�� ���� ����
    {
        if (other.CompareTag("Player")) // ������ ���� ������Ʈ�� �±� �˻� - Player�ΰ�?
        {
            PortalGuideUIPopup.SetActive(false); // �÷��̾ ����� �˾� ����
            Debug.Log("��ȣ�ۿ� ���� ��Ż & �̴� ���� ���� �Ұ�");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // �����̽� �� �Է� ��
        {
            if (CanEnterMiniGame()) // ���� ���� ���� Ȯ�� �޼��� ȣ�� - true�� ���
            {
                EnterMiniGame(); // �̴� ���� ���� �޼��� ȣ��
            }
        }
    }

    private bool CanEnterMiniGame() // �̴� ���� ���� ���� ���� Ȯ��
    {
        // ����: ��ȣ�ۿ� ���� ������ �� �ְ�, UI�� ���� ���� �� ��
        if(PortalGuideUIPopup.activeSelf == true) // PortalGuideUIPopup�� Ȱ��ȭ�Ǿ� �ִ� ���
        {
            return true; // ���� ����
        }
        return false; // ���� �Ұ�
    }

    private void EnterMiniGame() // �̴� ���� ���� - �� ����
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(miniGameSceneName);
    }
}
