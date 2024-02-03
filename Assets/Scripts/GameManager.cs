using UnityEngine;

public static class GameManager
{
    private static Camera _currentCamera = Camera.main;
    private static bool _isGamePaused = false;
    private static TypesOfControl _typeOfControl = TypesOfControl.PlayerControl;

    public static Camera CurrentCamera {  get { return _currentCamera; } }
    public static bool IsGamePaused
    {
        get { return _isGamePaused; }
    }
    public static TypesOfControl TypeOfControl
    {
        get { return _typeOfControl; }
    }
    private static void PauseGame()
    {
        _isGamePaused = true;
        Time.timeScale = 0f;
    }
    private static void ResumeGame()
    {
        _isGamePaused = false;
        Time.timeScale = 1f;
    }

    public static void TogglePause()
    {
        if (_isGamePaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }
    public static void ChangeTypeOfControll(TypesOfControl newType)
    {
        _typeOfControl = newType;
    }
    public static void SwitchCamera(Camera camera)
    {
        _currentCamera = camera;
    }
}

public enum TypesOfControl
{
    PlayerControl,
    InventoryControl
}