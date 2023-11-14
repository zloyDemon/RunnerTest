// Абилка героя которая замедляет время
public class DownSpeedAbility : BaseAbility
{
    private PlayerComponents _playerComponents;
    private GameConfig _gameConfig;
    private DefaultMoveAbility _defaultMoveAbility;
    
    public override void StartAbility()
    {
        _playerComponents = GameService.Instance.PlayerComponents;
        _gameConfig = GameService.Instance.GameConfig;
        _playerComponents.MoveSpeed = _gameConfig.MoveSpeed / _gameConfig.DownSpeedDivider;
        _defaultMoveAbility = AbilitiesService.GetAbility<DefaultMoveAbility>();
        _defaultMoveAbility.StartAbility();
    }

    public override void UpdateAbility(float deltaTime)
    {
        _defaultMoveAbility.UpdateAbility(deltaTime);
    }

    public override void StopAbility()
    {
        _defaultMoveAbility.StopAbility();
        _playerComponents.MoveSpeed = _gameConfig.MoveSpeed;
    }
}
