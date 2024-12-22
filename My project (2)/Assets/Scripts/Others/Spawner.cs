using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����, ���������� �� ����� ������.
/// </summary>
public class Spawner : MonoBehaviour
{
    /// <summary>
    /// ������ �������� ������ ��� ������.
    /// </summary>
    public GameObject[] enemy;

    /// <summary>
    /// ������ ����� ������.
    /// </summary>
    public Transform[] spawnPoint;

    private int rand;
    private int randPosition;

    /// <summary>
    /// ��������� ����� ����� ��������.
    /// </summary>
    public float startTimeBtwSpown;

    private float timeBtwSpawns;

    /// <summary>
    /// �����, ���������� ��� ������ ����.
    /// ������������� ��������� ����� ����� ��������.
    /// </summary>
    void Start()
    {
        timeBtwSpawns = startTimeBtwSpown;
    }

    /// <summary>
    /// �����, ���������� ������ ���� ��� ���������� ��������� ������.
    /// </summary>
    void Update()
    {
        if (timeBtwSpawns <= 0)
        {
            rand = Random.Range(0, enemy.Length);
            randPosition = Random.Range(0, spawnPoint.Length);
            Instantiate(enemy[rand], spawnPoint[randPosition].transform.position, Quaternion.identity);
            timeBtwSpawns = startTimeBtwSpown;
        }
        else
        {
            timeBtwSpawns -= Time.deltaTime;
        }
    }
}
