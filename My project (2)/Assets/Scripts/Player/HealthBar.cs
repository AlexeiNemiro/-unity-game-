using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс, отвечающий за отображение полоски здоровья игрока.
/// </summary>
public class HealthBar : MonoBehaviour
{
    /// <summary>
    /// Слайдер для отображения здоровья.
    /// </summary>
    public Slider healthBar;

    /// <summary>
    /// Ссылка на объект игрока.
    /// </summary>
    public Player player;

    /// <summary>
    /// Метод, вызываемый при старте игры.
    /// Устанавливает максимальное значение слайдера в зависимости от максимального здоровья игрока.
    /// </summary>
    void Start()
    {
        healthBar.maxValue = player.maxHealth; // Установите максимальное значение слайдера
    }

    /// <summary>
    /// Метод, вызываемый каждый кадр для обновления значения слайдера.
    /// Обновляет значение слайдера в зависимости от текущего здоровья игрока.
    /// </summary>
    void Update()
    {
        healthBar.value = player.currentHealth; // Обновляйте значение слайдера в зависимости от текущего здоровья игрока
    }
}
