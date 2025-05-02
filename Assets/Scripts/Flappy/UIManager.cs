using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestscoreText;
    public TextMeshProUGUI goodrestartText;
    public TextMeshProUGUI sosorestartText;
    public TextMeshProUGUI badrestartText;

    // Start is called before the first frame update
    void Start()
    {
        if (goodrestartText == null)
            Debug.LogError("goodrestart text is null");

        if (badrestartText == null)
            Debug.LogError("badrestart text is null");

        if (scoreText == null)
            Debug.LogError("score text is null");

        goodrestartText.gameObject.SetActive(false);
        sosorestartText.gameObject.SetActive(false);
        badrestartText.gameObject.SetActive(false);
    }

    public void SetRestart()
    {
        int prevBestScore = PlayerPrefs.GetInt("PrevBestScore", 0); // 게임 시작 시점의 최고 점수 불러오기
        int lastScore = PlayerPrefs.GetInt("LastScore", 0); // 최근 점수 불러오기

        if(lastScore > prevBestScore) // 최근 점수가 게임 시작 시점의 최고 점수보다 클 때
        {
            goodrestartText.gameObject.SetActive(true);
        }
        else if(lastScore == prevBestScore) // 최근 점수와 최고 점수가 동일
        {
            sosorestartText.gameObject.SetActive(true);
        }
        else // 최근 점수가 최고 점수보다 낮을 시
        {
            badrestartText.gameObject.SetActive(true);
        }
    }

    public void UpdateScore(int score, int best) // 현재 & 최고 점수 기록 및 갱신
    {
        scoreText.text = score.ToString();
        bestscoreText.text = best.ToString();
    }
}
