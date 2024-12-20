using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    public static Player Instance {  get; private set; }
    public event EventHandler OnPlayerDeath;
    public event EventHandler OnPlayerTakeHit;

    [SerializeField] private float movingSpeed = 5f;
    [SerializeField] public int maxHealth=30;
    [SerializeField] private float damageRecoveryTime = 0.5f;

    public GameOverScript gameOverScript;


    private PlayerInputActions playerInputActions;
    

    private EnemyEntity enemyEntity;

    public event EventHandler OnPlayerAttack;
    

    private Rigidbody2D rb;
    private KnockBack knockBack;

    private float minMovingSpeed = 0.001f;
    private bool isRunning = false;
    public int currentHealth;
    private bool canTakeDamage;
    private bool isAlive=true;

    

    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        knockBack = GetComponent<KnockBack>();
        playerInputActions =new PlayerInputActions();
        
        playerInputActions.Enable();
        playerInputActions.Combat.Attack.started += PlayerAttack_started;
        
    }

    private void Start()
    {
        Instance.OnPlayerAttack += Player_OnPlayerAttack;
        currentHealth = maxHealth;
        canTakeDamage = true;
    }

    public bool IsAlive()
    {
        return isAlive;
    }
    private void Player_OnPlayerAttack(object sender, System.EventArgs e)
    {
        if (isAlive)
        {
            ActiveWeapon.Instance.GetActiveWeapon().Attack();
        }
        
    }

    private void PlayerAttack_started(InputAction.CallbackContext obj)
    {
        
       OnPlayerAttack?.Invoke(this, EventArgs.Empty);
    }

    //private Vector2 GetMovementVector()
    //{
    //    Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
    //    return inputVector;
    //}
    // Update is called once per frame
    private void FixedUpdate()
    {
        
        if (knockBack.IsGettingBack) return;
        if (isAlive)
        {
            HandleMovement();
        }


        }
    private void HandleMovement()
    {
        Vector2 inputVector = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y = 1f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = 1f;
        }
        inputVector = inputVector.normalized;
        rb.MovePosition(rb.position + inputVector * movingSpeed * Time.fixedDeltaTime);
        if(Mathf.Abs(inputVector.x) > minMovingSpeed || Mathf.Abs(inputVector.y)> minMovingSpeed)
        {
            isRunning = true;
        } else {isRunning = false; }
    }
    public bool IsRunning()
    {
        return isRunning;
    }

    public Vector3 GetMousePosition()
    {
        Vector3 mousePos= Mouse.current.position.ReadValue();
        return mousePos;
    }

    public Vector3 GetPlayerScreenPosition()
    {
        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
       
        return playerScreenPosition;
    }

    private void DisableMovement()
    {
        playerInputActions.Disable();
    }

    private void DetectDeath()
    {
        if (currentHealth == 0 && isAlive) { 
            isAlive=false;
            knockBack.StopKnockBackMovement();
            OnPlayerDeath?.Invoke(this,EventArgs.Empty);
            gameOverScript.GameOverPlayer();
        }
    }


    public void TakeDamage(Transform damageSource, int damage)
    {
        if (canTakeDamage && isAlive)
        {
            canTakeDamage = false;
            currentHealth = Mathf.Max(0, currentHealth -= damage);
            knockBack.GetKnockedBack(damageSource);
            OnPlayerTakeHit?.Invoke(this, EventArgs.Empty);

            StartCoroutine(DamageRecoveryRoutine());
        }
        DetectDeath();
    }

    
    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }
    
}
