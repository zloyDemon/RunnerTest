using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
 * Класс view для игрового UI, который содержит кнопки управления
 */
public class GameUI : MonoBehaviour
{
    [SerializeField] private RectTransform jumpPanel;
    [SerializeField] private RectTransform upDownPanel;
    [SerializeField] private HoldButton jumpButton;
    [SerializeField] private HoldButton upButton;
    [SerializeField] private HoldButton downButton;

    public Vector2 MoveVector { get; private set; }
    public bool Jump { get; private set; }

    private bool jumpWasPressed;

    private void Awake()
    {
        /*
         * Здесь происходят подписки на события HoldButton
         */
        upButton.PointerUp += OnButtonPointerUp;
        upButton.PointerDown += OnUpPointerDown;
        downButton.PointerUp += OnButtonPointerUp;
        downButton.PointerDown += OnDownPointerDown;
        jumpButton.PointerUp += OnJumpClickUp;
        jumpButton.PointerDown += OnJumpClickDown;
    }

    private void OnDestroy()
    {
        upButton.PointerUp -= OnButtonPointerUp;
        upButton.PointerDown -= OnUpPointerDown;
        downButton.PointerUp -= OnButtonPointerUp;
        downButton.PointerDown -= OnDownPointerDown;
        jumpButton.PointerUp -= OnJumpClickUp;
        jumpButton.PointerDown -= OnJumpClickDown;
    }

    // В методе Update переменная jumpWasPressed служит для обозначения нажатия кнопки один раз
    private void Update()
    {
        if (jumpWasPressed)
        {
            Jump = true;
        }
        else
        {
            Jump = false;
        }

        jumpWasPressed = false;
    }

    // При смене абилки включается нужное отоюражение управления
    public void SetControlByAbilityType(AbilityType type)
    {
        switch (type)
        {
            case AbilityType.DefaultMove:
            case AbilityType.UpSpeed:
            case AbilityType.DownSpeed:
                jumpPanel.gameObject.SetActive(true);
                upDownPanel.gameObject.SetActive(false);
                break;
            case AbilityType.Fly:
                jumpPanel.gameObject.SetActive(false);
                upDownPanel.gameObject.SetActive(true);
                MoveVector = Vector2.zero;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }

    private void OnUpPointerDown(HoldButton hb, PointerEventData ped)
    {
        MoveVector = new Vector2(0, 1);
    }
    
    private void OnButtonPointerUp(HoldButton hb, PointerEventData ped)
    {
        MoveVector = new Vector2(0, 0);
    }
    
    private void OnDownPointerDown(HoldButton hb, PointerEventData ped)
    {
        MoveVector = new Vector2(0, -1);
    }

    private void OnJumpClickDown(HoldButton hb, PointerEventData ped)
    {
        jumpWasPressed = true;
    }
    
    private void OnJumpClickUp(HoldButton hb, PointerEventData ped)
    {
        jumpWasPressed = false;
    }
}
