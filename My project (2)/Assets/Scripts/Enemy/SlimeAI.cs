using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Adventures.Utils;

public class SlimeAI : MonoBehaviour
{
    //параметры логики 
    [SerializeField] private State startingState;
    [SerializeField] private float roamingDistanceMax = 7f;
    [SerializeField] private float roamingDistanceMin = 3f;
    [SerializeField] private float roamingTimerMax = 15f;

    private NavMeshAgent navMeshAgent;
    //хранение текущего состояния
    private State state;
    private float roamingTime;
    private Vector3 roamPosition;
    private Vector3 startPosition;

    //состояния
    private enum State
    {
        
        Roaming
    }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation=false;
        navMeshAgent.updateUpAxis=false;
        state = startingState;
    }

    

    private void Update()
    {
        switch (state)
        {
            
                case State.Roaming:
                roamingTime -=Time.deltaTime;
                if (roamingTime < 0)
                {
                    Roaming();
                    roamingTime = roamingTimerMax;
                }
                break;
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
            //разворачиваем объект slime
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        }
}
