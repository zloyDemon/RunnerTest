using UnityEngine;

// Абилка для движения персонажа по умолчанию
public class DefaultMoveAbility : BaseAbility
{
    private Rigidbody2D _playerRigidbody;
    private Vector2 _gravityVector;
    private float _fallMultiplier;
    private GameConfig _gameConfig;
    private BoxCollider2D _boxCollider2D;
    private IPlayerInput _playerInput;
    private float _jumpPower;
    private bool _isJumping;
    private float _jumpCounter;
    private float _jumpMultiplier;
    private float _jumpTime;

    public override void StartAbility()
    {
        _gameConfig = GameService.Instance.GameConfig;
        _fallMultiplier = _gameConfig.FallMultiplier;
        _playerRigidbody = GameService.Instance.PlayerComponents.Rigidbody2D;
        _boxCollider2D = GameService.Instance.PlayerComponents.PlayerBoxCollider;
        _playerInput = GameService.Instance.PlayerInput;
        _gravityVector = new Vector2(0, -Physics2D.gravity.y);
        _playerRigidbody.gravityScale = 1;
        _jumpPower = _gameConfig.JumpPower;
        _jumpMultiplier = _gameConfig.JumpMultiplier;
        _jumpTime = _gameConfig.JumpTime;
    }

    public override void UpdateAbility(float deltaTime)
    {
        Jump();
    }

    public override void StopAbility()
    {
        _playerRigidbody.gravityScale = 0;
    }

    //Метод проверки персонажа на то, находится ли она на повернхости
    private bool IsGrounded()
    {
        RaycastHit2D hit2D = Physics2D.BoxCast(_boxCollider2D.bounds.center, _boxCollider2D.bounds.size, .1f, Vector2.down, 0.25f);
        return hit2D.collider != null;
    }

    // Метод для прыжка персонажа
    private void Jump()
    {
        if (_playerInput.IsJump && IsGrounded())
        { 
            _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, _jumpPower);
            _isJumping = true;
            _jumpCounter = 0;
        }

        if (_playerRigidbody.velocity.y > 0 && _isJumping)
        {
            _jumpCounter += Time.deltaTime;
            if (_jumpCounter > _jumpTime)
            {
                _isJumping = false;
            }

            float t = _jumpCounter / _jumpTime;
            float currentJumpMultiplier = _jumpMultiplier;

            if (t > 0.5f)
            {
                currentJumpMultiplier = _jumpMultiplier * (1 - t);
            }
            
            _playerRigidbody.velocity += _gravityVector * currentJumpMultiplier * Time.deltaTime;
        }

        if (_playerRigidbody.velocity.y < 0)
        {
            _playerRigidbody.velocity -= _gravityVector * (_fallMultiplier * Time.deltaTime);
        }

        if (_playerInput.IsJump)
        {
            _isJumping = false;
            _jumpCounter = 0;

            if (_playerRigidbody.velocity.y > 0)
            {
                _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, _playerRigidbody.velocity.y * 0.8f);
            }
        }
    }
}
