using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Класс, отвечающий за визуальное представление игрока.
/// </summary>
public class PlayerVisual : MonoBehaviour
{
    /// <summary>
    /// Константа для проверки состояния бега.
    /// </summary>
    private const string IS_RUNNING = "isRunning";

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    /// <summary>
    /// Метод, вызываемый при инициализации объекта.
    /// </summary>
    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Метод, вызываемый при старте игры.
    /// </summary>
    private void Start()
    {
        Player.Instance.OnPlayerDeath += Player_OnPlayerDeath;
        Player.Instance.OnPlayerTakeHit += Player_OnPlayerTakeHit;
    }

    /// <summary>
    /// Обработчик события смерти игрока.
    /// </summary>
    private void Player_OnPlayerDeath(object sender, System.EventArgs e)
    {
        animator.SetBool("IsDie", true);
    }

    /// <summary>
    /// Обработчик события получения урона игроком.
    /// </summary>
    private void Player_OnPlayerTakeHit(object sender, System.EventArgs e)
    {
        animator.SetTrigger("TakeHit");
    }

    /// <summary>
    /// Метод, вызываемый каждый кадр для обновления состояния анимации.
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
    /// Метод для корректировки направления взгляда игрока.
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
