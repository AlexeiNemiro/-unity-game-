using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// �����, ���������� �� ���������� ������������� ������.
/// </summary>
public class PlayerVisual : MonoBehaviour
{
    /// <summary>
    /// ��������� ��� �������� ��������� ����.
    /// </summary>
    private const string IS_RUNNING = "isRunning";

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    /// <summary>
    /// �����, ���������� ��� ������������� �������.
    /// </summary>
    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// �����, ���������� ��� ������ ����.
    /// </summary>
    private void Start()
    {
        Player.Instance.OnPlayerDeath += Player_OnPlayerDeath;
        Player.Instance.OnPlayerTakeHit += Player_OnPlayerTakeHit;
    }

    /// <summary>
    /// ���������� ������� ������ ������.
    /// </summary>
    private void Player_OnPlayerDeath(object sender, System.EventArgs e)
    {
        animator.SetBool("IsDie", true);
    }

    /// <summary>
    /// ���������� ������� ��������� ����� �������.
    /// </summary>
    private void Player_OnPlayerTakeHit(object sender, System.EventArgs e)
    {
        animator.SetTrigger("TakeHit");
    }

    /// <summary>
    /// �����, ���������� ������ ���� ��� ���������� ��������� ��������.
    /// </summary>
    private void Update()
    {
        animator.SetBool(IS_RUNNING, Player.Instance.IsRunning());
        if (Player.Instance.IsAlive())
        {
            AdjustPlayerFacingDirection();
        }
    }

    /// <summary>
    /// ����� ��� ������������� ����������� ������� ������.
    /// </summary>
    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Player.Instance.GetMousePosition();
        Vector3 playerPosition = Player.Instance.GetPlayerScreenPosition();
        if (mousePos.x < playerPosition.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}
