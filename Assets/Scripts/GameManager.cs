using UnityEngine;

public static class GameManager
{
    private static bool _isGamePaused = false;
    private static TypesOfControl _typeOfControl = TypesOfControl.PlayerControl;

    public static bool IsGamePaused
    {
        get { return _isGamePaused; }
    }
    public static TypesOfControl TypeOfControl
    {
        get { return _typeOfControl; }
    }

    public static void PauseGame()
    {
        _isGamePaused = true;
    }
    public static void ResumeGame()
    {
        _isGamePaused = false;
    }
    public static void ChangeTypeOfControll(TypesOfControl newType)
    {
        _typeOfControl = newType;
    }
}

public enum TypesOfControl
{
    PlayerControl,
    InventoryControl,
    InteractionControl,
    InspectionControll
}