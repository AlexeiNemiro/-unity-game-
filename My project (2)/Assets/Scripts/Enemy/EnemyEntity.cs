using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс, представляющий сущность врага.
/// </summary>
public class EnemyEntity : MonoBehaviour
{
    /// <summary>
    /// Событие, вызываемое при получении урона врагом.
    /// </summary>
    public event EventHandler OnTakeHit;

    /// <summary>
    /// Событие, вызываемое при смерти врага.
    /// </summary>
    public event EventHandler OnDeath;

    /// <summary>
    /// Максимальное здоровье врага.
    /// </summary>
    [SerializeField] public int maxHealth;

    /// <summary>
    /// Количество урона, наносимого врагом.
    /// </summary>
    [SerializeField] private int damageAmount = 2;

    /// <summary>
    /// Текущее здоровье врага.
    /// </summary>
    public int currentHealth;

    /// <summary>
    /// Очки, получаемые за убийство врага.
    /// </summary>
    public int score = 0;

    /// <summary>
    /// Текст для отображения очков.
    /// </summary>
    public TextMeshProUGUI scoreText;

    private PolygonCollider2D polygonCollider;
    private BoxCollider2D boxCollider;
    private SkeletonAI skeletonAI;

    /// <summary>
    /// Метод, вызываемый при инициализации объекта.
    /// </summary>
    private void Awake()
    {
        polygonCollider = GetComponent<PolygonCollider2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        skeletonAI = GetComponent<SkeletonAI>();
    }

    /// <summary>
    /// Метод, вызываемый при старте игры.
    /// </summary>
    private void Start()
    {
        currentHealth = maxHealth;
    }

    /// <summary>
    /// Метод для нанесения урона врагу.
    /// </summary>
    /// <param name="damage">Количество урона.</param>
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        OnTakeHit?.Invoke(this, EventArgs.Empty);
        DetectDeath();
    }

    /// <summary>
    /// Метод, вызываемый при нахождении врага в триггерной зоне.
    /// </summary>
    /// <param name="collision">Коллайдер объекта, с которым произошло столкновение.</param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
        {
            player.TakeDamage(transform, damageAmount);
        }
    }

    /// <summary>
    /// Метод для проверки смерти врага.
    /// </summary>
    private void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            boxCollider.enabled = false;
            polygonCollider.enabled = false;
            skeletonAI.SetDeathState();
            OnDeath?.Invoke(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// Отключает полигональный коллайдер.
    /// </summary>
    public void PolygonColliderTurnOff()
    {
        polygonCollider.enabled = false;
    }

    /// <summary>
    /// Включает полигональный коллайдер.
    /// </summary>
    public void PolygonColliderTurnOn()
    {
        polygonCollider.enabled = true;
    }
}
