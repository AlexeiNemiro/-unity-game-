using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// Класс, отвечающий за управление игрой.
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Меню паузы.
    /// </summary>
    public GameObject pauseMenu;

    /// <summary>
    /// Кнопка для перезапуска игры.
    /// </summary>
    public Button restartButton;

    /// <summary>
    /// Кнопка для смены языка.
    /// </summary>
    public Button languageButton;

    /// <summary>
    /// Кнопка для выхода из игры.
    /// </summary>
    public Button exitButton;

    /// <summary>
    /// Текстовое поле для отображения текущего счета.
    /// </summary>
    public TextMeshProUGUI scoreText;

    /// <summary>
    /// Текстовое поле для отображения лучшего счета.
    /// </summary>
    public TextMeshProUGUI bestScoreText;

    private bool isPaused = false;
    private string currentLanguage = "English";

    /// <summary>
    /// Метод, вызываемый при старте игры.
    /// Устанавливает начальные значения и подписывается на события кнопок.
    /// </summary>
    void Start()
    {
        pauseMenu.SetActive(false);
        restartButton.onClick.AddListener(RestartGame);
        languageButton.onClick.AddListener(ToggleLanguage);
        exitButton.onClick.AddListener(ExitGame);
        UpdateLanguage();
    }

    /// <summary>
    /// Метод, вызываемый каждый кадр для обновления состояния игры.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            pauseMenu.SetActive(isPaused);
        }
    }

    /// <summary>
    /// Метод для перезапуска игры.
    /// Сбрасывает счет и перезагружает текущую сцену.
    /// </summary>
    void RestartGame()
    {
        ScoreScript.scoreValue = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Метод для переключения языка интерфейса.
    /// </summary>
    void ToggleLanguage()
    {
        currentLanguage = currentLanguage == "English" ? "Russian" : "English";
        UpdateLanguage();
        FindObjectOfType<ScoreScript>().currentLanguage = currentLanguage;
    }

    /// <summary>
    /// Метод для выхода из игры.
    /// </summary>
    void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    /// <summary>
    /// Метод для обновления текста интерфейса в зависимости от выбранного языка.
    /// </summary>
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
            scoreText.text = "Счет: " + ScoreScript.scoreValue;
            bestScoreText.text = "Лучший счет: " + PlayerPrefs.GetInt("BestScore", 0);
            languageButton.GetComponentInChildren<TextMeshProUGUI>().text = "Язык: Русский";
        }
    }
}
