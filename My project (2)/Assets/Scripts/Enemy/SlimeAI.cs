using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Adventures.Utils;

/// <summary>
/// �����, ���������� �� ��������� �����.
/// </summary>
public class SlimeAI : MonoBehaviour
{
    /// <summary>
    /// ��������� ��������� �����.
    /// </summary>
    [SerializeField] private State startingState;

    /// <summary>
    /// ������������ ��������� ���������.
    /// </summary>
    [SerializeField] private float roamingDistanceMax = 7f;

    /// <summary>
    /// ����������� ��������� ���������.
    /// </summary>
    [SerializeField] private float roamingDistanceMin = 3f;

    /// <summary>
    /// ������������ ����� ���������.
    /// </summary>
    [SerializeField] private float roamingTimerMax = 15f;

    private NavMeshAgent navMeshAgent;
    private State state;
    private float roamingTime;
    private Vector3 roamPosition;
    private Vector3 startPosition;

    /// <summary>
    /// ������������ ��������� �����.
    /// </summary>
    private enum State
    {
        Roaming
    }

    /// <summary>
    /// �����, ���������� ��� ������������� �������.
    /// </summary>
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        state = startingState;
    }

    /// <summary>
    /// �����, ���������� ������ ���� ��� ���������� ��������� �����.
    /// </summary>
    private void Update()
    {
        switch (state)
        {
            case State.Roaming:
                roamingTime -= Time.deltaTime;
                if (roamingTime < 0)
                {
                    Roaming();
                    roamingTime = roamingTimerMax;
                }
                break;
        }
    }

    /// <summary>
    /// ����� ��� ��������� ��������� �����.
    /// </summary>
    private void Roaming()
    {
        startPosition = transform.position;
        roamPosition = GetRoamPosition();
        ChangeFacingDirection(startPosition, roamPosition);
        navMeshAgent.SetDestination(roamPosition);
    }

    /// <summary>
    /// �������� ��������� ������� ��� ���������.
    /// </summary>
    /// <returns>��������� ������� ��� ���������.</returns>
    private Vector3 GetRoamPosition()
    {
        return startPosition + Utils.GetRandomDir() * UnityEngine.Random.Range(roamingDistanceMin, roamingDistanceMax);
    }

    /// <summary>
    /// ������ ����������� ������� �����.
    /// </summary>
    /// <param name="sourcePosition">�������� �������.</param>
    /// <param name="targetPosition">������� �������.</param>
    private void ChangeFacingDirection(Vector3 sourcePosition, Vector3 targetPosition)
    {
        if (sourcePosition.x > targetPosition.x)
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
