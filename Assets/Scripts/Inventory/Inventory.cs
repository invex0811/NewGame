using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject inventory;
    public bool flag = false;

    void Start()
    {
        Cursor.visible = false; // �������� ��������� ������ ��� ������
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            flag = !flag;
            inventory.SetActive(flag);

            // ���������/������������ ��������� ������ � ����������� �� ��������� ���������
            Cursor.visible = flag;

            // �������������/������������ �������� ������� � ����������� �� ��������� ���������
            if (flag)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Time.timeScale = 0f; // ������������� ����� � ����
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 1f; // ������������ ����� � ����
            }
        }
        // ��������� �������� ������� ������� CameraController
        CameraController cameraController = CameraController.instance;
        if (cameraController != null)
        {
            // ���� ��������� ������, ��������� �������� ������, ����� ��������
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
