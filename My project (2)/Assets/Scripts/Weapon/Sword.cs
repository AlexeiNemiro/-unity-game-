using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс, отвечающий за поведение меча.
/// </summary>
public class Sword : MonoBehaviour
{
    /// <summary>
    /// Количество урона, наносимого мечом.
    /// </summary>
    [SerializeField] private int damageAmount = 2;

    /// <summary>
    /// Событие, вызываемое при взмахе меча.
    /// </summary>
    public event EventHandler OnSwordSwing;

    private PolygonCollider2D polygonCollider2D;

    /// <summary>
    /// Метод, вызываемый при инициализации объекта.
    /// </summary>
    private void Awake()
    {
        polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    /// <summary>
    /// Метод, вызываемый при старте игры.
    /// Отключает коллайдер атаки.
    /// </summary>
    private void Start()
    {
        AttackColliderTurnOff();
    }

    /// <summary>
    /// Метод для выполнения атаки мечом.
    /// Включает коллайдер атаки и вызывает событие взмаха меча.
    /// </summary>
    public void Attack()
    {
        AttackColliderTurnOn();
        OnSwordSwing?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Метод, вызываемый при столкновении меча с другим объектом.
    /// </summary>
    /// <param name="collision">Коллайдер объекта, с которым произошло столкновение.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out EnemyEntity enemyEntity))
        {
            enemyEntity.TakeDamage(damageAmount);
        }
    }

    /// <summary>
    /// Отключает коллайдер атаки.
    /// </summary>
    public void AttackColliderTurnOff()
    {
        polygonCollider2D.enabled = false;
    }

    /// <summary>
    /// Включает коллайдер атаки.
    /// </summary>
    public void AttackColliderTurnOn()
    {
        polygonCollider2D.enabled = true;
    }
}
