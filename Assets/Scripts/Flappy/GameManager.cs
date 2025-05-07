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

    int prevBestScore = 0; // 게임 시작 시점의 최고 점수 - UIManager에서 최고 점수가 갱신되었는지 확인용
    public int PrevBestScore { get => bestScore; }
    private const string PrevBestScoreKey = "PrevBestScore"; // PlayerPrefs 키

    int lastScore = 0; // 마지막으로 플레이한 라운드의 점수
    public int LastScore { get => lastScore; }
    private const string LastScoreKey = "LastScore"; // PlayerPrefs 키

    UIManager uIManager;
    public UIManager UIManager { get { return uIManager; } }

    private void Awake()
    {
        gameManager = this;
        uIManager = FindObjectOfType<UIManager>();
    }

    private void Start()
    {
        //PlayerPrefs.DeleteAll(); // 데이터 초기화용 코드
        // 저장된 최고 점수 불러오기 (없으면 기본값 0)
        bestScore = PlayerPrefs.GetInt(BestScoreKey, 0);

        // 게임 시작 시점의 최고 점수 저장
        prevBestScore = PlayerPrefs.GetInt(BestScoreKey, 0);
        PlayerPrefs.SetInt(PrevBestScoreKey, bestScore);

        // 마지막으로 플레이한 라운드 점수 초기화
        PlayerPrefs.DeleteKey(LastScoreKey);

        uIManager.UpdateScore(0, bestScore);

        // 게임 일시 정지 - 설명 UI를 읽기 위한 시간 확보용
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
        Player.isDead = false; // 사망 여부 초기화
        SceneManager.LoadScene("MainScene");
    }

    public void AddScore(int score)
    {
        if(Player.isDead == false)
        {
            currentScore += score; // 현재 점수 증가

            lastScore = currentScore; // 마지막으로 플레이한 라운드 점수 최신화
            PlayerPrefs.SetInt(LastScoreKey, lastScore); // PlayerPrefs에 저장

            if (bestScore < currentScore) // 최고 점수 < 현재 점수
            {
                Debug.Log("최고 점수 갱신");
                Debug.Log($"기존 최고 점수 : {bestScore}");

                bestScore = currentScore; // 최고 점수 갱신

                PlayerPrefs.SetInt(BestScoreKey, bestScore); // PlayerPrefs에 저장
            }

            Debug.Log("Score : " + currentScore);
            uIManager.UpdateScore(currentScore, bestScore);
        }
        else
        {
            Debug.Log("사망했으므로 점수 증가 없음");
        }
    }
}
