using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �����, �������������� �������� �����.
/// </summary>
public class EnemyEntity : MonoBehaviour
{
    /// <summary>
    /// �������, ���������� ��� ��������� ����� ������.
    /// </summary>
    public event EventHandler OnTakeHit;

    /// <summary>
    /// �������, ���������� ��� ������ �����.
    /// </summary>
    public event EventHandler OnDeath;

    /// <summary>
    /// ������������ �������� �����.
    /// </summary>
    [SerializeField] public int maxHealth;

    /// <summary>
    /// ���������� �����, ���������� ������.
    /// </summary>
    [SerializeField] private int damageAmount = 2;

    /// <summary>
    /// ������� �������� �����.
    /// </summary>
    public int currentHealth;

    /// <summary>
    /// ����, ���������� �� �������� �����.
    /// </summary>
    public int score = 0;

    /// <summary>
    /// ����� ��� ����������� �����.
    /// </summary>
    public TextMeshProUGUI scoreText;

    private PolygonCollider2D polygonCollider;
    private BoxCollider2D boxCollider;
    private SkeletonAI skeletonAI;

    /// <summary>
    /// �����, ���������� ��� ������������� �������.
    /// </summary>
    private void Awake()
    {
        polygonCollider = GetComponent<PolygonCollider2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        skeletonAI = GetComponent<SkeletonAI>();
    }

    /// <summary>
    /// �����, ���������� ��� ������ ����.
    /// </summary>
    private void Start()
    {
        currentHealth = maxHealth;
    }

    /// <summary>
    /// ����� ��� ��������� ����� �����.
    /// </summary>
    /// <param name="damage">���������� �����.</param>
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        OnTakeHit?.Invoke(this, EventArgs.Empty);
        DetectDeath();
    }

    /// <summary>
    /// �����, ���������� ��� ���������� ����� � ���������� ����.
    /// </summary>
    /// <param name="collision">��������� �������, � ������� ��������� ������������.</param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
        {
            player.TakeDamage(transform, damageAmount);
        }
    }

    /// <summary>
    /// ����� ��� �������� ������ �����.
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
    /// ��������� ������������� ���������.
    /// </summary>
    public void PolygonColliderTurnOff()
    {
        polygonCollider.enabled = false;
    }

    /// <summary>
    /// �������� ������������� ���������.
    /// </summary>
    public void PolygonColliderTurnOn()
    {
        polygonCollider.enabled = true;
    }
}
