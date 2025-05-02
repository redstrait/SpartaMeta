using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestscoreText;
    public TextMeshProUGUI restartText;

    // Start is called before the first frame update
    void Start()
    {
        if (restartText == null)
            Debug.LogError("restart text is null");

        if (scoreText == null)
            Debug.LogError("score text is null");

        restartText.gameObject.SetActive(false);
    }

    public void SetRestart()
    {
        restartText.gameObject.SetActive(true);
    }

    public void UpdateScore(int score, int best)
    {
        scoreText.text = score.ToString();
        bestscoreText.text = best.ToString();
    }

    //public void UpdateBestScore(int best) // 최고 점수 기록 및 갱신
    //{
    //    bestscoreText.text = best.ToString();
    //}
}
