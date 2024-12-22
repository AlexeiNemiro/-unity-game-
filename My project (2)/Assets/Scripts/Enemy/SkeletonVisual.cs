using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����, ���������� �� ���������� ������������� �������.
/// </summary>
public class SkeletonVisual : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private SkeletonAI skeletonAI;
    [SerializeField] private EnemyEntity enemyEntity;
    [SerializeField] private GameObject skeletonShadow;
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
        skeletonAI.onEnemyAttack += skeletonAI_OnEnemyAttack;
        enemyEntity.OnTakeHit += enemyEntityOnTakeHit;
        enemyEntity.OnDeath += enemyEntityOnDeath;
    }

    /// <summary>
    /// ���������� ������� ������ �����.
    /// </summary>
    private void enemyEntityOnDeath(object sender, System.EventArgs e)
    {
        animator.SetBool("IsDie", true);
        spriteRenderer.sortingOrder = -1;
        skeletonShadow.SetActive(false);
    }

    /// <summary>
    /// ���������� ������� ��������� ����� ������.
    /// </summary>
    private void enemyEntityOnTakeHit(object sender, System.EventArgs e)
    {
        animator.SetTrigger("TakeHit");
    }

    /// <summary>
    /// ���������� ������� ����� �����.
    /// </summary>
    private void skeletonAI_OnEnemyAttack(object sender, System.EventArgs e)
    {
        animator.SetTrigger("Attack");
    }

    /// <summary>
    /// �����, ���������� ��� ����������� �������.
    /// </summary>
    private void OnDestroy()
    {
        skeletonAI.onEnemyAttack -= skeletonAI_OnEnemyAttack;
    }

    /// <summary>
    /// �����, ���������� ������ ���� ��� ���������� ��������� ��������.
    /// </summary>
    private void Update()
    {
        animator.SetBool("IsRunning", skeletonAI.IsRunning);
        animator.SetFloat("ChasingSpeedMultiplier", skeletonAI.GetRoamingAnimationSpeed());
    }

    /// <summary>
    /// ����������� ���� ��� ������ �����.
    /// </summary>
    public void TriggerGetScore()
    {
        ScoreScript.scoreValue++;
        Debug.Log("death");
    }

    /// <summary>
    /// ��������� ��������� �����.
    /// </summary>
    public void TriggerAttackAnimationTurnOff()
    {
        enemyEntity.PolygonColliderTurnOff();
    }

    /// <summary>
    /// �������� ��������� �����.
    /// </summary>
    public void TriggerAttackAnimationTurnOn()
    {
        enemyEntity.PolygonColliderTurnOn();
    }
}
