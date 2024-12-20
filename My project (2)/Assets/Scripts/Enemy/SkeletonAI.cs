using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Adventures.Utils;
using UnityEngine.InputSystem.XR.Haptics;
using System;

public class SkeletonAI : MonoBehaviour
{
    //параметры логики 
    [SerializeField] private State startingState;
    [SerializeField] private float roamingDistanceMax = 7f;
    [SerializeField] private float roamingDistanceMin = 3f;
    [SerializeField] private float roamingTimerMax = 10f;
    [SerializeField] private bool isChasingEnemy=false;
    [SerializeField] private float chasingDistance = 4f;
    private float chasingSpeedMultiplier=2f;

    [SerializeField] private bool isAttackingEnemy=false;
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

    

    //состояния
    public enum State
    {
        Idle,
        Roaming,
        Chasing,
        Attacking,
        Death
    }

    private void Awake()
    {
        enemyEntity = GetComponent<EnemyEntity>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation=false;
        navMeshAgent.updateUpAxis=false;
        currentState = startingState;
        roamingSpeed= navMeshAgent.speed;
        chasingSpeed=navMeshAgent.speed*chasingSpeedMultiplier;
    }

    

    private void Update()
    {
        StateHandler();
        MuvementDuractonHandler();
    }

    //типы состояний
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

    private void CheckCurrentState()
    {
        float distanceToPlayer=Vector3.Distance(transform.position, Player.Instance.transform.position);
        State newstate=State.Roaming;
        if (isChasingEnemy)
        {
            if (distanceToPlayer <= chasingDistance)
            {
                newstate=State.Chasing;
            }
        }
        if (isAttackingEnemy)
        {
            if (distanceToPlayer <= atackingDistance) { 
                newstate = State.Attacking;
            }
        }
        if (newstate != currentState) {
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
            else if (newstate == State.Attacking) {
                navMeshAgent.ResetPath();
            }
            currentState = newstate;
        }
    }

    private void MuvementDuractonHandler()
    {
        if (Time.time > nextCheckDirectionTime)
        {
            if (IsRunning)
            {
                ChangeFacingDirection(lastPostition, transform.position);
            }
            else if (currentState == State.Attacking) {
                ChangeFacingDirection(transform.position, Player.Instance.transform.position);
            }
            lastPostition = transform.position;
            nextCheckDirectionTime = Time.time+checkDirectionDuration;
        }
    }
    //преследование игрока
    private void ChasingTarget()
    {
        navMeshAgent.SetDestination(Player.Instance.transform.position);
    }

    public float GetRoamingAnimationSpeed()
    {
        return navMeshAgent.speed/roamingSpeed;
    }
    public bool IsRunning{
        get {
            if (navMeshAgent.velocity == Vector3.zero)
            {
                return false;
            }
            else { return true; }
        }
        
        }

    public void SetDeathState()
    {
        navMeshAgent.ResetPath();
        currentState = State.Death;
        
        

    }

   

        private void AttackingTarget()
    {
        if (Time.time > nextAttackTime)
        {
            onEnemyAttack?.Invoke(this, EventArgs.Empty);
            nextAttackTime = Time.time + attackRate;
        }
        
        
    }

    private void Roaming()
    {
        startPosition=transform.position;
        roamPosition = GetRoamPosition();
        ChangeFacingDirection(startPosition, roamPosition);
        navMeshAgent.SetDestination(roamPosition);
    }


    private Vector3 GetRoamPosition()
    {
        return startPosition + Utils.GetRandomDir()* UnityEngine.Random.Range(roamingDistanceMin, roamingDistanceMax);
    }

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
