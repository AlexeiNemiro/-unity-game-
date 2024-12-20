using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    [SerializeField] private float knockBackForce=1f;
    [SerializeField] private float knockBackMovingTimerMax=0.3f;

    private float knockBackMovingTimer;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        knockBackMovingTimer -= Time.deltaTime;
        if (knockBackMovingTimer < 0)
        {
            StopKnockBackMovement();
        }
    }

    public bool IsGettingBack { get; private set;}

    public void GetKnockedBack(Transform damageSource)
    {
        IsGettingBack = true;
        knockBackMovingTimer = knockBackMovingTimerMax;
        Vector2 difference =(transform.position - damageSource.position).normalized*knockBackForce/rb.mass;
        rb.AddForce (difference, ForceMode2D.Impulse);
    }

    public void StopKnockBackMovement()
    {
        rb.velocity = Vector2.zero;
        IsGettingBack= false;
    }
}
