/*
 * Базовый класс для абилок. Имеет ссылку на сервис абилок а также три метода
 * StartAbility запускается когда запускется новая абилка
 * UpdateAbility вызывается каждый кадр при работе абилки
 * StopAbility запускается когда абилка переключается на другую
 */
public abstract class BaseAbility
{
    private AbilitiesService _abilitiesService;

    protected AbilitiesService AbilitiesService => _abilitiesService;

    public void Init(AbilitiesService abilitiesService)
    {
        _abilitiesService = abilitiesService;
    }
    
    public abstract void StartAbility();
    public abstract void UpdateAbility(float deltaTime);
    public abstract void StopAbility();
}
