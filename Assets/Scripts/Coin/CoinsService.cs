using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;


/*
 * Сервис для управления монетками (Сппавн, обработка сбора)
 */
public class CoinsService : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;

    private List<Coin> _coins = new(); // Список монеток, которые были заспавнены.
    private List<Coin> _coinsToDelete = new List<Coin>(); // Список монеток, которые надо удалить

    private GameConfig _gameConfig;
    private bool _isSpawnEnabled = true;
    private CancellationTokenSource _cancellationTokenSource;
    
    private void Awake()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        _gameConfig = GameService.Instance.GameConfig;
        SpawnCoins().Forget();
    }

    private void OnDestroy()
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource?.Dispose();
    }

    // Метод спавна монетки. Конечно вместо Instantiate лучше использовать пулл объектов.
    public void SpawnCoinByType(AbilityType type)
    {
        var coin = Instantiate(_coinPrefab, transform);
        coin.transform.position = GetRandomPositionForCoin();
        coin.Init(type);
        coin.CoinCollected += OnCoinCollected;
        _coins.Add(coin);
    }

    // Callback на то, что персонаж подобрал монетку
    private void OnCoinCollected(Coin coin)
    {
        coin.CoinCollected -= OnCoinCollected;
        GameService.Instance.AbilitiesService.StarAbilityByType(coin.AbilityType);
        _coinsToDelete.Add(coin);
    }

    // Генерируем случайную Y позицию для монетки, исходя из данных конфига. Монетка генерируется на границе камеры + значение XSpawnOffset
    private Vector2 GetRandomPositionForCoin()
    {
        Vector2 randomPosition = Vector2.zero;

        float xValue = GameService.Instance.GameCamera.XEdgeCamera + _gameConfig.XSpawnOffset;
        float yValue = Random.Range(_gameConfig.YMinValue, _gameConfig.YMaxValue);
        randomPosition = new Vector2(xValue, yValue);
        
        return randomPosition;
    }

    // Берем случайное значение секунд между спавном монеток исаходя из данных конфига
    private int GetRandomSecondsBetweenSpawn()
    {
        return Random.Range(_gameConfig.MinSecondsBetweenSpawn, _gameConfig.MaxSecondsBetweenSpawn);
    }

    // Берем случайное значение типа монетки для спавна
    private AbilityType GetRandomType()
    {
        var count = Enum.GetValues(typeof(AbilityType)).Length;
        AbilityType type = (AbilityType)Random.Range(1, count);
        return type;
    }

    private void Update()
    {
        // Проверяем каждую монетку на то, вышла ли она за пределы экрана слева и добавляем ее в список на удаление
        foreach (var coin in _coins)
        {
            if (coin.transform.position.x < -(GameService.Instance.GameCamera.XEdgeCamera + _gameConfig.XSpawnOffset))
            {
                _coinsToDelete.Add(coin);
            }
        }

        /* Проверяем список на удаление. Если список не пустой уничтожаем gameObject и монетку из списка.  
            Здесь тоже лучше вместо Destroy использовать пулл объектов.
        */
        if (_coinsToDelete.Count > 0)
        {
            foreach (var coin in _coinsToDelete)
            {
                _coins.Remove(coin);
                Destroy(coin.gameObject);
            }
            
            _coinsToDelete.Clear();
        }
    }

    // UniTask для спавна монетки
    private async UniTask SpawnCoins()
    {
        while (_isSpawnEnabled)
        {
            var type = GetRandomType();
            SpawnCoinByType(type);
            int seconds = GetRandomSecondsBetweenSpawn();
            await UniTask.Delay(seconds * 1000, cancellationToken: _cancellationTokenSource.Token);
        }
    }
}
