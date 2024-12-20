using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    public static int scoreValue = 0;
    private int bestScore;
    private int previousScoreValue;
    public TextMeshProUGUI score;
    public TextMeshProUGUI bestScoreText;
    public string currentLanguage = "English";

    private void Start()
    {
        score = GetComponent<TextMeshProUGUI>();
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        UpdateBestScoreText();
    }

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

    private void UpdateBestScoreText()
    {
        if (bestScoreText != null)
        {
            bestScoreText.text = "Best Score: " + bestScore;
        }
    }

    public void UpdateScoreText()
    {
        if (score != null) { if (currentLanguage == "English") { score.text = "Score: " + scoreValue; } else { score.text = "—чет: " + scoreValue; } }
    }
}
