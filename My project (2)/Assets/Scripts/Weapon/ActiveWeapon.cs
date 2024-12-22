using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс, отвечающий за активное оружие игрока.
/// </summary>
public class ActiveWeapon : MonoBehaviour
{
    /// <summary>
    /// Экземпляр класса ActiveWeapon.
    /// </summary>
    public static ActiveWeapon Instance { get; private set; }

    /// <summary>
    /// Ссылка на объект меча.
    /// </summary>
    [SerializeField] private Sword sword;

    /// <summary>
    /// Метод, вызываемый при инициализации объекта.
    /// </summary>
    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Метод, вызываемый каждый кадр для обновления состояния оружия.
    /// </summary>
    private void Update()
    {
        if (Player.Instance.IsAlive())
        {
            FollowMousePosition();
        }
    }

    /// <summary>
    /// Возвращает активное оружие.
    /// </summary>
    /// <returns>Объект меча.</returns>
    public Sword GetActiveWeapon()
    {
        return sword;
    }

    /// <summary>
    /// Метод для следования оружия за позицией мыши.
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
