using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager;
    public static GameManager Instance { get { return gameManager; } }

    private int currentScore = 0; // 현재 점수

    int bestScore = 0; // 최고 점수
    public int BestScore { get => bestScore; }
    private const string BestScoreKey = "BestScore"; // PlayerPrefs 키

    UIManager uIManager;
    public UIManager UIManager { get { return uIManager; } }

    private void Awake()
    {
        gameManager = this;
        uIManager = FindObjectOfType<UIManager>();
    }

    private void Start()
    {
        uIManager.UpdateScore(0);

        // 저장된 최고 점수 불러오기 (없으면 기본값 0)
        bestScore = PlayerPrefs.GetInt(BestScoreKey, 0);
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        
        if (bestScore < currentScore) // 최고 점수 < 현재 점수
        {
            Debug.Log("최고 점수 갱신");
            Debug.Log($"기존 최고 점수 : {bestScore}");

            bestScore = currentScore; // 최고 점수 갱신

            PlayerPrefs.SetInt(BestScoreKey, bestScore); // PlayerPrefs에 저장
        }

        Debug.Log($"현재 최고 점수 : {bestScore}");

        uIManager.SetRestart();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddScore(int score)
    {
        //if(Player.isDead != true) // 플레이어가 죽지 않았다면
        //{
            // 점수 증가
            currentScore += score;
            Debug.Log("Score : " + currentScore);
            uIManager.UpdateScore(currentScore);
        //}
        //Debug.Log("게임 오버 상태이므로 점수 증가 X");
    }
}
