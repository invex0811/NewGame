using System;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

class UIController : MonoBehaviour, IDragHandler
{
    private GameObject _inspectablePrefab;

    public static UIController Instance;

    public GameObject Inventory;
    public GameObject OptionsPanel;
    public GameObject InspectionPanel;
    public Transform ItemContainer;
    public Camera InspectionCamera;
    public Canvas ParentCanvas;
    public Button UseButton;
    public Button InspectButton;
    public Button DiscardButton;
    public Button CancelButton;
    public Button CloseInspectionPanelButton;
    public TextMeshProUGUI ObjectName;
    public TextMeshProUGUI ObjectDescription;

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Player.Inventory.Add(ItemsList.Items[0]); // Код для дебага. Добавляет предмет "Key" в инвентарь игрока.
            UpdateInventory();
        }

        if (GameManager.TypeOfControl == TypesOfControl.InventoryControl && Input.GetKeyDown(KeyBindsList.InventoryControllBinds[InventoryControllBindTypes.CloseInventory]))
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
    private void UseItem(int itemID)
    {
        ItemsList.Items[itemID].Use();
        UpdateInventory();
    }
    private void InspectItem(int itemID)
    {
        if(_inspectablePrefab != null)
            Destroy(_inspectablePrefab.gameObject);

        _inspectablePrefab =  Instantiate(ItemsList.Items[itemID].Prefab, new Vector3 (1000, 1000, 1000), Quaternion.identity);

        CloseInspectionPanelButton.onClick.AddListener(() => CloseInspectionPanel());

        InspectionCamera.enabled = true;
        InspectionPanel.SetActive(true);

        ObjectName.text = ItemsList.Items[itemID].DisplayName;
        ObjectDescription.text = ItemsList.Items[itemID].Description;

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
    private void CloseInspectionPanel()
    {
        CloseInspectionPanelButton.onClick.RemoveAllListeners();

        InspectionPanel.SetActive(false);
        InspectionCamera.enabled = false;
    }

    public void OpenInventory()
    {
        Inventory.SetActive(true);
        UpdateInventory();
    }
    public void CloseInventory()
    {
        PlayerController.Instance.enabled = true;
        CameraController.Instance.enabled = true;

        GameManager.ChangeTypeOfControll(TypesOfControl.PlayerControl);
        GameManager.TogglePause();

        Inventory.SetActive(false);

        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if(_inspectablePrefab!= null)
            _inspectablePrefab.transform.eulerAngles += new Vector3(-eventData.delta.y / 3, -eventData.delta.x / 3);
    }
}