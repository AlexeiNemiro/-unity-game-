using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// Класс, отвечающий за обработку окончания игры.
/// </summary>
public class GameOverScript : MonoBehaviour
{
    /// <summary>
    /// Панель, отображающаяся при окончании игры.
    /// </summary>
    public GameObject gameOverPanel;

    /// <summary>
    /// Текст, отображающийся при окончании игры.
    /// </summary>
    public TextMeshProUGUI gameOverText;

    /// <summary>
    /// Метод, вызываемый при старте игры.
    /// Устанавливает начальное состояние панели окончания игры.
    /// </summary>
    private void Start()
    {
        gameOverPanel.SetActive(false);
    }

    /// <summary>
    /// Метод для обработки окончания игры.
    /// Активирует панель окончания игры.
    /// </summary>
    public void GameOverPlayer()
    {
        gameOverPanel.SetActive(true);
    }

    /// <summary>
    /// Метод для перезапуска уровня.
    /// Загружает текущую сцену заново.
    /// </summary>
    public void RestartLevel()
    {
        ScoreScript.scoreValue = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
