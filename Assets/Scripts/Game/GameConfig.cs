using UnityEngine;

/*
 * Игровой конфиг. Содержит числовые параметры для логики игры
 */

[CreateAssetMenu(fileName = "GameConfig", menuName = "Project/GameConfig", order = 1)]
public class GameConfig : ScriptableObject
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float fallMultiplier;
    [SerializeField] private float upDownFlySpeed;

    [Header("Spawn Coin")] 
    [SerializeField] private float yMinValue;
    [SerializeField] private float yMaxValue;
    [SerializeField] private float xSpawnOffset;
    [SerializeField] private int minSecondsBetweenSpawn;
    [SerializeField] private int maxSecondsBetweenSpawn;

    [Header("Ability")] 
    [SerializeField] private int abilityActingSeconds;
    [SerializeField] private float upSpeedMultiplier;
    [SerializeField] private float downSpeedDivider;

    [Header("Jump")] 
    [SerializeField] private float jumpPower;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private float jumpTime;

    public float MoveSpeed => moveSpeed;
    public float FallMultiplier => fallMultiplier;
    public float UpDownFlySpeed => upDownFlySpeed;
    public float YMinValue => yMinValue;
    public float YMaxValue => yMaxValue;
    public float XSpawnOffset => xSpawnOffset;
    public int MinSecondsBetweenSpawn => minSecondsBetweenSpawn;
    public int MaxSecondsBetweenSpawn => maxSecondsBetweenSpawn;
    public int AbilityActingSeconds => abilityActingSeconds;
    public float UpSpeedMultiplier => upSpeedMultiplier;
    public float DownSpeedDivider => downSpeedDivider;
    public float JumpPower => jumpPower;
    public float JumpMultiplier => jumpMultiplier;
    public float JumpTime => jumpTime;
}
