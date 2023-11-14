using UnityEngine;

// Компонент для работы с камерой
[RequireComponent(typeof(Camera))]
public class GameCamera : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    
    private float _xEdgeCamera;
    
    // Возвращет игровую камеру
    public Camera MainCamera => _mainCamera;
    //Возвращает координату правой границы камеры
    public float XEdgeCamera => _xEdgeCamera;
    
    private void Awake()
    {
        // вычисление правой границы камеры
        _xEdgeCamera = ((float) Screen.width / Screen.height) * _mainCamera.orthographicSize;    
    }
}
