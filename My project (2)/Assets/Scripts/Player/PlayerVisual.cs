using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerVisual : MonoBehaviour
{
    private const string IS_RUNNING = "isRunning";

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    



    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //gameOver = GetComponent<GameOver>();
    }

    private void Start()
    {
        Player.Instance.OnPlayerDeath += Player_OnPlayerDeath;
        Player.Instance.OnPlayerTakeHit += Player_OnPlayerTakeHit;
    }

    //public void GameOverRestart()
    //{
    //    gameOver.GameOverPlayer();
    //}

    private void Player_OnPlayerDeath(object sender, System.EventArgs e)
    {
        animator.SetBool("IsDie", true);
        
    }

    private void Player_OnPlayerTakeHit(object sender, System.EventArgs e)
    {
        animator.SetTrigger("TakeHit");
    }

    
    private void Update()
    {
        animator.SetBool(IS_RUNNING, Player.Instance.IsRunning());
        if (Player.Instance.IsAlive())
        {
            AdjustPlayerFacingDirection();
        }
        
    }
    //выбор направления героя
    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Player.Instance.GetMousePosition();
        Vector3 playerPosition= Player.Instance.GetPlayerScreenPosition();
        if (mousePos.x < playerPosition.x)
        {
            spriteRenderer.flipX = true;
        }
        else { spriteRenderer.flipX = false; }
    }

    
}
