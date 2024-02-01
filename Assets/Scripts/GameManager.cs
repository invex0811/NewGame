using UnityEngine;

public static class GameManager
{
    private static bool isGamePaused = false;

    public static bool IsGamePaused
    {
        get { return isGamePaused; }
    }

    public static void TogglePause()
    {
        if (isGamePaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public static void EnableCameraControl(Camera camera)
    {
        SetCameraControl(true,camera);

    }

    public static void DisableCameraControl(Camera camera)
    {
        SetCameraControl(false,camera);

    }

    private static void SetCameraControl(bool enable, Camera camera)
    {

        if (camera == null)
        {
            Debug.LogError("Main Camera not found!");
            return;
        }

        CameraController cameraController = camera.GetComponent<CameraController>();

        if (cameraController == null)
        {
            Debug.LogError("CameraController script not found on the object with tag 'Main Camera'!");
            return;
        }

        cameraController.enabled = enable;
    }

    public static void PerformPauseActions()
    {
        // ƒополнительные действи€ при паузе (если нужны)
        // ...
    }

    public static void PerformResumeActions()
    {
        // ƒополнительные действи€ при возобновлении игры (если нужны)
        // ...
    }

    private static void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0f;
        // ƒополнительные действи€ при паузе (если нужны)
        // ...

        // ¬аш код дл€ отключени€ управлени€ игроком и камерой
    }

    private static void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1f;
        // ƒополнительные действи€ при возобновлении игры (если нужны)
        // ...

        // ¬аш код дл€ включени€ управлени€ игроком и камерой
    }
    public static void DisablePlayerControl()
    {
        PlayerController playerController = GetPlayerController();

        if (playerController != null)
        {
            playerController.enabled = false;
        }
        else
        {
            Debug.LogError("PlayerController script not found on player object!");
        }
    }

    public static void EnablePlayerControl()
    {
        PlayerController playerController = GetPlayerController();

        if (playerController != null)
        {
            playerController.enabled = true;
        }
        else
        {
            Debug.LogError("PlayerController script not found on player object!");
        }
    }

    private static PlayerController GetPlayerController()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            return player.GetComponent<PlayerController>();
        }
        else
        {
            Debug.LogError("Player object is null or not found!");
            return null;
        }
    }
}

