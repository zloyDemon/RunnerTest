using System;
using TMPro;
using UnityEngine;

// GameObject монетки
public class Coin : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _abilityName;
    
    private AbilityType _abilityType;

    public event Action<Coin> CoinCollected;
    public AbilityType AbilityType => _abilityType;

    // Инициализация монетки
    public void Init(AbilityType abilityType)
    {
        _abilityType = abilityType;
        _abilityName.text = _abilityType.ToString();
    }
    
    // Движение монетки
    private void Update()
    {
        transform.Translate(Vector3.left * GameService.Instance.PlayerComponents.MoveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // При срабатывания триггера с персонажем вызывается event
        CoinCollected?.Invoke(this);
    }
}
