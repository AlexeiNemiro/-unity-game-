using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// �����, ���������� �� ���������� �����.
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// ���� �����.
    /// </summary>
    public GameObject pauseMenu;

    /// <summary>
    /// ������ ��� ����������� ����.
    /// </summary>
    public Button restartButton;

    /// <summary>
    /// ������ ��� ����� �����.
    /// </summary>
    public Button languageButton;

    /// <summary>
    /// ������ ��� ������ �� ����.
    /// </summary>
    public Button exitButton;

    /// <summary>
    /// ��������� ���� ��� ����������� �������� �����.
    /// </summary>
    public TextMeshProUGUI scoreText;

    /// <summary>
    /// ��������� ���� ��� ����������� ������� �����.
    /// </summary>
    public TextMeshProUGUI bestScoreText;

    private bool isPaused = false;
    private string currentLanguage = "English";

    /// <summary>
    /// �����, ���������� ��� ������ ����.
    /// ������������� ��������� �������� � ������������� �� ������� ������.
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
    /// �����, ���������� ������ ���� ��� ���������� ��������� ����.
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
    /// ����� ��� ����������� ����.
    /// ���������� ���� � ������������� ������� �����.
    /// </summary>
    void RestartGame()
    {
        ScoreScript.scoreValue = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// ����� ��� ������������ ����� ����������.
    /// </summary>
    void ToggleLanguage()
    {
        currentLanguage = currentLanguage == "English" ? "Russian" : "English";
        UpdateLanguage();
        FindObjectOfType<ScoreScript>().currentLanguage = currentLanguage;
    }

    /// <summary>
    /// ����� ��� ������ �� ����.
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
    /// ����� ��� ���������� ������ ���������� � ����������� �� ���������� �����.
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
            scoreText.text = "����: " + ScoreScript.scoreValue;
            bestScoreText.text = "������ ����: " + PlayerPrefs.GetInt("BestScore", 0);
            languageButton.GetComponentInChildren<TextMeshProUGUI>().text = "����: �������";
        }
    }
}
