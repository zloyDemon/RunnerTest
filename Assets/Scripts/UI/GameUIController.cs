using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Контроллен GameUI
public class GameUIController : IPlayerInput
{
    private GameUI _gameUI;
    private AbilitiesService _abilitiesService;
    
    public GameUIController(GameUI gameUI, AbilitiesService abilitiesService)
    {
        _gameUI = gameUI;
        _abilitiesService = abilitiesService;
        _abilitiesService.AbilityChanged += OnAbilityChanged;
    }

    private void OnAbilityChanged(AbilityType type)
    {
        _gameUI.SetControlByAbilityType(type);
    }

    public Vector2 InputDirection => _gameUI.MoveVector;
    public bool IsJump => _gameUI.Jump;
}
