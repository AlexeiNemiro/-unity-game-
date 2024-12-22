using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Adventures.Utils;
using UnityEngine.InputSystem.XR.Haptics;
using System;

/// <summary>
/// Класс, управляющий поведением скелета.
/// </summary>
public class SkeletonAI : MonoBehaviour
{
    //параметры логики 
    [SerializeField] private State startingState;
    [SerializeField] private float roamingDistanceMax = 7f;
    [SerializeField] private float roamingDistanceMin = 3f;
    [SerializeField] private float roamingTimerMax = 10f;
    [SerializeField] private bool isChasingEnemy = false;
    [SerializeField] private float chasingDistance = 4f;
    private float chasingSpeedMultiplier = 2f;

    [SerializeField] private bool isAttackingEnemy = false;
    private float atackingDistance = 2f;
    private float attackRate = 1f;
    private float nextAttackTime = 0f;

    private NavMeshAgent navMeshAgent;
    //хранение текущего состояния
    private State currentState;
    private float roamingTime;
    private Vector3 roamPosition;
    private Vector3 startPosition;

    private float roamingSpeed;
    private float chasingSpeed;

    private float nextCheckDirectionTime = 0f;
    private float checkDirectionDuration = 0.1f;
    private Vector3 lastPostition;

    public event EventHandler onEnemyAttack;

    private EnemyEntity enemyEntity;
    private Animator animator;

    /// <summary>
    /// Перечисление состояний скелета.
    /// </summary>
    public enum State
    {
        Idle,
        Roaming,
        Chasing,
        Attacking,
        Death
    }

    /// <summary>
    /// Метод, вызываемый при инициализации объекта.
    /// </summary>
    private void Awake()
    {
        enemyEntity = GetComponent<EnemyEntity>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        currentState = startingState;
        roamingSpeed = navMeshAgent.speed;
        chasingSpeed = navMeshAgent.speed * chasingSpeedMultiplier;

        Logger.Log("SkeletonAI initialized.");
    }

    /// <summary>
    /// Метод, вызываемый каждый кадр.
    /// </summary>
    private void Update()
    {
        StateHandler();
        MuvementDuractonHandler();
    }

    /// <summary>
    /// Обработчик состояний.
    /// </summary>
    private void StateHandler()
    {
        switch (currentState)
        {
            case State.Roaming:
                roamingTime -= Time.deltaTime;
                if (roamingTime < 0)
                {
                    Roaming();
                    roamingTime = roamingTimerMax;
                }
                CheckCurrentState();
                break;
            case State.Idle:
                break;
            case State.Chasing:
                ChasingTarget();
                CheckCurrentState();
                break;
            case State.Attacking:
                AttackingTarget();
                CheckCurrentState();
                break;
            case State.Death:
                break;
        }
    }

    /// <summary>
    /// Проверка текущего состояния.
    /// </summary>
    private void CheckCurrentState()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, Player.Instance.transform.position);
        State newstate = State.Roaming;
        if (isChasingEnemy)
        {
            if (distanceToPlayer <= chasingDistance)
            {
                newstate = State.Chasing;
            }
        }
        if (isAttackingEnemy)
        {
            if (distanceToPlayer <= atackingDistance)
            {
                newstate = State.Attacking;
            }
        }
        if (newstate != currentState)
        {
            if (newstate == State.Chasing)
            {
                navMeshAgent.ResetPath();
                navMeshAgent.speed = chasingSpeed;
            }
            else if (newstate == State.Roaming)
            {
                roamingTime = 0f;
                navMeshAgent.speed = roamingSpeed;
            }
            else if (newstate == State.Attacking)
            {
                navMeshAgent.ResetPath();
            }
            currentState = newstate;

            
        }
    }

    /// <summary>
    /// Обработчик движения.
    /// </summary>
    private void MuvementDuractonHandler()
    {
        if (Time.time > nextCheckDirectionTime)
        {
            if (IsRunning)
            {
                ChangeFacingDirection(lastPostition, transform.position);
            }
            else if (currentState == State.Attacking)
            {
                ChangeFacingDirection(transform.position, Player.Instance.transform.position);
            }
            lastPostition = transform.position;
            nextCheckDirectionTime = Time.time + checkDirectionDuration;
        }
    }

    /// <summary>
    /// Преследование цели.
    /// </summary>
    private void ChasingTarget()
    {
        navMeshAgent.SetDestination(Player.Instance.transform.position);
        Logger.Log("SkeletonAI is chasing the target.");
    }

    /// <summary>
    /// Получение скорости анимации блуждания.
    /// </summary>
    public float GetRoamingAnimationSpeed()
    {
        return navMeshAgent.speed / roamingSpeed;
    }

    /// <summary>
    /// Проверка, бежит ли объект.
    /// </summary>
    public bool IsRunning
    {
        get
        {
            if (navMeshAgent.velocity == Vector3.zero)
            {
                return false;
            }
            else { return true; }
        }
    }

    /// <summary>
    /// Установка состояния смерти.
    /// </summary>
    public void SetDeathState()
    {
        navMeshAgent.ResetPath();
        currentState = State.Death;
        Logger.Log("SkeletonAI has died.");
    }

    /// <summary>
    /// Атака цели.
    /// </summary>
    private void AttackingTarget()
    {
        if (Time.time > nextAttackTime)
        {
            onEnemyAttack?.Invoke(this, EventArgs.Empty);
            nextAttackTime = Time.time + attackRate;
            Logger.Log("SkeletonAI is attacking the target.");
        }
    }

    /// <summary>
    /// Блуждание.
    /// </summary>
    private void Roaming()
    {
        startPosition = transform.position;
        roamPosition = GetRoamPosition();
        ChangeFacingDirection(startPosition, roamPosition);
        navMeshAgent.SetDestination(roamPosition);
        Logger.Log("SkeletonAI is roaming.");
    }

    /// <summary>
    /// Получение позиции для блуждания.
    /// </summary>
    private Vector3 GetRoamPosition()
    {
        return startPosition + Utils.GetRandomDir() * UnityEngine.Random.Range(roamingDistanceMin, roamingDistanceMax);
    }

    /// <summary>
    /// Изменение направления взгляда.
    /// </summary>
    private void ChangeFacingDirection(Vector3 sourcePosition, Vector3 targetPosition)
    {
        if (sourcePosition.x > targetPosition.x)
        {
            //разворачиваем объект skeleton
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
