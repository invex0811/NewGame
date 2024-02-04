using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

class InventoryUI : MonoBehaviour
{
    public GameObject Inventory;
    public GameObject OptionsPanel;
    public Transform ItemContainer;
    public Canvas ParentCanvas;
    public Button UseButton;
    public Button InspectButton;
    public Button DiscardButton;
    public Button CancelButton;

    private void OnEnable()
    {
        UpdateInventory();
    }
    private void Update()
    {
        if (GameManager.TypeOfControl != TypesOfControl.InventoryControl)
        {
            Debug.LogError("Type of controll conflict");
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Player.Inventory.Add(ItemsList.Items[0]); // Код для дебага. Добавляет предмет "Key" в инвентарь игрока.
            UpdateInventory();
        }

        if (Input.GetKeyDown(KeyBindsList.InventoryControllBinds[InventoryControllBindTypes.CloseInventory]))
            CloseInventory();
    }

    private void UpdateInventory()
    {
        foreach (Transform itemSlot in ItemContainer)
        {
            Destroy(itemSlot.gameObject);
        }
        
        foreach  (InventorySlot slot in Player.Inventory.Slots)
        {
            GameObject itemSlot = Instantiate(Resources.Load<GameObject>("Prefabs/ItemSlot"), ItemContainer, false);
            itemSlot.transform.Find("ItemIcon").GetComponent<Image>().sprite = ItemsList.Items[slot.ItemID].Sprite;
            itemSlot.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = ItemsList.Items[slot.ItemID].DisplayName;
            itemSlot.GetComponent<Button>().onClick.AddListener(() => OpenOptionsPanel(slot.ItemID));
        }

        CloseOptionsPanel();
    }
    private void OpenOptionsPanel(int itemID)
    {
        UseButton.onClick.AddListener(() => UseItem(itemID));
        InspectButton.onClick.AddListener(() => InspectItem(itemID));
        DiscardButton.onClick.AddListener(() => DiscardItem(itemID));
        CancelButton.onClick.AddListener(() => CloseOptionsPanel());

        OptionsPanel.SetActive(true);

        Vector3 mousePosition = Input.mousePosition;
        Vector2 localMousePosition;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(ParentCanvas.transform as RectTransform, mousePosition, ParentCanvas.worldCamera, out localMousePosition);
        OptionsPanel.transform.position = ParentCanvas.transform.TransformPoint(localMousePosition);
    }
    private void CloseInventory()
    {
        PlayerController.Instance.enabled = true;
        CameraController.Instance.enabled = true;

        GameManager.ChangeTypeOfControll(TypesOfControl.PlayerControl);
        GameManager.TogglePause();

        Inventory.SetActive(false);

        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
    }
    private void UseItem(int itemID)
    {
        ItemsList.Items[itemID].Use();
        UpdateInventory();
    }
    private void InspectItem(int itemID)
    {
        UpdateInventory();
    }
    private void DiscardItem(int itemID)
    {
        Player.Inventory.Remove(ItemsList.Items[itemID]);
        UpdateInventory();
    }
    private void CloseOptionsPanel()
    {
        UseButton.onClick.RemoveAllListeners();
        InspectButton.onClick.RemoveAllListeners();
        DiscardButton.onClick.RemoveAllListeners();
        CancelButton.onClick.RemoveAllListeners();

        OptionsPanel.SetActive(false);
    }
}