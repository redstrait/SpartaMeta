using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class PortalSystem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) // Ʈ���ſ� Ÿ ������Ʈ ����
    {
        if (other.CompareTag("Player")) // ������ ������Ʈ�� Player �±׸� ���� �ִ°�
        {
            Debug.Log("��ȣ �ۿ� �ߵ�");

            // ���� ����
        }
    }
}
