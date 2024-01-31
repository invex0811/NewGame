using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject inventory;
    public bool flag = false;

    void Start()
    {
        Cursor.visible = false; // Скрываем системный курсор при старте
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            flag = !flag;
            inventory.SetActive(flag);

            // Блокируем/разблокируем системный курсор в зависимости от состояния инвентаря
            Cursor.visible = flag;

            // Останавливаем/возобновляем движение курсора в зависимости от состояния инвентаря
            if (flag)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Time.timeScale = 0f; // Останавливаем время в игре
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 1f; // Возобновляем время в игре
            }
        }
        // Добавляем проверку наличия объекта CameraController
        CameraController cameraController = CameraController.instance;
        if (cameraController != null)
        {
            // Если инвентарь открыт, отключаем вращение камеры, иначе включаем
            if (flag)
            {
                cameraController.DisableRotation();
            }
            else
            {
                cameraController.EnableRotation();
            }
        }
    }
    public void ToggleInventory()
    {

    }
}
