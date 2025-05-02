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
        int prevBestScore = PlayerPrefs.GetInt("PrevBestScore", 0); // ���� ���� ������ �ְ� ���� �ҷ�����
        int lastScore = PlayerPrefs.GetInt("LastScore", 0); // �ֱ� ���� �ҷ�����

        if(lastScore > prevBestScore) // �ֱ� ������ ���� ���� ������ �ְ� �������� Ŭ ��
        {
            goodrestartText.gameObject.SetActive(true);
        }
        else if(lastScore == prevBestScore) // �ֱ� ������ �ְ� ������ ����
        {
            sosorestartText.gameObject.SetActive(true);
        }
        else // �ֱ� ������ �ְ� �������� ���� ��
        {
            badrestartText.gameObject.SetActive(true);
        }
    }

    public void UpdateScore(int score, int best) // ���� & �ְ� ���� ��� �� ����
    {
        scoreText.text = score.ToString();
        bestscoreText.text = best.ToString();
    }
}
