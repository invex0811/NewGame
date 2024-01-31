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

    public static void EnableCameraControl()
    {
        SetCameraControl(true);

    }

    public static void DisableCameraControl()
    {
        SetCameraControl(false);

    }

    private static void SetCameraControl(bool enable)
    {
        Camera mainCamera = Camera.main;

        if (mainCamera != null)
        {
            CameraController cameraController = mainCamera.GetComponent<CameraController>();

            if (cameraController != null)
            {
                cameraController.enabled = enable;
            }
            else
            {
                Debug.LogError("CameraController script not found on the object with tag 'Main Camera'!");
            }
        }
        else
        {
            Debug.LogError("Main Camera not found!");
        }
    }

    public static void PerformPauseActions()
    {
        // �������������� �������� ��� ����� (���� �����)
        // ...
    }

    public static void PerformResumeActions()
    {
        // �������������� �������� ��� ������������� ���� (���� �����)
        // ...
    }

    private static void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0f;
        // �������������� �������� ��� ����� (���� �����)
        // ...

        // ��� ��� ��� ���������� ���������� ������� � �������
    }

    private static void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1f;
        // �������������� �������� ��� ������������� ���� (���� �����)
        // ...

        // ��� ��� ��� ��������� ���������� ������� � �������
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

