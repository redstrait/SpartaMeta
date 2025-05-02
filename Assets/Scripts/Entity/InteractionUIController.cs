using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionUIController : MonoBehaviour
{
    public GameObject interactionPopup;  // ��ȣ�ۿ� �ȳ� UI (�����Ϳ��� ����)

    private void OnTriggerEnter2D(Collider2D other) // Ʈ���ſ� ������Ʈ�� ����
    {
        if (other.CompareTag("Player")) // ������ ������Ʈ�� �±� �˻� - Player�ΰ�?
        {
            interactionPopup.SetActive(true); // �÷��̾ �����ϸ� �˾� ǥ��
        }
    }

    private void OnTriggerExit2D(Collider2D other) // Ʈ���ſ� ������Ʈ�� ���� ����
    {
        if (other.CompareTag("Player")) // ������ ���� ������Ʈ�� �±� �˻� - Player�ΰ�?
        {
            interactionPopup.SetActive(false); // �÷��̾ ����� �˾� ����
        }
    }
}
