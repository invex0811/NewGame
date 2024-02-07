using UnityEngine;

class UIController : MonoBehaviour
{
    public static UIController Instance;

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Player.Inventory.Add(EntitiesList.Entities[0] as Item); // Код для дебага. Добавляет предмет "Key" в инвентарь игрока.
        }

        if (GameManager.TypeOfControl == TypesOfControl.PlayerControl && Input.GetKeyDown(KeyBindsList.PlayerControllBinds[PlayerControllBindTypes.OpenInventory]))
            OpenInventory();
    }

    public void OpenInventory()
    {
        CameraController.Instance.enabled = false;
        PlayerController.Instance.enabled = false;

        GameManager.TogglePause();

        if (InteractionController.Instance.CurrentInteraction == InteractionType.None)
            GameManager.ChangeTypeOfControll(TypesOfControl.InventoryControl);
        else
            GameManager.ChangeTypeOfControll(TypesOfControl.InteractionControl);

        InventoryController.Instance.enabled = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}