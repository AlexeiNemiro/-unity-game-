using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SkeletonAI))]
public class EnemyEntity : MonoBehaviour
{
    public event EventHandler OnTakeHit;
    public event EventHandler OnDeath;
    [SerializeField] public int maxHealth;
    [SerializeField] private int damageAmount = 2;
    public int currentHealth;
    public int score = 0;
    public TextMeshProUGUI scoreText;

    private PolygonCollider2D polygonCollider;
    private BoxCollider2D boxCollider;
    private SkeletonAI skeletonAI;

    private void Awake()
    {
        polygonCollider = GetComponent<PolygonCollider2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        skeletonAI = GetComponent<SkeletonAI>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        OnTakeHit?.Invoke(this, EventArgs.Empty);
        DetectDeath();
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
        {
            player.TakeDamage(transform, damageAmount);
        }
    }

    private void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            boxCollider.enabled = false;
            polygonCollider.enabled = false;
            
            skeletonAI.SetDeathState();
            
            OnDeath?.Invoke(this, EventArgs.Empty);
            // Увеличиваем score и обновляем текст
            

        }
    }

    


    public void PolygonColliderTurnOff()
    {
        polygonCollider.enabled=false;
    }

    public void PolygonColliderTurnOn()
    {
        polygonCollider.enabled = true;
    }
}
