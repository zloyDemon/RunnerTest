using UnityEngine;

/*
 * Класс который двигает фон игры, симулируя движение игрока.
 * Когда фон достигает определенной позиции, он возвращается на исходную, благодаря которому есть ощущение бесконечности.
 */
public class LocationMover : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    private float _length;
    private float _startpos;
    private Camera _gameCamera;

    private float MoveSpeed => GameService.Instance.PlayerComponents.MoveSpeed;

    private void Awake()
    {
        _startpos = transform.position.x;
        _length = _spriteRenderer.bounds.size.x;
        _gameCamera = GameService.Instance.GameCamera.MainCamera;
    }

    private void Update()
    {
        transform.Translate(Vector3.left * MoveSpeed * Time.deltaTime);
        
        if (transform.position.x + _length < _gameCamera.transform.position.x)
        {
            transform.position = new Vector3(_startpos, transform.position.y, transform.position.z);
        }
    }
}
