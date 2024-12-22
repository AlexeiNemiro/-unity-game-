using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс, отвечающий за обработку эффекта отбрасывания.
/// </summary>
public class KnockBack : MonoBehaviour
{
    /// <summary>
    /// Сила отбрасывания.
    /// </summary>
    [SerializeField] private float knockBackForce = 1f;

    /// <summary>
    /// Максимальное время движения при отбрасывании.
    /// </summary>
    [SerializeField] private float knockBackMovingTimerMax = 0.3f;

    private float knockBackMovingTimer;
    private Rigidbody2D rb;

    /// <summary>
    /// Метод, вызываемый при инициализации объекта.
    /// </summary>
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Метод, вызываемый каждый кадр для обновления состояния отбрасывания.
    /// </summary>
    private void Update()
    {
        knockBackMovingTimer -= Time.deltaTime;
        if (knockBackMovingTimer < 0)
        {
            StopKnockBackMovement();
        }
    }

    /// <summary>
    /// Свойство, указывающее, находится ли объект в состоянии отбрасывания.
    /// </summary>
    public bool IsGettingBack { get; private set; }

    /// <summary>
    /// Метод для обработки отбрасывания объекта.
    /// </summary>
    /// <param name="damageSource">Источник урона.</param>
    public void GetKnockedBack(Transform damageSource)
    {
        IsGettingBack = true;
        knockBackMovingTimer = knockBackMovingTimerMax;
        Vector2 difference = (transform.position - damageSource.position).normalized * knockBackForce / rb.mass;
        rb.AddForce(difference, ForceMode2D.Impulse);
    }

    /// <summary>
    /// Метод для остановки движения при отбрасывании.
    /// </summary>
    public void StopKnockBackMovement()
    {
        rb.velocity = Vector2.zero;
        IsGettingBack = false;
    }
}
