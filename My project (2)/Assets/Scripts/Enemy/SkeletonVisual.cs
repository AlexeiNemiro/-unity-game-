using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс, отвечающий за визуальное представление скелета.
/// </summary>
public class SkeletonVisual : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private SkeletonAI skeletonAI;
    [SerializeField] private EnemyEntity enemyEntity;
    [SerializeField] private GameObject skeletonShadow;
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
        skeletonAI.onEnemyAttack += skeletonAI_OnEnemyAttack;
        enemyEntity.OnTakeHit += enemyEntityOnTakeHit;
        enemyEntity.OnDeath += enemyEntityOnDeath;
    }

    /// <summary>
    /// Обработчик события смерти врага.
    /// </summary>
    private void enemyEntityOnDeath(object sender, System.EventArgs e)
    {
        animator.SetBool("IsDie", true);
        spriteRenderer.sortingOrder = -1;
        skeletonShadow.SetActive(false);
    }

    /// <summary>
    /// Обработчик события получения урона врагом.
    /// </summary>
    private void enemyEntityOnTakeHit(object sender, System.EventArgs e)
    {
        animator.SetTrigger("TakeHit");
    }

    /// <summary>
    /// Обработчик события атаки врага.
    /// </summary>
    private void skeletonAI_OnEnemyAttack(object sender, System.EventArgs e)
    {
        animator.SetTrigger("Attack");
    }

    /// <summary>
    /// Метод, вызываемый при уничтожении объекта.
    /// </summary>
    private void OnDestroy()
    {
        skeletonAI.onEnemyAttack -= skeletonAI_OnEnemyAttack;
    }

    /// <summary>
    /// Метод, вызываемый каждый кадр для обновления состояния анимации.
    /// </summary>
    private void Update()
    {
        animator.SetBool("IsRunning", skeletonAI.IsRunning);
        animator.SetFloat("ChasingSpeedMultiplier", skeletonAI.GetRoamingAnimationSpeed());
    }

    /// <summary>
    /// Увеличивает счет при смерти врага.
    /// </summary>
    public void TriggerGetScore()
    {
        ScoreScript.scoreValue++;
        Debug.Log("death");
    }

    /// <summary>
    /// Отключает коллайдер атаки.
    /// </summary>
    public void TriggerAttackAnimationTurnOff()
    {
        enemyEntity.PolygonColliderTurnOff();
    }

    /// <summary>
    /// Включает коллайдер атаки.
    /// </summary>
    public void TriggerAttackAnimationTurnOn()
    {
        enemyEntity.PolygonColliderTurnOn();
    }
}
