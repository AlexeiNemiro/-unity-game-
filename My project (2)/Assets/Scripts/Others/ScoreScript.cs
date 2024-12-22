using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// �����, ���������� �� ����������� � ���������� ����� ������.
/// </summary>
public class ScoreScript : MonoBehaviour
{
    /// <summary>
    /// ������� �������� �����.
    /// </summary>
    public static int scoreValue = 0;

    private int bestScore;
    private int previousScoreValue;

    /// <summary>
    /// ��������� ���� ��� ����������� �������� �����.
    /// </summary>
    public TextMeshProUGUI score;

    /// <summary>
    /// ��������� ���� ��� ����������� ������� �����.
    /// </summary>
    public TextMeshProUGUI bestScoreText;

    /// <summary>
    /// ������� ���� ����������.
    /// </summary>
    public string currentLanguage = "English";

    /// <summary>
    /// �����, ���������� ��� ������ ����.
    /// ������������� ��������� �������� � ��������� ����� ������� �����.
    /// </summary>
    private void Start()
    {
        score = GetComponent<TextMeshProUGUI>();
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        UpdateBestScoreText();
    }

    /// <summary>
    /// �����, ���������� ������ ���� ��� ���������� �����.
    /// </summary>
    private void Update()
    {
        if (scoreValue != previousScoreValue)
        {
            UpdateScoreText();
            previousScoreValue = scoreValue;
        }

        if (scoreValue > bestScore)
        {
            bestScore = scoreValue;
            PlayerPrefs.SetInt("BestScore", bestScore);
            PlayerPrefs.Save();
            UpdateBestScoreText();
        }
    }

    /// <summary>
    /// ��������� ����� ������� ����� � ����������� �� ���������� �����.
    /// </summary>
    private void UpdateBestScoreText()
    {
        if (bestScoreText != null)
        {
            if (currentLanguage == "English")
            {
                bestScoreText.text = "Best Score: " + bestScore;
            }
            else
            {
                bestScoreText.text = "������ ����: " + bestScore;
            }
        }
    }

    /// <summary>
    /// ��������� ����� �������� ����� � ����������� �� ���������� �����.
    /// </summary>
    public void UpdateScoreText()
    {
        if (score != null)
        {
            if (currentLanguage == "English")
            {
                score.text = "Score: " + scoreValue;
            }
            else
            {
                score.text = "����: " + scoreValue;
            }
        }
    }

}
