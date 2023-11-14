using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;


//Класс для хранения и переключения между абилками героя
public class AbilitiesService
{
    private Dictionary<AbilityType, BaseAbility> _abilities = new();
    private BaseAbility _currentAbility;
    private CancellationTokenSource _cancellationTokenSource;

    public BaseAbility CurrentAbility => _currentAbility;

    public event Action<AbilityType> AbilityChanged;

    public void Init()
    {
        AddAbility(AbilityType.DefaultMove, new DefaultMoveAbility());
        AddAbility(AbilityType.Fly, new FlyAbility());
        AddAbility(AbilityType.UpSpeed, new UpSpeedAbility());
        AddAbility(AbilityType.DownSpeed, new DownSpeedAbility());
    }
    
    public void AddAbility(AbilityType type, BaseAbility ability)
    {
        _abilities.TryAdd(type, ability);
        ability.Init(this);
    }

    public void StarAbilityByType(AbilityType type)
    {
        if (_abilities.TryGetValue(type, out BaseAbility ability))
        {
            _currentAbility?.StopAbility();

            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = null;
            }

            if (type != AbilityType.DefaultMove)
            {
                _cancellationTokenSource = new CancellationTokenSource();
                StartAbilityWorkingTimer().Forget();
            }
            
            _currentAbility = ability;
            _currentAbility.StartAbility();
            AbilityChanged?.Invoke(type);
        }
    }

    public T GetAbility<T>() where T : BaseAbility
    {
        var abilityKvp = _abilities.FirstOrDefault(e => e.Value.GetType() == typeof(T));
        return abilityKvp.Value as T;
    }

    private async UniTask StartAbilityWorkingTimer()
    {
        var seconds = GameService.Instance.GameConfig.AbilityActingSeconds;
        await UniTask.Delay(seconds * 1000, cancellationToken: _cancellationTokenSource.Token);
        StarAbilityByType(AbilityType.DefaultMove);
    }
}
