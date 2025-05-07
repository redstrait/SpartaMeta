using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager;
    public static GameManager Instance { get { return gameManager; } }

    private int currentScore = 0; // ���� ����

    int bestScore = 0; // �ְ� ����
    public int BestScore { get => bestScore; }
    private const string BestScoreKey = "BestScore"; // PlayerPrefs Ű

    int prevBestScore = 0; // ���� ���� ������ �ְ� ���� - UIManager���� �ְ� ������ ���ŵǾ����� Ȯ�ο�
    public int PrevBestScore { get => bestScore; }
    private const string PrevBestScoreKey = "PrevBestScore"; // PlayerPrefs Ű

    int lastScore = 0; // ���������� �÷����� ������ ����
    public int LastScore { get => lastScore; }
    private const string LastScoreKey = "LastScore"; // PlayerPrefs Ű

    UIManager uIManager;
    public UIManager UIManager { get { return uIManager; } }

    private void Awake()
    {
        gameManager = this;
        uIManager = FindObjectOfType<UIManager>();
    }

    private void Start()
    {
        //PlayerPrefs.DeleteAll(); // ������ �ʱ�ȭ�� �ڵ�
        // ����� �ְ� ���� �ҷ����� (������ �⺻�� 0)
        bestScore = PlayerPrefs.GetInt(BestScoreKey, 0);

        // ���� ���� ������ �ְ� ���� ����
        prevBestScore = PlayerPrefs.GetInt(BestScoreKey, 0);
        PlayerPrefs.SetInt(PrevBestScoreKey, bestScore);

        // ���������� �÷����� ���� ���� �ʱ�ȭ
        PlayerPrefs.DeleteKey(LastScoreKey);

        uIManager.UpdateScore(0, bestScore);

        // ���� �Ͻ� ���� - ���� UI�� �б� ���� �ð� Ȯ����
        Time.timeScale = 0f;
    }

    public void GameOver()
    {
        Debug.Log("Game Over");

        uIManager.SetRestart();
    }

    public void RestartGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Player.isDead = false; // ��� ���� �ʱ�ȭ
        SceneManager.LoadScene("MainScene");
    }

    public void AddScore(int score)
    {
        if(Player.isDead == false)
        {
            currentScore += score; // ���� ���� ����

            lastScore = currentScore; // ���������� �÷����� ���� ���� �ֽ�ȭ
            PlayerPrefs.SetInt(LastScoreKey, lastScore); // PlayerPrefs�� ����

            if (bestScore < currentScore) // �ְ� ���� < ���� ����
            {
                Debug.Log("�ְ� ���� ����");
                Debug.Log($"���� �ְ� ���� : {bestScore}");

                bestScore = currentScore; // �ְ� ���� ����

                PlayerPrefs.SetInt(BestScoreKey, bestScore); // PlayerPrefs�� ����
            }

            Debug.Log("Score : " + currentScore);
            uIManager.UpdateScore(currentScore, bestScore);
        }
        else
        {
            Debug.Log("��������Ƿ� ���� ���� ����");
        }
    }
}
