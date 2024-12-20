using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public GameObject pauseMenu;
    public Button restartButton;
    public Button languageButton;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;

    private bool isPaused = false;
    private string currentLanguage = "English";

    void Start()
    {
        pauseMenu.SetActive(false);
        restartButton.onClick.AddListener(RestartGame);
        languageButton.onClick.AddListener(ToggleLanguage);
        UpdateLanguage();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            pauseMenu.SetActive(isPaused);
        }
    }

    void RestartGame()
    {
        // Ћогика рестарта игры
        ScoreScript.scoreValue = 0;
        // ѕерезагрузка сцены или сброс параметров
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void ToggleLanguage()
    {
        if (currentLanguage == "English")
        {
            currentLanguage = "Russian";
        }
        else
        {
            currentLanguage = "English";
        }
        UpdateLanguage();
        FindObjectOfType<ScoreScript>().currentLanguage = currentLanguage;
    }

    void UpdateLanguage()
    {
        if (currentLanguage == "English")
        {
            scoreText.text = "Score: " + ScoreScript.scoreValue;
            bestScoreText.text = "Best Score: " + PlayerPrefs.GetInt("BestScore", 0);
            languageButton.GetComponentInChildren<TextMeshProUGUI>().text = "Language: English";
        }
        else
        {
            scoreText.text = "—чет: " + ScoreScript.scoreValue;
            bestScoreText.text = "Ћучший счет: " + PlayerPrefs.GetInt("BestScore", 0);
            languageButton.GetComponentInChildren<TextMeshProUGUI>().text = "язык: –усский";
        }
        
    }
}


