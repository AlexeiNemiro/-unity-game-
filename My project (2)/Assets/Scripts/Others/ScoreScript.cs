using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Класс, отвечающий за отображение и обновление счета игрока.
/// </summary>
public class ScoreScript : MonoBehaviour
{
    /// <summary>
    /// Текущее значение счета.
    /// </summary>
    public static int scoreValue = 0;

    private int bestScore;
    private int previousScoreValue;

    /// <summary>
    /// Текстовое поле для отображения текущего счета.
    /// </summary>
    public TextMeshProUGUI score;

    /// <summary>
    /// Текстовое поле для отображения лучшего счета.
    /// </summary>
    public TextMeshProUGUI bestScoreText;

    /// <summary>
    /// Текущий язык интерфейса.
    /// </summary>
    public string currentLanguage = "English";

    /// <summary>
    /// Метод, вызываемый при старте игры.
    /// Устанавливает начальные значения и обновляет текст лучшего счета.
    /// </summary>
    private void Start()
    {
        score = GetComponent<TextMeshProUGUI>();
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        UpdateBestScoreText();
    }

    /// <summary>
    /// Метод, вызываемый каждый кадр для обновления счета.
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
    /// Обновляет текст лучшего счета в зависимости от выбранного языка.
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
                bestScoreText.text = "Лучший счет: " + bestScore;
            }
        }
    }

    /// <summary>
    /// Обновляет текст текущего счета в зависимости от выбранного языка.
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
                score.text = "Счет: " + scoreValue;
            }
        }
    }

}
