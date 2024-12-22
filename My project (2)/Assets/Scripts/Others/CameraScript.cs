using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����, ���������� �� ���������� ������ �� �������.
/// </summary>
public class CameraScript : MonoBehaviour
{
    private Transform player;

    /// <summary>
    /// �����, ���������� ��� ������ ����.
    /// ������� ������ ������ � ��������� ��� ���������.
    /// </summary>
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    /// <summary>
    /// �����, ���������� ������ ���� ��� ���������� ������� ������.
    /// ������� �� �������� ������.
    /// </summary>
    private void Update()
    {
        Vector3 temp = transform.position;
        temp.x = player.position.x;
        temp.y = player.position.y;
        transform.position = temp;
    }
}
