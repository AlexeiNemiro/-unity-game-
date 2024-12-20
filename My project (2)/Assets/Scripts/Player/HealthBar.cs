using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public Player player; 

    void Start()
    {
        healthBar.maxValue = player.maxHealth; // ���������� ������������ �������� ��������
    }

    void Update()
    {
        healthBar.value = player.currentHealth; // ���������� �������� �������� � ����������� �� �������� �������� ������
    }
}



