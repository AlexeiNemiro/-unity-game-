using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����, ���������� �� ��������� ����.
/// </summary>
public class Sword : MonoBehaviour
{
    /// <summary>
    /// ���������� �����, ���������� �����.
    /// </summary>
    [SerializeField] private int damageAmount = 2;

    /// <summary>
    /// �������, ���������� ��� ������ ����.
    /// </summary>
    public event EventHandler OnSwordSwing;

    private PolygonCollider2D polygonCollider2D;

    /// <summary>
    /// �����, ���������� ��� ������������� �������.
    /// </summary>
    private void Awake()
    {
        polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    /// <summary>
    /// �����, ���������� ��� ������ ����.
    /// ��������� ��������� �����.
    /// </summary>
    private void Start()
    {
        AttackColliderTurnOff();
    }

    /// <summary>
    /// ����� ��� ���������� ����� �����.
    /// �������� ��������� ����� � �������� ������� ������ ����.
    /// </summary>
    public void Attack()
    {
        AttackColliderTurnOn();
        OnSwordSwing?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// �����, ���������� ��� ������������ ���� � ������ ��������.
    /// </summary>
    /// <param name="collision">��������� �������, � ������� ��������� ������������.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out EnemyEntity enemyEntity))
        {
            enemyEntity.TakeDamage(damageAmount);
        }
    }

    /// <summary>
    /// ��������� ��������� �����.
    /// </summary>
    public void AttackColliderTurnOff()
    {
        polygonCollider2D.enabled = false;
    }

    /// <summary>
    /// �������� ��������� �����.
    /// </summary>
    public void AttackColliderTurnOn()
    {
        polygonCollider2D.enabled = true;
    }
}
