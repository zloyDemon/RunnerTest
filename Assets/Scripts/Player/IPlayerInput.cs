using UnityEngine;

// Интерфейс для сбора ввода игрока
public interface IPlayerInput
{
    Vector2 InputDirection { get; }
    bool IsJump { get; }
}