using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс, отвечающий за спавн врагов.
/// </summary>
public class Spawner : MonoBehaviour
{
    /// <summary>
    /// Массив объектов врагов для спавна.
    /// </summary>
    public GameObject[] enemy;

    /// <summary>
    /// Массив точек спавна.
    /// </summary>
    public Transform[] spawnPoint;

    private int rand;
    private int randPosition;

    /// <summary>
    /// Начальное время между спавнами.
    /// </summary>
    public float startTimeBtwSpown;

    private float timeBtwSpawns;

    /// <summary>
    /// Метод, вызываемый при старте игры.
    /// Устанавливает начальное время между спавнами.
    /// </summary>
    void Start()
    {
        timeBtwSpawns = startTimeBtwSpown;
    }

    /// <summary>
    /// Метод, вызываемый каждый кадр для обновления состояния спавна.
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
