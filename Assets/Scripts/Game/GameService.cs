using UnityEngine;
/*
 * Класс-серви Singleton, который хранит необходимы классы для быстрого доступа к ним.
 * В подобных кейсах лучше использовать DI(Zenject)
 */
public class GameService : MonoBehaviour
{
    [SerializeField] private PlayerComponents playerComponents;
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private LocationMover locationMover;
    [SerializeField] private GameCamera _gameCamera;
    [SerializeField] private GameUI _gameUI;

    private AbilitiesService _abilitiesService;
    private GameUIController _gameUIController;
    
    public static GameService Instance;
    
    public PlayerComponents PlayerComponents => playerComponents;
    public GameConfig GameConfig => _gameConfig;
    public LocationMover LocationMover => locationMover;
    public AbilitiesService AbilitiesService => _abilitiesService;
    public GameCamera GameCamera => _gameCamera;
    public IPlayerInput PlayerInput => _gameUIController;

    private void Awake()
    {
        Instance = this;
        Init();
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    private void Init()
    {
        _abilitiesService = new AbilitiesService();
        playerComponents.Init(_abilitiesService);
        _abilitiesService.Init();
        _gameUIController = new GameUIController(_gameUI, _abilitiesService);
        playerComponents.MoveSpeed = _gameConfig.MoveSpeed;
        _abilitiesService.StarAbilityByType(AbilityType.DefaultMove);
    }
}
