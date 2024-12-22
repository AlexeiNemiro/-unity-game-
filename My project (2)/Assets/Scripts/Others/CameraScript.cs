using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс, отвечающий за следование камеры за игроком.
/// </summary>
public class CameraScript : MonoBehaviour
{
    private Transform player;

    /// <summary>
    /// Метод, вызываемый при старте игры.
    /// Находит объект игрока и сохраняет его трансформ.
    /// </summary>
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    /// <summary>
    /// Метод, вызываемый каждый кадр для обновления позиции камеры.
    /// Следует за позицией игрока.
    /// </summary>
    private void Update()
    {
        Vector3 temp = transform.position;
        temp.x = player.position.x;
        temp.y = player.position.y;
        transform.position = temp;
    }
}
