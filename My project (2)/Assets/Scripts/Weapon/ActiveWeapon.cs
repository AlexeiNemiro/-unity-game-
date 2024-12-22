using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����, ���������� �� �������� ������ ������.
/// </summary>
public class ActiveWeapon : MonoBehaviour
{
    /// <summary>
    /// ��������� ������ ActiveWeapon.
    /// </summary>
    public static ActiveWeapon Instance { get; private set; }

    /// <summary>
    /// ������ �� ������ ����.
    /// </summary>
    [SerializeField] private Sword sword;

    /// <summary>
    /// �����, ���������� ��� ������������� �������.
    /// </summary>
    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// �����, ���������� ������ ���� ��� ���������� ��������� ������.
    /// </summary>
    private void Update()
    {
        if (Player.Instance.IsAlive())
        {
            FollowMousePosition();
        }
    }

    /// <summary>
    /// ���������� �������� ������.
    /// </summary>
    /// <returns>������ ����.</returns>
    public Sword GetActiveWeapon()
    {
        return sword;
    }

    /// <summary>
    /// ����� ��� ���������� ������ �� �������� ����.
    /// </summary>
    private void FollowMousePosition()
    {
        Vector3 mousePos = Player.Instance.GetMousePosition();
        Vector3 playerPosition = Player.Instance.GetPlayerScreenPosition();
        if (mousePos.x < playerPosition.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
