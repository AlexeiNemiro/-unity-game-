using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Класс, представляющий игрока.
/// </summary>
public class Player : MonoBehaviour
{
    /// <summary>
    /// Экземпляр класса Player.
    /// </summary>
    public static Player Instance { get; private set; }

    /// <summary>
    /// Событие, вызываемое при смерти игрока.
    /// </summary>
    public event EventHandler OnPlayerDeath;

    /// <summary>
    /// Событие, вызываемое при получении урона игроком.
    /// </summary>
    public event EventHandler OnPlayerTakeHit;

    /// <summary>
    /// Скорость передвижения игрока.
    /// </summary>
    [SerializeField] private float movingSpeed = 5f;

    /// <summary>
    /// Максимальное здоровье игрока.
    /// </summary>
    [SerializeField] public int maxHealth = 30;

    /// <summary>
    /// Время восстановления после получения урона.
    /// </summary>
    [SerializeField] private float damageRecoveryTime = 0.5f;

    /// <summary>
    /// Скрипт для обработки окончания игры.
    /// </summary>
    public GameOverScript gameOverScript;

    private PlayerInputActions playerInputActions;
    private EnemyEntity enemyEntity;

    /// <summary>
    /// Событие, вызываемое при атаке игрока.
    /// </summary>
    public event EventHandler OnPlayerAttack;

    private Rigidbody2D rb;
    private KnockBack knockBack;
    private float minMovingSpeed = 0.001f;
    private bool isRunning = false;
    public int currentHealth;
    private bool canTakeDamage;
    private bool isAlive = true;

    /// <summary>
    /// Метод, вызываемый при инициализации объекта.
    /// </summary>
    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        knockBack = GetComponent<KnockBack>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
        playerInputActions.Combat.Attack.started += PlayerAttack_started;

        Logger.Log("Player initialized.");
    }

    /// <summary>
    /// Метод, вызываемый при старте игры.
    /// </summary>
    private void Start()
    {
        Instance.OnPlayerAttack += Player_OnPlayerAttack;
        currentHealth = maxHealth;
        canTakeDamage = true;

        Logger.Log("Player started with max health: " + maxHealth);
    }

    /// <summary>
    /// Проверяет, жив ли игрок.
    /// </summary>
    /// <returns>Возвращает true, если игрок жив.</returns>
    public bool IsAlive()
    {
        return isAlive;
    }

    private void Player_OnPlayerAttack(object sender, EventArgs e)
    {
        if (isAlive)
        {
            ActiveWeapon.Instance.GetActiveWeapon().Attack();
            Logger.Log("Player attacked.");
        }
    }

    private void PlayerAttack_started(InputAction.CallbackContext obj)
    {
        OnPlayerAttack?.Invoke(this, EventArgs.Empty);
    }

    private void FixedUpdate()
    {
        if (knockBack.IsGettingBack) return;
        if (isAlive)
        {
            HandleMovement();
        }
    }

    /// <summary>
    /// Обрабатывает передвижение игрока.
    /// </summary>
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
        isRunning = Mathf.Abs(inputVector.x) > minMovingSpeed || Mathf.Abs(inputVector.y) > minMovingSpeed;

        
    }

    /// <summary>
    /// Проверяет, бежит ли игрок.
    /// </summary>
    /// <returns>Возвращает true, если игрок бежит.</returns>
    public bool IsRunning()
    {
        return isRunning;
    }

    /// <summary>
    /// Получает позицию мыши.
    /// </summary>
    /// <returns>Возвращает позицию мыши.</returns>
    public Vector3 GetMousePosition()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        return mousePos;
    }

    /// <summary>
    /// Получает экранную позицию игрока.
    /// </summary>
    /// <returns>Возвращает экранную позицию игрока.</returns>
    public Vector3 GetPlayerScreenPosition()
    {
        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
        return playerScreenPosition;
    }

    /// <summary>
    /// Отключает передвижение игрока.
    /// </summary>
    private void DisableMovement()
    {
        playerInputActions.Disable();
        Logger.Log("Player movement disabled.");
    }

    /// <summary>
    /// Проверяет смерть игрока.
    /// </summary>
    private void DetectDeath()
    {
        if (currentHealth == 0 && isAlive)
        {
            isAlive = false;
            knockBack.StopKnockBackMovement();
            OnPlayerDeath?.Invoke(this, EventArgs.Empty);
            gameOverScript.GameOverPlayer();

            Logger.Log("Player died.");
        }
    }

    /// <summary>
    /// Обрабатывает получение урона игроком.
    /// </summary>
    /// <param name="damageSource">Источник урона.</param>
    /// <param name="damage">Количество урона.</param>
    public void TakeDamage(Transform damageSource, int damage)
    {
        if (canTakeDamage && isAlive)
        {
            canTakeDamage = false;
            currentHealth = Mathf.Max(0, currentHealth -= damage);
            knockBack.GetKnockedBack(damageSource);
            OnPlayerTakeHit?.Invoke(this, EventArgs.Empty);
            StartCoroutine(DamageRecoveryRoutine());

            Logger.Log("Player took damage: " + damage + ", current health: " + currentHealth);
        }
        DetectDeath();
    }

    /// <summary>
    /// Корутин для восстановления после получения урона.
    /// </summary>
    /// <returns>Возвращает IEnumerator.</returns>
    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;

        Logger.Log("Player recovered from damage.");
    }
}
