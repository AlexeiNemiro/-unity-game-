using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����, ���������� �� ������������ ����� ����.
/// </summary>
public class SwordSlashVis : MonoBehaviour
{
    /// <summary>
    /// ������ �� ������ ����.
    /// </summary>
    [SerializeField] private Sword sword;

    private Animator animator;

    /// <summary>
    /// �����, ���������� ��� ������������� �������.
    /// </summary>
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// �����, ���������� ��� ������ ����.
    /// ������������� �� ������� ������ ����.
    /// </summary>
    private void Start()
    {
        sword.OnSwordSwing += Sword_OnSwordSwing;
    }

    /// <summary>
    /// ���������� ������� ������ ����.
    /// </summary>
    private void Sword_OnSwordSwing(object sender, System.EventArgs e)
    {
        animator.SetTrigger("Attack");
    }
}
