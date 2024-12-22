using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �����, ���������� �� ����������� ������� �������� ������.
/// </summary>
public class HealthBar : MonoBehaviour
{
    /// <summary>
    /// ������� ��� ����������� ��������.
    /// </summary>
    public Slider healthBar;

    /// <summary>
    /// ������ �� ������ ������.
    /// </summary>
    public Player player;

    /// <summary>
    /// �����, ���������� ��� ������ ����.
    /// ������������� ������������ �������� �������� � ����������� �� ������������� �������� ������.
    /// </summary>
    void Start()
    {
        healthBar.maxValue = player.maxHealth; // ���������� ������������ �������� ��������
    }

    /// <summary>
    /// �����, ���������� ������ ���� ��� ���������� �������� ��������.
    /// ��������� �������� �������� � ����������� �� �������� �������� ������.
    /// </summary>
    void Update()
    {
        healthBar.value = player.currentHealth; // ���������� �������� �������� � ����������� �� �������� �������� ������
    }
}
