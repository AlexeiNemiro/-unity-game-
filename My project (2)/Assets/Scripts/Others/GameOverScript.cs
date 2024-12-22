using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// �����, ���������� �� ��������� ��������� ����.
/// </summary>
public class GameOverScript : MonoBehaviour
{
    /// <summary>
    /// ������, �������������� ��� ��������� ����.
    /// </summary>
    public GameObject gameOverPanel;

    /// <summary>
    /// �����, �������������� ��� ��������� ����.
    /// </summary>
    public TextMeshProUGUI gameOverText;

    /// <summary>
    /// �����, ���������� ��� ������ ����.
    /// ������������� ��������� ��������� ������ ��������� ����.
    /// </summary>
    private void Start()
    {
        gameOverPanel.SetActive(false);
    }

    /// <summary>
    /// ����� ��� ��������� ��������� ����.
    /// ���������� ������ ��������� ����.
    /// </summary>
    public void GameOverPlayer()
    {
        gameOverPanel.SetActive(true);
    }

    /// <summary>
    /// ����� ��� ����������� ������.
    /// ��������� ������� ����� ������.
    /// </summary>
    public void RestartLevel()
    {
        ScoreScript.scoreValue = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
