using UnityEngine;

// Абилка для полета героя
public class FlyAbility : BaseAbility
{
    private const float YMaxValue = 2.5f;
    
    private Rigidbody2D _playerRigidbody2D;
    private IPlayerInput _playerInput;
    private float _flySpeed;
    private float _startFlyInterpolation;
    
    public override void StartAbility()
    {
        _playerRigidbody2D = GameService.Instance.PlayerComponents.Rigidbody2D;
        _playerInput = GameService.Instance.PlayerInput;
        _flySpeed = GameService.Instance.GameConfig.UpDownFlySpeed;
        _startFlyInterpolation = 0;
    }

    public override void UpdateAbility(float deltaTime)
    {
        // Этот блок  нужен для поднятия персонажа в воздух, если он ниже координаты 0.5, чисто визуал
        if (_startFlyInterpolation < 1 && _playerRigidbody2D.transform.position.y < -0.5f)
        {
            _startFlyInterpolation += deltaTime;
            _playerRigidbody2D.velocity = Vector2.up * _flySpeed * 2;
            return;
        }

        _startFlyInterpolation = 1;

        // Здесь мы ограничиваем полет персонажа выше определенного значения.
        if (_playerRigidbody2D.transform.position.y > YMaxValue && _playerInput.InputDirection.y > 0)
        {
            _playerRigidbody2D.velocity = Vector2.zero;
            return;
        }
        
        var flyDirection = new Vector2(0, _playerInput.InputDirection.y);
        _playerRigidbody2D.velocity = flyDirection * _flySpeed;
        
    }

    public override void StopAbility() { }
}
