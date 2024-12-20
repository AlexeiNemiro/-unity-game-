using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private int damageAmount=2;
    public event EventHandler OnSwordSwing;

    private PolygonCollider2D polygonCollider2D;

    

    private void Awake()
    {
        polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    private void Start()
    {
        AttackColliderTurnOff();
    }
    public void Attack()
    {
        AttackColliderTurnOn();
        OnSwordSwing?.Invoke(this, EventArgs.Empty);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out EnemyEntity enemyEntity))
        {
            enemyEntity.TakeDamage(damageAmount);
            
        }
    }

    public void AttackColliderTurnOff()
    {
        polygonCollider2D.enabled = false;
    }

    public void AttackColliderTurnOn()
    {
        polygonCollider2D.enabled = true;
    }

}