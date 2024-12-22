using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс, отвечающий за визуализацию удара меча.
/// </summary>
public class SwordSlashVis : MonoBehaviour
{
    /// <summary>
    /// Ссылка на объект меча.
    /// </summary>
    [SerializeField] private Sword sword;

    private Animator animator;

    /// <summary>
    /// Метод, вызываемый при инициализации объекта.
    /// </summary>
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Метод, вызываемый при старте игры.
    /// Подписывается на событие взмаха меча.
    /// </summary>
    private void Start()
    {
        sword.OnSwordSwing += Sword_OnSwordSwing;
    }

    /// <summary>
    /// Обработчик события взмаха меча.
    /// </summary>
    private void Sword_OnSwordSwing(object sender, System.EventArgs e)
    {
        animator.SetTrigger("Attack");
    }
}
