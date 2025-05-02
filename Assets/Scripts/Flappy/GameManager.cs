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

    UIManager uIManager;
    public UIManager UIManager { get { return uIManager; } }

    private void Awake()
    {
        gameManager = this;
        uIManager = FindObjectOfType<UIManager>();
    }

    private void Start()
    {
        // ����� �ְ� ���� �ҷ����� (������ �⺻�� 0)
        bestScore = PlayerPrefs.GetInt(BestScoreKey, 0);

        uIManager.UpdateScore(0, bestScore);

        //uIManager.UpdateBestScore(bestScore);
    }

    public void GameOver()
    {
        Debug.Log("Game Over");

        //if (bestScore < currentScore) // �ְ� ���� < ���� ����
        //{
        //    Debug.Log("�ְ� ���� ����");
        //    Debug.Log($"���� �ְ� ���� : {bestScore}");

        //    bestScore = currentScore; // �ְ� ���� ����

        //    PlayerPrefs.SetInt(BestScoreKey, bestScore); // PlayerPrefs�� ����
        //}

        Debug.Log($"���� �ְ� ���� : {bestScore}");

        uIManager.SetRestart();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddScore(int score)
    {
        // ���� ����
        currentScore += score;

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
}
