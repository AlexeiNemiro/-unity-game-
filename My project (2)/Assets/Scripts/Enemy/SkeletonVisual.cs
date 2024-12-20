using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonVisual : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private SkeletonAI skeletonAI;
    [SerializeField] private EnemyEntity enemyEntity;
    [SerializeField] private GameObject skeletonShadow;
    


    
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        skeletonAI.onEnemyAttack += skeletonAI_OnEnemyAttack;
        enemyEntity.OnTakeHit += enemyEntityOnTakeHit;
        enemyEntity.OnDeath += enemyEntityOnDeath;
    }

    private void enemyEntityOnDeath(object sender,System.EventArgs e)
    {
        animator.SetBool("IsDie", true);
        
        spriteRenderer.sortingOrder = -1;
        skeletonShadow.SetActive(false);
        
    }

 
    

    private void enemyEntityOnTakeHit(object sender,System.EventArgs e)
    {
        animator.SetTrigger("TakeHit");
    }
    private void skeletonAI_OnEnemyAttack(object sender, System.EventArgs e)
    {
        animator.SetTrigger("Attack");
    }

    private void OnDestroy()
    {
        skeletonAI.onEnemyAttack -= skeletonAI_OnEnemyAttack;
    }

    private void Update()
    {
        animator.SetBool("IsRunning", skeletonAI.IsRunning);
        animator.SetFloat("ChasingSpeedMultiplier", skeletonAI.GetRoamingAnimationSpeed());
        
    }

    public void TriggerGetScore()
    {
        ScoreScript.scoreValue++;
        Debug.Log("death");
    }

    public void TriggerAttackAnimationTurnOff()
    {
        enemyEntity.PolygonColliderTurnOff();
    }

    public void TriggerAttackAnimationTurnOn()
    {
        enemyEntity.PolygonColliderTurnOn();
    }
}
