using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance;

    public GameObject OptionsPanel;
    public GameObject InspectionPanel;
    public Transform ItemContainer;
    public Canvas ParentCanvas;
    public Button UseButton;
    public Button InspectButton;
    public Button DiscardButton;
    public Button CancelButton;

    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        UpdateInventory();
    }
    private void Update()
    {
        if (GameManager.TypeOfControl == TypesOfControl.InventoryControl)
        {
            if (Input.GetKeyDown(KeyBindsList.InventoryControllBinds[InventoryControllBindTypes.CloseInventory]) || Input.GetKeyDown(KeyBindsList.InventoryControllBinds[InventoryControllBindTypes.AlternativeCloseInventory]))
                CloseInventory();
        }
    }

    private void OpenOptionsPanel(int itemID)
    {
        UseButton.onClick.AddListener(() => UseItem(itemID));
        InspectButton.onClick.AddListener(() => InspectItem(itemID));
        DiscardButton.onClick.AddListener(() => DiscardItem(itemID));
        CancelButton.onClick.AddListener(() => CloseOptionsPanel());

        OptionsPanel.SetActive(true);

        Vector3 mousePosition = Input.mousePosition;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(ParentCanvas.transform as RectTransform, mousePosition, ParentCanvas.worldCamera, out Vector2 localMousePosition);
        OptionsPanel.transform.position = ParentCanvas.transform.TransformPoint(localMousePosition);
    }
    private void UseItem(int itemID)
    {
        Item item = EntitiesList.Entities[itemID] as Item;
        item.Use();
        UpdateInventory();
    }
    private void InspectItem(int itemID)
    {
        InspectionPanel.SetActive(true);
        InspectionController.Instance.Initialize(itemID);
        UpdateInventory();
    }
    private void DiscardItem(int itemID)
    {
        Item item = EntitiesList.Entities[itemID] as Item;
        Player.Inventory.Remove(item);
        UpdateInventory();
    }
    public void CloseInventory()
    {
        CloseOptionsPanel();

        PlayerController.Instance.enabled = true;
        CameraController.Instance.enabled = true;

        GameManager.ChangeTypeOfControll(TypesOfControl.PlayerControl);
        GameManager.TogglePause();

        gameObject.SetActive(false);

        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
    }
    private void CloseOptionsPanel()
    {
        UseButton.onClick.RemoveAllListeners();
        InspectButton.onClick.RemoveAllListeners();
        DiscardButton.onClick.RemoveAllListeners();
        CancelButton.onClick.RemoveAllListeners();

        OptionsPanel.SetActive(false);
    }

    public void UpdateInventory()
    {
        foreach (Transform itemSlot in ItemContainer)
        {
            Destroy(itemSlot.gameObject);
        }

        foreach (InventorySlot slot in Player.Inventory.Slots)
        {
            Item item = EntitiesList.Entities[slot.ItemID] as Item;
            GameObject itemSlot = Instantiate(Resources.Load<GameObject>("Prefabs/ItemSlot"), ItemContainer, false);
            itemSlot.transform.Find("ItemIcon").GetComponent<Image>().sprite = item.Sprite;
            itemSlot.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = item.DisplayName;
            itemSlot.GetComponent<Button>().onClick.AddListener(() => OpenOptionsPanel(slot.ItemID));
        }

        CloseOptionsPanel();
    }
}