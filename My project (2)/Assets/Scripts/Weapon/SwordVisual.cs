using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordVisual : MonoBehaviour
{
    private Animator animator;
     
    [SerializeField] private Sword sword;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        sword.OnSwordSwing += Sword_OnSwordSwing;
    }
    private void Sword_OnSwordSwing (object sender, System.EventArgs e)
    {
        animator.SetTrigger("Attack");
    }

    public void TriggerEndAnimation()
    {
        sword.AttackColliderTurnOff();
    }
}
