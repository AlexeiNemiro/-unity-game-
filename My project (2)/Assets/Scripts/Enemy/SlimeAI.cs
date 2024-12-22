using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Adventures.Utils;

/// <summary>
/// Класс, отвечающий за поведение слизи.
/// </summary>
public class SlimeAI : MonoBehaviour
{
    /// <summary>
    /// Начальное состояние слизи.
    /// </summary>
    [SerializeField] private State startingState;

    /// <summary>
    /// Максимальная дистанция блуждания.
    /// </summary>
    [SerializeField] private float roamingDistanceMax = 7f;

    /// <summary>
    /// Минимальная дистанция блуждания.
    /// </summary>
    [SerializeField] private float roamingDistanceMin = 3f;

    /// <summary>
    /// Максимальное время блуждания.
    /// </summary>
    [SerializeField] private float roamingTimerMax = 15f;

    private NavMeshAgent navMeshAgent;
    private State state;
    private float roamingTime;
    private Vector3 roamPosition;
    private Vector3 startPosition;

    /// <summary>
    /// Перечисление состояний слизи.
    /// </summary>
    private enum State
    {
        Roaming
    }

    /// <summary>
    /// Метод, вызываемый при инициализации объекта.
    /// </summary>
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        state = startingState;
    }

    /// <summary>
    /// Метод, вызываемый каждый кадр для обновления состояния слизи.
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
    /// Метод для обработки блуждания слизи.
    /// </summary>
    private void Roaming()
    {
        startPosition = transform.position;
        roamPosition = GetRoamPosition();
        ChangeFacingDirection(startPosition, roamPosition);
        navMeshAgent.SetDestination(roamPosition);
    }

    /// <summary>
    /// Получает случайную позицию для блуждания.
    /// </summary>
    /// <returns>Случайная позиция для блуждания.</returns>
    private Vector3 GetRoamPosition()
    {
        return startPosition + Utils.GetRandomDir() * UnityEngine.Random.Range(roamingDistanceMin, roamingDistanceMax);
    }

    /// <summary>
    /// Меняет направление взгляда слизи.
    /// </summary>
    /// <param name="sourcePosition">Исходная позиция.</param>
    /// <param name="targetPosition">Целевая позиция.</param>
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
