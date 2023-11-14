using System;
using UnityEngine;

// MonoBehaviour, который висит на игроке и содержит необходимы компоненты для их обработки в классах абилок
public class PlayerComponents : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private BoxCollider2D _playerBoxCollider;
    
    private AbilitiesService _abilitiesService;
    public Rigidbody2D Rigidbody2D => _rigidbody2D;
    public BoxCollider2D PlayerBoxCollider => _playerBoxCollider;
    public float MoveSpeed { get; set; }

    public void Init(AbilitiesService abilitiesService)
    {
        MoveSpeed = GameService.Instance.GameConfig.MoveSpeed;
        _abilitiesService = abilitiesService;
    }
    
    private void FixedUpdate()
    {
        _abilitiesService?.CurrentAbility?.UpdateAbility(Time.deltaTime);
    }
}
