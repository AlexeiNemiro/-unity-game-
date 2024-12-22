using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����, ���������� �� ��������� ������� ������������.
/// </summary>
public class KnockBack : MonoBehaviour
{
    /// <summary>
    /// ���� ������������.
    /// </summary>
    [SerializeField] private float knockBackForce = 1f;

    /// <summary>
    /// ������������ ����� �������� ��� ������������.
    /// </summary>
    [SerializeField] private float knockBackMovingTimerMax = 0.3f;

    private float knockBackMovingTimer;
    private Rigidbody2D rb;

    /// <summary>
    /// �����, ���������� ��� ������������� �������.
    /// </summary>
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// �����, ���������� ������ ���� ��� ���������� ��������� ������������.
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
    /// ��������, �����������, ��������� �� ������ � ��������� ������������.
    /// </summary>
    public bool IsGettingBack { get; private set; }

    /// <summary>
    /// ����� ��� ��������� ������������ �������.
    /// </summary>
    /// <param name="damageSource">�������� �����.</param>
    public void GetKnockedBack(Transform damageSource)
    {
        IsGettingBack = true;
        knockBackMovingTimer = knockBackMovingTimerMax;
        Vector2 difference = (transform.position - damageSource.position).normalized * knockBackForce / rb.mass;
        rb.AddForce(difference, ForceMode2D.Impulse);
    }

    /// <summary>
    /// ����� ��� ��������� �������� ��� ������������.
    /// </summary>
    public void StopKnockBackMovement()
    {
        rb.velocity = Vector2.zero;
        IsGettingBack = false;
    }
}
